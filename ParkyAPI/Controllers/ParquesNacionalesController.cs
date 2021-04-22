using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Modelos;
using ParkyAPI.Modelos.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParquesNacionalesController : ControllerBase
    {
        private IParqueNacionalRepository _pnRepository;

        private readonly IMapper _mapper;


        public ParquesNacionalesController(IParqueNacionalRepository pnRepo, IMapper mapper)
        {
            _pnRepository = pnRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Devuelve una lista de parques nacionales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetParquesNacionales()
        {
            var objLista = _pnRepository.GetParqueNacionales();

            var objDto = new List<ParqueNacionalDto>();

            foreach (var obj in objLista)
            {
                objDto.Add(_mapper.Map<ParqueNacionalDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Devuelve el parque nacional que corresponde al parámetro Id
        /// </summary>
        /// <param name="parqueNacionalId"></param>
        /// <returns></returns>
        [HttpGet("{parqueNacionalId:int}", Name = "GetParqueNacional")]
        public IActionResult GetParqueNacional(int parqueNacionalId)
        {
            var obj = _pnRepository.GetParqueNacional(parqueNacionalId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ParqueNacionalDto>(obj);
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult CrearParqueNacional([FromBody] ParqueNacionalDto parqueNacionalDto)
        {
            if (parqueNacionalDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_pnRepository.ParqueNacionalExiste(parqueNacionalDto.Nombre))
            {
                ModelState.AddModelError("", "El parque nacional ya existe!");
                return StatusCode(404, ModelState);
            }

            var parqueNacionalObj = _mapper.Map<ParqueNacional>(parqueNacionalDto);

            if (!_pnRepository.CrearParqueNacional(parqueNacionalObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {parqueNacionalObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetParqueNacional", new { parqueNacionalId = parqueNacionalObj.Id }, parqueNacionalObj);
        }

        [HttpPatch("{parqueNacionalId:int}", Name = "ActualizarParqueNacional")]
        public IActionResult ActualizarParqueNacional(int parqueNacionalId, [FromBody] ParqueNacionalDto parqueNacionalDto)
        {
            if (parqueNacionalDto == null || parqueNacionalId != parqueNacionalDto.Id)
            {
                return BadRequest(ModelState);
            }

            var parqueNacionalObj = _mapper.Map<ParqueNacional>(parqueNacionalDto);

            if (!_pnRepository.UpdateParqueNacional(parqueNacionalObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el registro {parqueNacionalObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{parqueNacionalId:int}", Name = "EliminarParqueNacional")]
        public IActionResult EliminarParqueNacional(int parqueNacionalId)
        {
            if (! _pnRepository.ParqueNacionalExiste(parqueNacionalId))
            {
                return NotFound();
            }

            var parqueNacionalObj = _pnRepository.GetParqueNacional(parqueNacionalId);

            if (!_pnRepository.DeleteParqueNacional(parqueNacionalObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el registro {parqueNacionalObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
