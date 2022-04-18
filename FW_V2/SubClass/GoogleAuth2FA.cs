using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.SubClass
{
    class GoogleAuth2FA
    {
		protected string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

		private int intervalLength;
		private int pinCodeLength;
		private int pinModulo;

		private byte[] randomBytes = new byte[10];

		public GoogleAuth2FA(string secret)
		{
			pinCodeLength = 6;
			intervalLength = 30;
			pinModulo = (int)Math.Pow(10, pinCodeLength);
			var key = secret.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
			randomBytes = Transcoder.ToBytes(string.Concat(key));
		}

		public byte[] getPrivateKey()
		{
			return randomBytes;
		}

		/// <summary>
		/// Generates a PIN of desired length when given a challenge (counter)
		/// </summary>
		/// <param name="challenge">Counter to calculate hash</param>
		/// <returns>Desired length PIN</returns>
		private String generateResponseCode(long challenge, byte[] randomBytes)
		{
			HMACSHA1 myHmac = new HMACSHA1(randomBytes);
			myHmac.Initialize();

			byte[] value = BitConverter.GetBytes(challenge);
			Array.Reverse(value); //reverses the challenge array due to differences in c# vs java
			myHmac.ComputeHash(value);
			byte[] hash = myHmac.Hash;
			int offset = hash[hash.Length - 1] & 0xF;
			byte[] SelectedFourBytes = new byte[4];
			//selected bytes are actually reversed due to c# again, thus the weird stuff here
			SelectedFourBytes[0] = hash[offset];
			SelectedFourBytes[1] = hash[offset + 1];
			SelectedFourBytes[2] = hash[offset + 2];
			SelectedFourBytes[3] = hash[offset + 3];
			Array.Reverse(SelectedFourBytes);
			int finalInt = BitConverter.ToInt32(SelectedFourBytes, 0);
			int truncatedHash = finalInt & 0x7FFFFFFF; //remove the most significant bit for interoperability as per HMAC standards
			int pinValue = truncatedHash % pinModulo; //generate 10^d digits where d is the number of digits
			return padOutput(pinValue);
		}

		/// <summary>
		/// Gets current interval number since Unix Epoch based on given interval length
		/// </summary>
		/// <returns>Current interval number</returns>
		public long getCurrentInterval()
		{
			TimeSpan TS = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long currentTimeSeconds = (long)Math.Floor(TS.TotalSeconds);
			long currentInterval = currentTimeSeconds / intervalLength; // 30 Seconds
			return currentInterval;
		}

		/// <summary>
		/// Pads the output string with leading zeroes just in case the result is less than the length of desired digits
		/// </summary>
		/// <param name="value">Value to pad</param>
		/// <returns>Padded Result</returns>
		private String padOutput(int value)
		{
			String result = value.ToString();
			for (int i = result.Length; i < pinCodeLength; i++)
			{
				result = "0" + result;
			}
			return result;
		}

		/// <summary>
		/// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
		/// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
		/// </summary>
		/// <param name="value">The value to Url encode</param>
		/// <returns>Returns a Url encoded string</returns>
		protected string UrlEncode(string value)
		{
			StringBuilder result = new StringBuilder();

			foreach (char symbol in value)
			{
				if (unreservedChars.IndexOf(symbol) != -1)
				{
					result.Append(symbol);
				}
				else
				{
					result.Append('%' + String.Format("{0:X2}", (int)symbol));
				}
			}

			return result.ToString();
		}


		public string GeneratePin()
		{
			return generateResponseCode(getCurrentInterval(), randomBytes);
		}

	}
	public class Transcoder
	{
		private const int IN_BYTE_SIZE = 8;
		private const int OUT_BYTE_SIZE = 5;
		private static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

		public static byte[] ToBytes(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}

			input = input.TrimEnd('='); //remove padding characters
			int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
			byte[] returnArray = new byte[byteCount];

			byte curByte = 0, bitsRemaining = 8;
			int mask = 0, arrayIndex = 0;

			foreach (char c in input)
			{
				int cValue = CharToValue(c);

				if (bitsRemaining > 5)
				{
					mask = cValue << (bitsRemaining - 5);
					curByte = (byte)(curByte | mask);
					bitsRemaining -= 5;
				}
				else
				{
					mask = cValue >> (5 - bitsRemaining);
					curByte = (byte)(curByte | mask);
					returnArray[arrayIndex++] = curByte;
					curByte = (byte)(cValue << (3 + bitsRemaining));
					bitsRemaining += 3;
				}
			}

			//if we didn't end with a full byte
			if (arrayIndex != byteCount)
			{
				returnArray[arrayIndex] = curByte;
			}

			return returnArray;
		}
		private static int CharToValue(char c)
		{
			int value = (int)c;

			//65-90 == uppercase letters
			if (value < 91 && value > 64)
			{
				return value - 65;
			}
			//50-55 == numbers 2-7
			if (value < 56 && value > 49)
			{
				return value - 24;
			}
			//97-122 == lowercase letters
			if (value < 123 && value > 96)
			{
				return value - 97;
			}

			throw new ArgumentException("Character is not a Base32 character.", "c");
		}
		public static string Base32Encode(byte[] data)
		{
			int i = 0, index = 0, digit = 0;
			int current_byte, next_byte;
			StringBuilder result = new StringBuilder((data.Length + 7) * IN_BYTE_SIZE / OUT_BYTE_SIZE);

			while (i < data.Length)
			{
				current_byte = (data[i] >= 0) ? data[i] : (data[i] + 256); // Unsign

				/* Is the current digit going to span a byte boundary? */
				if (index > (IN_BYTE_SIZE - OUT_BYTE_SIZE))
				{
					if ((i + 1) < data.Length)
						next_byte = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
					else
						next_byte = 0;

					digit = current_byte & (0xFF >> index);
					index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
					digit <<= index;
					digit |= next_byte >> (IN_BYTE_SIZE - index);
					i++;
				}
				else
				{
					digit = (current_byte >> (IN_BYTE_SIZE - (index + OUT_BYTE_SIZE))) & 0x1F;
					index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
					if (index == 0)
						i++;
				}
				result.Append(alphabet[digit]);
			}

			return result.ToString();
		}
	}
}
