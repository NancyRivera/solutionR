using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace SGOUtil
{
    public static class EncryptionExtension
    {
        public static string ToSha1Encode(this string contents)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(contents));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public static string ToSha256Encode(this string contents)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(contents);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        public static string ToMd5Hash(this string input)
        {
            // Create a new instance of the MD5 object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static string EncriptarConClave(this string texto)
        {
            try
            {

                var secretKey = ConfigurationManager.AppSettings["CLAVE_MD5_ENCRIP"];
                string key = secretKey;

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return texto;
        }
        public static string DesencriptarConClave(this string textoEncriptado)
        {
            try
            {
                var secretKey = ConfigurationManager.AppSettings["CLAVE_MD5_ENCRIP"];
                string key = secretKey;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //var keyArray = hashmd5.ComputeHash(Encoding.Default.GetBytes(textoEncriptado));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                //textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return textoEncriptado;
        }
        public static string ToEncriptar(this string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public static string ToDesEncriptar(this string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
        public static string TMD5Hash(this string word)
        {
            MD5 md5 = MD5.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            var stream = md5.ComputeHash(encoding.GetBytes(word));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();
        }
        public static string Token()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray()) i *= b + 1;
            //return TMD5Hash(string.Format("{0:x}", i - DateTime.Now.Ticks));
            return TMD5Hash($"{i - DateTime.Now.Ticks:x}");
        }
        public static string ToBase64Encode(this string word)
        {
            byte[] byt = Encoding.UTF8.GetBytes(word);
            return Convert.ToBase64String(byt);
        }
        public static string ToBase64Decode(this string word)
        {
            byte[] b = Convert.FromBase64String(word);
            return Encoding.UTF8.GetString(b);
        }
        public static string ToSHA1(this string str)
        {
            SHA1 sha1 = SHA1.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            var stream = sha1.ComputeHash(encoding.GetBytes(str));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();
        }
        public static string ToSHA256(this string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            var stream = sha256.ComputeHash(encoding.GetBytes(str));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();
        }
        public static string ToSHA384(this string str)
        {
            SHA384 sha384 = SHA384.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            var stream = sha384.ComputeHash(encoding.GetBytes(str));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();
        }
        public static string ToSHA512(this string str)
        {
            SHA512 sha512 = SHA512.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            //byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            var stream = sha512.ComputeHash(encoding.GetBytes(str));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();
        }
    }//end class
}
