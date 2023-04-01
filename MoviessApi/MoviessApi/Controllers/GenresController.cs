using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MoviessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly DbContainer _Context;

        public GenresController(DbContainer Context)
        {
            this._Context = Context;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genres = await _Context.Genres.FirstOrDefaultAsync(a=>a.Id==id);
            return Ok(genres);
        }

        [HttpGet]
        public async Task<IActionResult> GetAallAsync()
        {
            var genres = await _Context.Genres.OrderBy(g=>g.Name).ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto )
        {
            var genre = new Genre 
            {
                Name = dto.Name
            };
            await _Context.Genres.AddAsync(genre);
            _Context.SaveChanges();
    
            
            return Ok(genre);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsysnc(int id, [FromBody] CreateGenreDto  dto)
        {
            var genre = await _Context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"no genre was found with id {id}");

            genre.Name=dto.Name;
            _Context.SaveChanges();

            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteasync(int id)
        {
            var genre = await _Context.Genres.SingleOrDefaultAsync(g=>g.Id==id);
            if (genre == null)
                return NotFound($"no genre was found with id = {id}");
           
            _Context.Genres.Remove(genre);

            _Context.SaveChanges();
            return Ok(genre);


        }
    }
}