﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interface_de_votantes.Models;
using Interface_de_votantes.Services;

namespace Interface_de_votantes.Controllers
{
    public class HomeController : Controller
    {
        Storage Temp = Storage.GetInstance();

        public IActionResult Index(string dpi, int depto, int presi, int alcalde, int boletadiputadodistrito, int boletadiputadonacional, string fecha)
        {
            Temp.Listado.Add(new Votantes()
            {
                id = Temp.Listado.Count,
                depto = depto,
                dpi = dpi,
                boleta_presidente = presi,
                boleta_alcalde = alcalde,
                boleta_diputados_distrito = boletadiputadodistrito,
                boleta_diputado_nacional = boletadiputadonacional,
                fecha_hora = fecha
            });
            return View("Index", Temp);
        }

        public IEnumerable<Votantes> Get()
        {
            return Temp.Listado;
        }
    }
}
