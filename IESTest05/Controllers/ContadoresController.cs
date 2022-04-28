﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IESTest05.Data;
using MobileLite.Entities;
using IESTest05.Entity;

namespace MobileLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContadoresController : ControllerBase
    {
        private readonly DataContext context;

        public ContadoresController(DataContext context)
        {
            this.context = context;
        }

        // Devuelve un listado con todos los contadores
        [HttpGet] // GET: api/contadores
        public IEnumerable<Contadores> Get()
        {
            try
            {
                return context.Contadores.ToList();
            }
            catch
            {
                return null;
            }

        }

        //Recibe comco parametro la id de un contador y devuelve el correspondiente
        [HttpGet("{id}")] // GET api/contadores/5
        public Contadores Get(String id)
        {
            try
            {
                return context.Contadores.FirstOrDefault(c => c.Codigo.ToString() == id);
            }
            catch
            {
                return null;
            }

        }
    }
}