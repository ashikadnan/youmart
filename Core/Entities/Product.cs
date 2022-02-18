using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
    }
}