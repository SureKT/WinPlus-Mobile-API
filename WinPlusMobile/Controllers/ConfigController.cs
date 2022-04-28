﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WinPlusMobile.Data;
using MobileLite.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MobileLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly DataContext db;

        public ConfigController(DataContext context)
        {
            db = context;
        }

        // In: Id de config | Out: Objeto Config
        [HttpGet("{id}")] // GET api/config/5
        public Config Get(String id)
        {           
            try
            {
                return db.Config.FirstOrDefault(c => c.clave == id);
            }
            catch
            {
                return null;
            }
        }
    }
}