using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Client
{
    public class RSA_Crypto
    {
        public static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        RSAParameters _privateKey;
        RSAParameters _publicKey;

        public RSA_Crypto()
        {
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }
        // Hàm in ra public key
        public string PublicKeyString()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string Encrypt(string plaintext)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_publicKey);

            var data = Encoding.Unicode.GetBytes(plaintext);
            var cypher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }
        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            csp.ImportParameters(_privateKey);
            var plaintext = csp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plaintext);
        }
    }
}
