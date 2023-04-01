namespace MoviessApi.Dtos
{
    public class MovieDto
    {
        [MaxLength(255)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double rate { get; set; }

        [MaxLength(2500)]
        public string storyline { get; set; }

        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }

    }
}
