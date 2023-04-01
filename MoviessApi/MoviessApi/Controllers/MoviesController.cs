using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviessApi.Models;

namespace MoviessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DbContainer _dbContainer;
        private new List<string> _allowedExtensions = new List<string>
        {
            ".jpg" , ".png"
        };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(DbContainer dbContainer)
        {
            _dbContainer = dbContainer;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAysnc()
        {
            var movies = await _dbContainer.Movies
                .Include(m=>m.Genre)
                .Select(m => new MovieDetailsDto
                {
                   Id =m.Id,
                   GenreId = m.GenreId,
                   GenreName=m.Genre.Name,
                   Poster=m.Poster,
                   rate=m.rate,
                   storyline=m.storyline,
                   Year=m.Year,
                   Title=m.Title
                })
                .OrderByDescending(a=>a.GenreId)
                .ToListAsync();
            return Ok(movies);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById( int id)
        {
            var data = await _dbContainer.Movies
                .Include(a => a.Genre)
                .Select(m => new MovieDetailsDto
                {
                    Id = m.Id,
                    GenreId = m.GenreId,
                    GenreName = m.Genre.Name,
                    Poster = m.Poster,
                    rate = m.rate,
                    storyline = m.storyline,
                    Year = m.Year,
                    Title = m.Title

                })
                .OrderByDescending(a => a.rate)
                .SingleOrDefaultAsync(a => a.Id == id);
            if (data == null)
                return NotFound();
            return Ok(data);    
        }
        [HttpGet("GetGenreIdAsync")]
        public async Task<IActionResult> GetbyGenreIdAsync(Byte genreId)
        {
            var movies = await _dbContainer.Movies
                .Where(m=>m.GenreId == genreId)
              .Include(m => m.Genre)
              .Select(m => new MovieDetailsDto
              {
                  Id = m.Id,
                  GenreId = m.GenreId,
                  GenreName = m.Genre.Name,
                  Poster = m.Poster,
                  rate = m.rate,
                  storyline = m.storyline,
                  Year = m.Year,
                  Title = m.Title

              })
              .OrderByDescending(a => a.GenreId)
              .ToListAsync();
            return Ok(movies);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {
            if (! _allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) 
               return BadRequest(" only jpg  and png are allowd");

            if (dto.Poster == null)
                return BadRequest("poster is required");
            if(dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1 m byte");

            var isValidGenre = await _dbContainer.Genres.AnyAsync(a => a.Id == dto.GenreId);

            if (!isValidGenre)
                return BadRequest("invalid Genre id");

            using var datastream =new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var movie = new Movie()
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = datastream.ToArray(),
                rate = dto.rate,
                storyline = dto.storyline,
                Year = dto.Year
            };
            await _dbContainer.AddAsync(movie);
            _dbContainer.SaveChanges();
            return Ok(movie);
        }
        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _dbContainer.Movies.FindAsync(id);

            if(data==null)

                return BadRequest($"THE  ID OF {id} is NOT FOUND");

            _dbContainer.Movies.Remove(data);

            _dbContainer.SaveChanges();
            
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsysnc(int id , [FromForm] MovieDto dto)
        {
            var movie = await _dbContainer.Movies.FindAsync(id);

            if(movie==null)
                return NotFound();

            var isValidGenre = await _dbContainer.Genres.AnyAsync(a => a.Id == dto.GenreId);
            if (!isValidGenre)
                return BadRequest("invalid Genre id");

            if(dto.Poster!=null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest(" only jpg  and png are allowd");


                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1 m byte");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);

                movie.Poster =datastream.ToArray();

            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.rate = dto.rate;
            movie.storyline = dto.storyline;
            movie.Year = dto.Year;

            _dbContainer.SaveChanges();
            return Ok(movie);
        }
    }
}
