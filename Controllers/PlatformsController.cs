using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase{
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public  ActionResult<IEnumerable<Platform>> GetAllPlatforms(){
            try{
                var platformItems =_repo.GetAllPlatforms();
                return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
            }
            catch (Exception ex){
                 return StatusCode(500, $"Internal server error: {ex.Message}");
            }
           
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(Guid id){
            try{
                var platformItem = _repo.GetPlatformById(id);
                if(platformItem ==null){
                    return NotFound();
                }
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            catch(Exception ex){
                 return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto dto){
            try{
                var model = _mapper.Map<Platform>(dto);
                _repo.CreatePlatform(model);
                _repo.SaveChanges();

                var readDto = _mapper.Map<PlatformReadDto>(model);
                return CreatedAtRoute(nameof(GetPlatformById), new {id = readDto.Id},readDto);
            }
            catch(Exception ex){
                return BadRequest("poka" + ex.Message);
            }
        }
    }
}