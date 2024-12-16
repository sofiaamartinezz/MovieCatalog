using MovieCatalog.Identity;

namespace MovieCatalog.Models
{
    public class UserMovie
    {
        public string? UserId { get; set; }
        public MyUser? User { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
