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

        public IActionResult Index(string dpi, int depto, int presi, int alcalde, int boletadiputadodistrito, int boletadiputadonacional, string jclock1)
        {
            ViewBag.FechaAlta = new DateTime(2008, 12, 10);

            if(dpi == null){ 
                return View("Index", Temp);
            }else{         
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
                return View("Index", Temp);
            }
        }

        public IEnumerable<Votantes> Get()
        {
            return Temp.Listado;
        }
    }
}
