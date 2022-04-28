﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IESTest05.Entity;
using IESTest05.Data;
using System.Globalization;
using MobileLite.Entities;

namespace IESTest05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VContadoresController : ControllerBase
    {
        private readonly DataContext db;

        public VContadoresController(DataContext context)
        {
            db = context;
        }

        //Recibe como parametro de entrada el token de sesion del usuario, la fecha inicio y fin y devuelve un listado de contadores dentro de esas fechas
        [HttpPost] // POST api/vcontadores
        public IEnumerable<Object> Post(string token, DateTime inicio, DateTime fin)
        {
            try
            {
                var tUsuario = db.TUsuarios.FirstOrDefault(u => u.Token == token);

                if (tUsuario == null)
                    return null;

                return db.VContadores.Where(vc =>
                    vc.Personal == tUsuario.Personal &&
                    vc.Fecha >= inicio &&
                    vc.Fecha <= fin)
                    .GroupBy(vc => vc.Contador)
                    .Select(c => new
                    {
                        Contador = c.Key,
                        Valor = c.Sum(v => v.Valor)
                    })
                    .Join(db.Contadores,
                    vc => vc.Contador,
                    c => c.Codigo,
                    (vc, c) => new { Codigo = vc.Contador, Descripcion = c.Descripcion, Valor = vc.Valor, Consultar = c.Consultar }).Where(c => c.Consultar == 1);
            }
            catch
            {
                return null;
            }
        }
    }
}