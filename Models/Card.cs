using System.ComponentModel.DataAnnotations.Schema;

namespace SiimpleMvc.Models
{
    public class Card
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string IconName { get; set; }
        public string Title { get; set; }
        public string? ImageName { get; set; }
        [NotMapped]   
        public IFormFile Images { get; set; }   


    }
}
