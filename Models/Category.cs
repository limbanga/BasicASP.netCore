using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Models
{
    public class Category
    {
        public long Id { get; set; }
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [MaxLength(28, ErrorMessage = "{0} không quá {1} ký tự.")]
        public required string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
