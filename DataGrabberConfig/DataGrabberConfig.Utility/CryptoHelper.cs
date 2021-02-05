using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataGrabberConfig.Utility
{

    public class CryptoHelper : ICryptoHelper
    {

        #region RSA Encryption


        public string RsaEncrypt(string plainText, int keySize, string rsaPublicKey)
        {
            var decryptedBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var doOaepPadding = true; // "OAEP";
            var rsa = GetNewRsaProvider();
            // Import the RSA Key information (exclude private key).
            rsa.ImportParameters(GetRsaKey(false, keySize, rsaPublicKey));
            // Encrypt the passed byte array and specify OAEP padding.
            var encryptedBytes = rsa.Encrypt(decryptedBytes, doOaepPadding);
            var encryptedString = System.Convert.ToBase64String(encryptedBytes);
            // ------------------------------------------------
            // Display the encrypted data.
            //var encryptedString = System.BitConverter.ToString(encryptedBytes).Replace("-","");
            return encryptedString;
        }

        public string RsaDecrypt(string encryptedText, int keySize, string rsaPrivateKey)
        {
            var encryptedBytes = System.Convert.FromBase64String(encryptedText);
            var doOaepPadding = true; // "OAEP";

            var rsa = GetNewRsaProvider(keySize);
            // Import the RSA Key information (include private key).
            rsa.ImportParameters(GetRsaKey(true, keySize, rsaPrivateKey));
            // Decrypt the passed byte array and specify OAEP padding.
            var decryptedBytes = rsa.Decrypt(encryptedBytes, doOaepPadding);
            // ------------------------------------------------
            // Display the decrypted data.
            var decryptedString = System.Text.Encoding.UTF8.GetString(decryptedBytes);
            return decryptedString;
        }

        private RSACryptoServiceProvider GetNewRsaProvider(int dwKeySize = 512)
        {
            // Tell IIS to use Machine Key store or creation of RSA service provider will fail.
            var cspParams = new CspParameters();
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            // Create a new instance of RSACryptoServiceProvider.
            return new System.Security.Cryptography.RSACryptoServiceProvider(dwKeySize, cspParams);
        }

        private RSAParameters GetRsaKey(bool includePrivateParameters, int keySize, string RsaPrivateKey)
        {
            var rsa = GetNewRsaProvider(keySize);
            var keyParams = RsaPrivateKey;
            if (keyParams.StartsWith("<"))
            {
                // Import parameters from XML.
                rsa.FromXmlString(keyParams);
            }
            else
            {
                // Import parameters from BLOB.
                var keyBlob = System.Convert.FromBase64String(keyParams);
                rsa.ImportCspBlob(keyBlob);
            }
            // Export RSA key to RSAParameters and include:
            //    false - Only public key required for encryption.
            //    true  - Private key required for decryption.
            return rsa.ExportParameters(includePrivateParameters);
        }

        #endregion


        #region AES-256 Encryption

        private byte[] Transform(byte[] dataBytes, byte[] passwordBytes, bool encrypt)
        {
            /// <summary>Encrypt by using AES-256 algorithm.</summary>
            // Create an instance of the AES class.
            var cipher = new System.Security.Cryptography.AesCryptoServiceProvider();
            // Calculate salt to make it harder to guess key by using a dictionary attack.
            var hmac = new System.Security.Cryptography.HMACSHA1(passwordBytes);
            var salt = hmac.ComputeHash(passwordBytes);
            // Generate Secret Key from the password and salt.
            // Note: Set number of iterations to 10 in order for JavaScript example to work faster.
            var secretKey = new System.Security.Cryptography.Rfc2898DeriveBytes(passwordBytes, salt, 10);
            // Create a encryptor from the existing SecretKey bytes by using
            // 32 bytes (256 bits) for the secret key and
            // 16 bytes (128 bits) for the initialization vector (IV).
            var key = secretKey.GetBytes(32);
            var iv = secretKey.GetBytes(16);
            // Get cryptor as System.Security.Cryptography.ICryptoTransform class.
            var cryptor = encrypt
                ? cipher.CreateEncryptor(key, iv)
                : cipher.CreateDecryptor(key, iv);
            // Create new Input.
            var inputBuffer = new byte[dataBytes.Length];
            // Copy data bytes to input buffer.
            System.Buffer.BlockCopy(dataBytes, 0, inputBuffer, 0, inputBuffer.Length);
            // Create a MemoryStream to hold the output bytes.
            var stream = new System.IO.MemoryStream();
            // Create a CryptoStream through which we are going to be processing our data.
            var mode = System.Security.Cryptography.CryptoStreamMode.Write;
            var cryptoStream = new System.Security.Cryptography.CryptoStream(stream, cryptor, mode);
            // Start the crypting process.
            cryptoStream.Write(inputBuffer, 0, inputBuffer.Length);
            // Finish crypting.
            cryptoStream.FlushFinalBlock();
            // Convert data from a memoryStream into a byte array.
            var outputBuffer = stream.ToArray();
            // Close both streams.
            stream.Close();
            cryptoStream.Close();
            return outputBuffer;
        }

        /// <summary>
		/// Encrypt string with AES-256 by using password.
		/// </summary>
		/// <param name="plainText">String (or bytes) to encrypt.</param>
		/// <param name="aesPassword">Password string (or bytes).</param>
		/// <returns>Encrypted Base64 string.</returns>
		public string AesEncrypt(string plainText, string aesPassword)
        {
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(aesPassword);
            var bytes = Transform(dataBytes, passwordBytes, true);
            // Convert encrypted data into a Base64 string.
            var text = System.Convert.ToBase64String(bytes);
            return text;
        }

        /// <summary>
        /// Decrypt string with AES-256 by using password.
        /// </summary>
        /// <param name="encryptedText">Base64 string or bytes to encrypt.</param>
        /// <param name="aesPassword">Password string (or bytes).</param>
        /// <returns>Decrypted string.</returns>
        public string AesDecrypt(string encryptedText, string aesPassword)
        {
            // If data is string then turn string into a byte array.
            var dataBytes = System.Convert.FromBase64String(encryptedText);
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(aesPassword);
            var bytes = Transform(dataBytes, passwordBytes, false);
            // Convert decrypted data into a string.
            var text = System.Text.Encoding.UTF8.GetString(bytes);
            return text;
        }


        #endregion


        #region SHA Hash


        public string ShaHash(string plainText)
        {
            var text = string.Empty;
            text = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(plainText)));
            return text;
        }


        #endregion


    }


    public interface ICryptoHelper
    {

        string RsaEncrypt(string plainText, int keySize, string rsaPublicKey);

        string RsaDecrypt(string encryptedText, int keySize, string rsaPrivateKey);


        string AesEncrypt(string plainText, string aesPassword);

        string AesDecrypt(string encryptedText, string aesPassword);


        string ShaHash(string plainText);

    }

}
