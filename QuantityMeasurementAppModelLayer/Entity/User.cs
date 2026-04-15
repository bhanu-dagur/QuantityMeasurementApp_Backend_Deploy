using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementAppModelLayer.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    }
}