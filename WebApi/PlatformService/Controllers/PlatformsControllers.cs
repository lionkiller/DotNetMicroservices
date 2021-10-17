using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using System;
using System.Collections.Generic;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")] //Controller name in the Route is derived from class name Prefix (Platforms)
    [ApiController]
    public class PlatformsControllers : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformsControllers(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("***** Getting Platforms...");

            var platforms = _platformRepo.GetAllPlatforms();
            var dtos = _mapper.Map<IEnumerable<PlatformReadDTO>>(platforms);

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = nameof(GetPlatformById))]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id)
        {
            Console.WriteLine($"***** Trying to get an id({id}) specific Platform...");

            var platformDto = _platformRepo.GetPlatformById(id);

            if (platformDto != null)
            {

                return Ok(_mapper.Map<PlatformReadDTO>(platformDto));
            }

            return NotFound();
        }

        [HttpPost(Name = nameof(CreatePlatform))]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public ActionResult<PlatformReadDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            Console.WriteLine($"***** Creating a new Platform...");

            var platform = _mapper.Map<Platform>(platformCreateDTO);

            _platformRepo.CreatePlatform(platform);

            if (_platformRepo.SaveChanges())
            {
                return new CreatedAtRouteResult(nameof(GetPlatformById), new { Id = platform.Id },
                                                _mapper.Map<PlatformReadDTO>(platform));
            }

            return StatusCode(500);
        }
    }
}
