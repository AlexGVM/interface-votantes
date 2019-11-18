using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Generators;
using System.IO;
using System.Text;
using Interface_de_votantes.Models;
using Interface_de_votantes.Controllers;

namespace Interface_de_votantes.Digital_singning
{
    public class Digital_signing
    {
        private static Digital_signing _instance = null;
        public static Digital_signing Instance
        {
            get
            {
                if (_instance == null) _instance = new Digital_signing();
                return _instance;
            }
        }
        public Dictionary<string, string> save = new Dictionary<string, string>();

        public string Generar_llaves(string SourceData)
        {
            #region Variables
            var Dato = SourceData;
            byte[] tmpSourse;
            string print_publicKey = "";
            string Privada = "";
            #endregion

            if (Dato != null)
            {
                tmpSourse = ASCIIEncoding.ASCII.GetBytes(Dato);
                /// Los pares de claves se están generando ... Espere unos momentos ......
                RsaKeyPairGenerator rsaKeyPairGen = new RsaKeyPairGenerator();
                rsaKeyPairGen.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
                AsymmetricCipherKeyPair keyPair = rsaKeyPairGen.GenerateKeyPair();
                ///
                RsaKeyParameters privateKey = (RsaKeyParameters)keyPair.Private;
                RsaKeyParameters PublicKey = (RsaKeyParameters)keyPair.Public;
                ///
                TextWriter textWriterl = new StringWriter();
                PemWriter pemWriterl = new PemWriter(textWriterl);
                ///llave publica
                pemWriterl.WriteObject(PublicKey);
                pemWriterl.Writer.Flush();
                print_publicKey = textWriterl.ToString();
                ///llave privada
                ISigner sign = SignerUtilities.GetSigner(PkcsObjectIdentifiers.Sha1WithRsaEncryption.Id);
                sign.Init(true, privateKey);
                sign.BlockUpdate(tmpSourse, 0, tmpSourse.Length);
                byte[] singnature = sign.GenerateSignature();
                    Privada = ByteArrayToString(singnature);
                Almacenar(Dato, Privada);
            }

            return Privada;
        }
        static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("x").ToLower());
            }
            return sOutput.ToString();
        }
        public void Almacenar(string llave, string valor)
        {
            save.Add(llave, valor);
        }
        public bool Buscar(string dpi, string priv)
        {
            bool Estado = false;
            if (save.ContainsKey(dpi) )
            {
                Estado = true;
            }
            return Estado;
        }
    }
}
