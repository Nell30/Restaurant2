using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Review
    {
        [Key]
        public string? Id { get; set; }

        public string? userEmail { get; set; }

        public string comments { get; set; }

    }
}
