﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IESTest05.Data;
using IESTest05.Entity;
using MobileLite.Entities;

namespace IESTest05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly DataContext db;

        public PersonalController(DataContext context)
        {
            db = context;
        }

        // GET api/personal
        // Recibe como parametro un token de sesion y devuelve el personal al que esté relacionado
        [HttpGet("{Token}")]
        public Personal Get(String token)
        {
            try
            {
                TUsuarios usuario = db.TUsuarios.FirstOrDefault(u => u.Token == token);
                Personal personal = db.Personal.FirstOrDefault(u => u.Codigo == usuario.Personal);
                return personal;
            }
            catch
            {
                return null;
            }

        }

        // POST api/personal
        // Recibe como parametro un token de sesion y devuelve el string de la foto de usuarios codificada en base 64
        [HttpPost("{Token}")]
        public String Post(String token)
        {
            try
            {
                Config dirFoto = db.Config.FirstOrDefault(c => c.clave == "DirFotos");
                TUsuarios usuario = db.TUsuarios.FirstOrDefault(u => u.Token == token);
                Personal personal = db.Personal.FirstOrDefault(p => p.Codigo == usuario.Personal);
                Byte[] b = System.IO.File.ReadAllBytes(dirFoto.valor + personal.Foto);
                return Convert.ToBase64String(b);
            }
            catch
            {
                return null;
            }
        }

        private bool PersonalExists(string id)
        {
            return db.Personal.Any(p => p.Codigo == id);
        }
    }
}
