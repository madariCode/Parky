using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class SenderosController : Controller
    {
        private readonly IParqueNacionalRepository _pnRepo;
        private readonly ISenderoRepository _sRepo;

        public SenderosController(IParqueNacionalRepository pnRepo, ISenderoRepository sRepo)
        {
            _pnRepo = pnRepo;
            _sRepo = sRepo;
        }

        public IActionResult Index()
        {
            return View(new Sendero());
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<ParqueNacional> pnLista = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath);

            SenderosVM objVM = new SenderosVM()
            {
                ParqueNacionalLista = pnLista.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Id.ToString()
                }),
                Sendero = new Sendero()
            };

            if (id == null)
            {
                //Insertar/Crear
                return View(objVM);
            }

            //Update
            objVM.Sendero = await _sRepo.GetAsync(SD.SenderosAPIPath, id.GetValueOrDefault());

            if (objVM == null)
            {
                NotFound();
            }
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SenderosVM obj)
        {
            if (ModelState.IsValid)
            {                
                if (obj.Sendero.Id == 0)
                {
                    await _sRepo.CreateAsync(SD.SenderosAPIPath, obj.Sendero);
                }
                else
                {
                    await _sRepo.UpdateAsync(SD.SenderosAPIPath + obj.Sendero.Id, obj.Sendero);

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<ParqueNacional> pnLista = await _pnRepo.GetAllAsync(SD.ParquesNacionalesAPIPath);

                SenderosVM objVM = new SenderosVM()
                {
                    ParqueNacionalLista = pnLista.Select(i => new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Id.ToString()
                    }),
                    Sendero = obj.Sendero
                };

                return View(objVM);
            }
        }

        public async Task<IActionResult> GetAllSenderos()
        {
            return Json(new { data = await _sRepo.GetAllAsync(SD.SenderosAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var estado = await _sRepo.DeleteAsync(SD.SenderosAPIPath, id);
            if (estado)
            {
                return Json(new { success = true, message = "Eliminado satisfactoriamente" });
            }

            return Json(new { success = false, message = "Error al intentar eliminar el elemento" });
        }
    }
}
