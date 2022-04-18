using EosSharp;
using EosSharp.Core;
using EosSharp.Core.Api.v1;
using EosSharp.Core.Helpers;
using EosSharp.Core.Interfaces;
using EosSharp.Core.Providers;
using FW_V3.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.SubClass
{
   public class EosClass : EosBase
    {
        private EosConfigurator EosConfig { get; set; }
        private EosApi Api { get; set; }
        private AbiSerializationProvider AbiSerializer { get; set; }
        public ProcessedTransaction response { get; set; }
        /// <summary>
        /// EOSIO Client wrapper constructor.
        /// </summary>
        /// <param name="config">Configures client parameters</param>
        /// 

        public static byte[] WriteName(string name)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var a = SerializationHelper.ConvertNameToBytes(name);
                ms.Write(a, 0, 8);
                return ms.ToArray();
            }
        }
        public EosClass(EosConfigurator config, IHttpHandler httpHandler) :
            base(config, new HttpHandler())
        {
            EosConfig = config;
            if (EosConfig == null)
            {
                throw new ArgumentNullException("config");
            }
            Api = new EosApi(EosConfig, httpHandler);
            AbiSerializer = new AbiSerializationProvider(Api);
        }

        /// <summary>
        /// Creates a signed transaction using the signature provider and broadcasts it to the network
        /// </summary>
        /// <param name="trx">Transaction to send</param>
        /// <param name="requiredKeys">Override required keys to sign transaction</param>
        /// <returns>transaction id</returns>
        /// 
        public async Task<string> CreateTransaction(Transaction trx, List<string> requiredKeys = null,bool t = false)
        { 
            var signedTrx = await SignTransaction2(trx, requiredKeys);
            response = await BroadcastTransaction(signedTrx);
            return $"Success";
        }
        public async Task<string> CreateTransaction(Transaction trx, string token)
        {
            var signedTrx = await SignTransaction(trx, token);
            if (signedTrx.Signatures.ToArray()[0] == "Session Token is invalid or missing.")
                return "Invalid Token";
            else if (signedTrx.Signatures.ToArray()[0] == "Exceeded rate limit. Cooling down for 60 minutes")
                return "Limit";
            else
            {
                response = await BroadcastTransaction(signedTrx);
                return $"Success";
            }
        }

        /// <summary>
        /// Creates a signed transaction using the signature provider
        /// </summary>
        /// <param name="trx">Transaction to sign</param>
        /// <param name="requiredKeys">Override required keys to sign transaction</param>
        /// <returns>transaction id</returns>
        ///  
        public async Task<SignedTransaction> SignTransaction2(Transaction trx, List<string> requiredKeys = null)
        {
          
            if (trx == null)
                throw new ArgumentNullException("Transaction");

            if (EosConfig.SignProvider == null)
                throw new ArgumentNullException("SignProvider");

            GetInfoResponse getInfoResult = null;
            string chainId = EosConfig.ChainId;

            if (string.IsNullOrWhiteSpace(chainId))
            {
                getInfoResult = await Api.GetInfo();
                chainId = getInfoResult.chain_id;
            }
          
            if (trx.expiration == DateTime.MinValue ||
               trx.ref_block_num == 0 ||
               trx.ref_block_prefix == 0)
            {
                if (getInfoResult == null)
                    getInfoResult = await Api.GetInfo();

                var taposBlockNum = getInfoResult.head_block_num - (int)EosConfig.blocksBehind;

                if ((taposBlockNum - getInfoResult.last_irreversible_block_num) < 2)
                {
                    if (EAV.block_num == 0)
                    {
                        var getBlockResult = await Api.GetBlock(new GetBlockRequest()
                        {
                            block_num_or_id = taposBlockNum.ToString()
                        });
                        trx.expiration = getBlockResult.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                        trx.ref_block_num = (UInt16)(getBlockResult.block_num & 0xFFFF);
                        trx.ref_block_prefix = getBlockResult.ref_block_prefix;
                    }
                    else
                    {
                        var getBlockHeaderState = await Api.GetBlockHeaderState(new GetBlockHeaderStateRequest()
                        {
                            block_num_or_id = taposBlockNum.ToString()
                        });
                        trx.expiration = getBlockHeaderState.header.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                        trx.ref_block_num = (UInt16)(getBlockHeaderState.block_num & 0xFFFF);
                        trx.ref_block_prefix = Convert.ToUInt32(SerializationHelper.ReverseHex(getBlockHeaderState.id.Substring(16, 8)), 16);
                    }
               
                }
                else
                {
                   if(EAV.block_num == 0)
                    { 
                        var getBlockHeaderState = await Api.GetBlockHeaderState(new GetBlockHeaderStateRequest()
                        {
                            block_num_or_id = taposBlockNum.ToString()
                        });
                        trx.expiration = getBlockHeaderState.header.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                        trx.ref_block_num = (UInt16)(getBlockHeaderState.block_num & 0xFFFF);
                        trx.ref_block_prefix = Convert.ToUInt32(SerializationHelper.ReverseHex(getBlockHeaderState.id.Substring(16, 8)), 16);
                    }
                    else
                    {
                        var getBlockResult = await Api.GetBlock(new GetBlockRequest()
                        {
                            block_num_or_id = taposBlockNum.ToString()
                        });
                        trx.expiration = getBlockResult.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                        trx.ref_block_num = (UInt16)(getBlockResult.block_num & 0xFFFF);
                        trx.ref_block_prefix = getBlockResult.ref_block_prefix;
                    }
                 
                }
            }

            var packedTrx = await AbiSerializer.SerializePackedTransaction(trx);
          
            if (requiredKeys == null || requiredKeys.Count == 0)
            {
                var availableKeys = await EosConfig.SignProvider.GetAvailableKeys();
                requiredKeys = await GetRequiredKeys(availableKeys.ToList(), trx);
            }

            IEnumerable<string> abis = null;

            if (trx.actions != null)
                abis = trx.actions.Select(a => a.account);

            var signatures = await EosConfig.SignProvider.Sign(chainId, requiredKeys, packedTrx, abis);
          
            return new SignedTransaction()
            {
                Signatures = signatures,
                PackedTransaction = packedTrx
            };
        }

        public async Task<SignedTransaction> SignTransaction(Transaction trx, string token)
        {

            if (trx == null)
                throw new ArgumentNullException("Transaction");

            GetInfoResponse getInfoResult = null;
            string chainId = EosConfig.ChainId;

            if (string.IsNullOrWhiteSpace(chainId))
            {
                getInfoResult = await Api.GetInfo();
                chainId = getInfoResult.chain_id;
            }

            if (trx.expiration == DateTime.MinValue ||
               trx.ref_block_num == 0 ||
               trx.ref_block_prefix == 0)
            {
                if (getInfoResult == null)
                    getInfoResult = await Api.GetInfo();

                var taposBlockNum = getInfoResult.head_block_num - (int)EosConfig.blocksBehind;

                if ((taposBlockNum - getInfoResult.last_irreversible_block_num) < 2)
                {
                    var getBlockResult = await Api.GetBlock(new GetBlockRequest()
                    {
                        block_num_or_id = taposBlockNum.ToString()
                    });
                    trx.expiration = getBlockResult.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                    trx.ref_block_num = (UInt16)(getBlockResult.block_num & 0xFFFF);
                    trx.ref_block_prefix = getBlockResult.ref_block_prefix;
                }
                else
                {
                    var getBlockHeaderState = await Api.GetBlockHeaderState(new GetBlockHeaderStateRequest()
                    {
                        block_num_or_id = taposBlockNum.ToString()
                    });
                    trx.expiration = getBlockHeaderState.header.timestamp.AddSeconds(EosConfig.ExpireSeconds);
                    trx.ref_block_num = (UInt16)(getBlockHeaderState.block_num & 0xFFFF);
                    trx.ref_block_prefix = Convert.ToUInt32(SerializationHelper.ReverseHex(getBlockHeaderState.id.Substring(16, 8)), 16);
                }
            }

            var packedTrx = await AbiSerializer.SerializePackedTransaction(trx);

            IEnumerable<string> abis = null;

            if (trx.actions != null)
                abis = trx.actions.Select(a => a.account);
            var packed = packedTrx.Select(x => Convert.ToString(x)).ToArray();
            var signatures = WaxAPI.sign(token, packed.PackArray());
            return new SignedTransaction()
            {
                Signatures = signatures,
                PackedTransaction = packedTrx
            };
        }

        public new async Task<ProcessedTransaction> BroadcastTransaction(SignedTransaction strx)
        {
            if (strx == null)
                throw new ArgumentNullException("SignedTransaction");

            var result = await Api.PushTransaction(new PushTransactionRequest()
            {
                signatures = strx.Signatures.ToArray(),
                compression = 0,
                packed_context_free_data = "",
                packed_trx = SerializationHelper.ByteArrayToHexString(strx.PackedTransaction)
            });
            return result.processed;
        }
    }
}
