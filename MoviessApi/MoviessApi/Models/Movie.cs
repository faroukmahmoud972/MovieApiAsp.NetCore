namespace MoviessApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double rate { get; set; }

        [MaxLength(2500)]
        public string storyline { get; set; }

        public byte[] Poster{ get; set; }

        public byte GenreId { get; set; }

        public Genre  Genre{ get; set; }


    }
}
