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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class SenderosController : ControllerBase
    {
        private ISenderoRepository _sRepository;

        private readonly IMapper _mapper;


        public SenderosController(ISenderoRepository sRepo, IMapper mapper)
        {
            _sRepository = sRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Devuelve una lista de senderos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SenderoDto>))]

        public IActionResult GetSenderos()
        {
            var objLista = _sRepository.GetSenderos();

            var objDto = new List<SenderoDto>();

            foreach (var obj in objLista)
            {
                objDto.Add(_mapper.Map<SenderoDto>(obj));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Devuelve el sendero que corresponde al parámetro Id
        /// </summary>
        /// <param name="senderoId"></param>
        /// <returns></returns>
        [HttpGet("{senderoId:int}", Name = "GetSendero")]
        [ProducesResponseType(200, Type = typeof(SenderoDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetSendero(int senderoId)
        {
            var obj = _sRepository.GetSendero(senderoId);

            if (obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<SenderoDto>(obj);
            return Ok(obj);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(SenderoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearSendero([FromBody] SenderoCreateDto senderoDto)
        {
            if (senderoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_sRepository.SenderoExiste(senderoDto.Nombre))
            {
                ModelState.AddModelError("", "El sendero ya existe!");
                return StatusCode(404, ModelState);
            }

            var senderoObj = _mapper.Map<Sendero>(senderoDto);

            if (!_sRepository.CrearSendero(senderoObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {senderoObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetSendero", new { senderoId = senderoObj.Id }, senderoObj);
        }

        [HttpPatch("{senderoId:int}", Name = "ActualizarSendero")]
        [ProducesResponseType(204)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarSendero(int senderoId, [FromBody] SenderoUpdateDto senderoDto)
        {
            if (senderoDto == null || senderoId != senderoDto.Id)
            {
                return BadRequest(ModelState);
            }

            var senderoObj = _mapper.Map<Sendero>(senderoDto);

            if (!_sRepository.UpdateSendero(senderoObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el registro {senderoObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{senderoId:int}", Name = "EliminarSendero")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminarSendero(int senderoId)
        {
            if (! _sRepository.SenderoExiste(senderoId))
            {
                return NotFound();
            }

            var senderoObj = _sRepository.GetSendero(senderoId);

            if (!_sRepository.DeleteSendero(senderoObj))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el registro {senderoObj.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
