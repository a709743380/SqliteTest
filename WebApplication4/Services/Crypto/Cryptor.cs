using System.Collections;
using System.Security.Cryptography;
using System.Text;
using WebApplication4.Interface;
using WebApplication4.Models;

namespace WebApplication4.Services.Crypto
{
    public class Cryptor : ICrypto
    {
        public CryptoModel DoCrypto(CryptoModel model)
        {
            switch (model.Option)
            {
                case e_OPTION.Encryp:
                    if (!string.IsNullOrEmpty(model.DecodeContent))
                        AESEncrypt(ref model);
                    break;
                case e_OPTION.Decryp:
                    if (!string.IsNullOrEmpty(model.EncodeContent))
                        AESDecrypt(ref model);
                    break;
                case e_OPTION.PwdHash:
                    if (!string.IsNullOrEmpty(model.DecodeContent))
                        PwdHashEncrypt(ref model);
                    break;
                case e_OPTION.ToBase64:
                    if (!string.IsNullOrEmpty(model.DecodeContent))
                        ToBase64(ref model);
                    break;
            }

            return model;
        }

        public void AESEncrypt(ref CryptoModel model)
        {
            bool isEncrypt = true;

            byte[] Key = Encoding.UTF8.GetBytes(model.Key);
            byte[] IV = Encoding.UTF8.GetBytes(model.Iv);
            byte[] data = switchEncryptInput(isEncrypt, model.DecodeContent);
            byte[] byteArray = aesProcess(isEncrypt, data, Key, IV);
            model.EncodeContent = switchEncryptResult(isEncrypt, byteArray);
        }
        public void AESDecrypt(ref CryptoModel model)
        {
            bool isEncrypt = false;

            byte[] Key = Encoding.UTF8.GetBytes(model.Key);
            byte[] IV = Encoding.UTF8.GetBytes(model.Iv);
            byte[] data = switchEncryptInput(isEncrypt, model.EncodeContent);
            byte[] byteArray = aesProcess(isEncrypt, data, Key, IV);
            model.DecodeContent = switchEncryptResult(isEncrypt, byteArray);

        }
        public void PwdHashEncrypt(ref CryptoModel model)
        {
            model.EncodeContent = HashSha256(model.Key + model.DecodeContent + model.Iv);
        }
        public void ToBase64(ref CryptoModel model)
        {
            byte[] data = switchEncryptInput(false, model.DecodeContent);
            model.EncodeContent = switchEncryptResult(true, data);
        }

        private string HashSha256(string content, int stringType = 0)
        {
            byte[] input = Encoding.UTF8.GetBytes(content);
            byte[] output = null;

            using (var provider = new SHA256CryptoServiceProvider())
            {
                output = provider.ComputeHash(input);
            }

            if (stringType == 0)
            {
                return BitConverter.ToString(output).Replace("-", string.Empty);
            }
            else
            {
                return Convert.ToBase64String(output);
            }
        }
        private byte[] aesProcess(bool isEncrypt, byte[] str, byte[] key, byte[] iv)
        {
            byte[] result = null;

            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform cryptoTransform = null;
                if (isEncrypt)
                {
                    cryptoTransform = aes.CreateEncryptor();
                }
                else
                {
                    cryptoTransform = aes.CreateDecryptor();
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                    {
                        cs.Write(str, 0, str.Length);
                        cs.FlushFinalBlock();
                        result = ms.ToArray();
                    }
                }

                return result;
            }
        }
        private byte[] switchEncryptInput(bool isEncrypt, string str)
        {
            byte[] result = null;

            if (!string.IsNullOrWhiteSpace(str))
            {
                if (isEncrypt)
                {
                    result = Encoding.UTF8.GetBytes(str);
                }
                else
                {
                    result = Convert.FromBase64String(str);
                }
            }

            return result;
        }
        private string switchEncryptResult(bool isEncrypt, byte[] byteArray)
        {
            string result = null;

            if (byteArray != null && byteArray.Length > 0)
            {
                if (isEncrypt)
                {
                    result = Convert.ToBase64String(byteArray);
                }
                else
                {
                    result = Encoding.UTF8.GetString(byteArray);
                }
            }

            return result;
        }
    }
}
