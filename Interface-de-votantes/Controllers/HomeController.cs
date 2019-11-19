using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interface_de_votantes.Models;
using Interface_de_votantes.Services;
using Interface_de_votantes.Digital_singning;

namespace Interface_de_votantes.Controllers
{
    public class HomeController : Controller
    {
        Storage Temp = Storage.GetInstance();

        DateTime thisDay = DateTime.Today;
        
        
        public bool valido = false;
        public int Mun;
        public int Dep;
        public int todoDPI;
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Municipio { get; set; }

        public bool Validacion(string dpi)
        {

            if (dpi.Length == 13)
            {
                return valido = true;
            }
            else
            {
                return valido = false;
            }
        }
        //Extrae todos los datos
        public void Convertir(string ndpi)
        {
            string Dep1;
            string Mun1;
            string todoDPI1;

            Dep1 = ndpi.Substring(9, 2);
            Mun1 = ndpi.Substring(11, 2);
            todoDPI1 = ndpi.Substring(0, 13);

            Dep = int.Parse(Dep1);
            Mun = int.Parse(Mun1);
            todoDPI = int.Parse(todoDPI1);
        }
        


public IActionResult Index(string dpi, int depto, int presi, int alcalde, int boletadiputadodistrito, int boletadiputadonacional, string jclock1)
        {
            ViewBag.FechaAlta = new DateTime(2008, 12, 10);

            if (dpi == null){ 
                return View("Index", Temp);
            }
            else{

                Temp.Listado.Add(new Votantes()
                {
                    id = Temp.Listado.Count,
                    depto = depto,
                    dpi = dpi,
                    boleta_presidente = presi,
                    boleta_alcalde = alcalde,
                    boleta_diputados_distrito = boletadiputadodistrito,
                    boleta_diputado_nacional = boletadiputadonacional,
                    fecha_hora = jclock1
                });
                var Firma = Digital_signing.Instance.Generar_llaves(dpi);
                ViewBag.Message =Firma;
                return View("Index", Temp);
            }
        }

        public IEnumerable<Votantes> Get()
        {
            return Temp.Listado;
        }
    }
}
