using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThesesWebApi.Dto;
using ThesesWebApi.Interfaces;
using ThesesWebApi.Models;

namespace ThesesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThesisController : Controller
    {
        private readonly IThesisRepository _thesisRepository;
        private readonly IMapper _mapper;

        public ThesisController(IThesisRepository thesisRepository, IMapper mapper)
        {
            _thesisRepository = thesisRepository;
            _mapper = mapper;
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetThesis(int Id)
        {
            if (!_thesisRepository.ThesisExists(Id))
                return NotFound();

            var thesis = _mapper.Map<Thesis>(_thesisRepository.GetThesis(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(thesis);
        }

        [HttpGet("all")]
        public IActionResult GetThesis()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Thesis, ThesisTable>()
            .ForMember("MainAuthor", opt => opt.MapFrom(t => t.MainAuthor.FirstName + " " + t.MainAuthor.LastName)));

            var mapper = new Mapper(config);

            var theses = mapper.Map<List<ThesisTable>>(_thesisRepository.GetAllTheses());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(theses);
        }

        [HttpPost]
        public IActionResult AddThesis([FromBody] ThesisDto addThesis)
        {
            if (addThesis == null)
                return BadRequest(ModelState);

            var thesis = _thesisRepository.GetAllTheses()
                .Where(c => c.Topic.Trim().ToUpper() == addThesis.Topic.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (thesis != null)
            {
                ModelState.AddModelError("", "Thesis already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var thesisMap = _mapper.Map<Thesis>(addThesis);
            thesisMap.Created = thesisMap.Updated = DateTime.Now;

            if (!_thesisRepository.Add(thesisMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{Id:int}")]
        public IActionResult UpdateThesis(int Id, [FromBody] ThesisDto newThesis)
        {
            if (newThesis == null)
                return BadRequest(ModelState);

            if (!_thesisRepository.ThesisExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_thesisRepository.Update(Id, newThesis))
            {
                ModelState.AddModelError("", "Something went wrong updating thesis");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public IActionResult DeleteThesis(int Id)
        {
            if (!_thesisRepository.ThesisExists(Id))
                return NotFound();

            var thesisToDelete = _thesisRepository.GetThesis(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_thesisRepository.Delete(thesisToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting thesis");
            }

            return NoContent();
        }
    }
}
