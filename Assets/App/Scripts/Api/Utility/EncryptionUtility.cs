using System;
using System.Security.Cryptography;
using System.Text;

namespace Flappy.Api
{
	/// <summary>
	/// 暗号化/復号
	/// </summary>
	public class EncryptionUtility
	{
		/// <summary>
		/// 暗号化
		/// </summary>
		/// <param name="plainText">テキスト</param>
		/// <param name="key">暗号化キー</param>
		public static string Encrypt(string plainText, string key)
		{
			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = Encoding.UTF8.GetBytes(key);
				aesAlg.Mode = CipherMode.ECB;
				aesAlg.Padding = PaddingMode.PKCS7;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (var msEncrypt = new System.IO.MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}
					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		/// <summary>
		/// 復号
		/// </summary>
		/// <param name="cipherText">暗号化テキスト</param>
		/// <param name="key">暗号化キー</param>
		public static string Decrypt(string cipherText, string key)
		{
			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = Encoding.UTF8.GetBytes(key);
				aesAlg.Mode = CipherMode.ECB;
				aesAlg.Padding = PaddingMode.PKCS7;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}

		/*
		/// <summary>
		/// 使い方サンプル
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			string key = "YourSecretKey"; // 16バイトのキーを使用する場合
			string plainText = "Hello, AES-128-ECB!";

			string encryptedText = Encrypt(plainText, key);
			Console.WriteLine("Encrypted Text: " + encryptedText);

			string decryptedText = Decrypt(encryptedText, key);
			Console.WriteLine("Decrypted Text: " + decryptedText);
		}
		*/
	}
}