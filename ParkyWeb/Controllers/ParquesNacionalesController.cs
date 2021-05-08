using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (id == null)
            {
                //Insertar/Crear
                return View(obj);
            }

            //Update
            obj = await _pnRepo.GetAsync(SD.ParquesNacionalesAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));

            if (obj == null)
            {
                NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ParqueNacional obj)
        {
            if (ModelState.IsValid)
            {
                var ficheros = HttpContext.Request.Form.Files;
                if (ficheros.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = ficheros[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        obj.Imagen = p1;
                    }
                }
                else
                {
                    var objDeBD = await _pnRepo.GetAsync(SD.ParquesNacionalesAPIPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                    obj.Imagen = objDeBD.Imagen;
                }
                if (obj.Id == 0)
                {
                    await _pnRepo.CreateAsync(SD.ParquesNacionalesAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _pnRepo.UpdateAsync(SD.ParquesNacionalesAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllParquesNacionales()
        {
            return Json(new { data = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var estado = await _pnRepo.DeleteAsync(SD.ParquesNacionalesAPIPath, id, HttpContext.Session.GetString("JWToken"));
            if (estado)
            {
                return Json(new { success = true, message = "Eliminado satisfactoriamente" });
            }

            return Json(new { success = false, message = "Error al intentar eliminar el elemento" });
        }
    }
}
