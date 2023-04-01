namespace MoviessApi.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public int Year { get; set; }

        public double rate { get; set; }

        public string storyline { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        public string GenreName { get; set; }

    }
}
