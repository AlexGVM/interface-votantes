using System;
using System.Collections.Generic;
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
            string Privada = "";           
            var Primero = new Random();
            var Segundo = new Random();
            #endregion

            if (Dato != null)
            {
               var P = Primero.Next(1000, 9000);
                var S = Segundo.Next(10, 90);
                Random rand = new Random();

                int numero = rand.Next(26);

                char letra = (char)(((int)'A') + numero);
                Privada = Convert.ToString(P) + Convert.ToString(S) + rand;
                Almacenar(Dato, Privada);
            }

            return Privada;
        }
        public void Almacenar(string llave, string valor)
        {
            save.Add(llave, valor);
        }
        public bool Buscar(string dpi, string priv)
        {
            bool Estado = false;
            if (save.ContainsKey(dpi) && save.ContainsValue(priv))
            {
                Estado = true;
            }
            return Estado;
        }
    }
}
