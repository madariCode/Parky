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
    [Route("api/v{version:apiVersion}/parquesnacionales")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecParquesNacionales")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ParquesNacionalesV2Controller : ControllerBase
    {
        private IParqueNacionalRepository _pnRepository;

        private readonly IMapper _mapper;


        public ParquesNacionalesV2Controller(IParqueNacionalRepository pnRepo, IMapper mapper)
        {
            _pnRepository = pnRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ParqueNacionalDto>))]

        public IActionResult GetParquesNacionales()
        {
            var obj = _pnRepository.GetParqueNacionales().FirstOrDefault();

            return Ok(_mapper.Map<ParqueNacionalDto>(obj));
        }

    }
}
