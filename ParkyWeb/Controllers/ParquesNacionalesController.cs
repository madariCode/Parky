﻿using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class ParquesNacionalesController : Controller
    {
        private readonly IParqueNacionalRepository _pnRepo;

        public ParquesNacionalesController(IParqueNacionalRepository pnRepo)
        {
            _pnRepo = pnRepo;
        }

        public IActionResult Index()
        {
            return View(new ParqueNacional() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ParqueNacional obj = new ParqueNacional();

            if(id == null)
            {
                //Insertar/Crear
                return View(obj);
            }
            
            //Update
            obj = await _pnRepo.GetAsync(SD.ParquesNacionalesAPIPath, id.GetValueOrDefault());

            if(obj == null)
            {
                NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> GetAllParquesNacionales()
        {
            return Json(new { data = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath) });
        }
    }
}
