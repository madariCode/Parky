using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetParquesNacionales()
        {
            var objLista = _pnRepository.GetParqueNacionales();

            var objDto = new List<ParqueNacionalDto>();

            foreach(var obj in objLista)
            {
                objDto.Add(_mapper.Map<ParqueNacionalDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpGet("{parqueNacionalId:int}")]
        public IActionResult GetParqueNacional(int parqueNacionalId)
        {
            var obj = _pnRepository.GetParqueNacional(parqueNacionalId);

            if(obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<ParqueNacionalDto>(obj);
            return Ok(obj);
        }
    }
}
