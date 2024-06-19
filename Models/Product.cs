using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ClothesStore.Models
{
    public class Product
    {
        public long Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [MaxLength(50, ErrorMessage = "{0} không quá {1} ký tự.")]
        public required string Name { get; set; }
        [Display(Name = "Mô tả sản phẩm")]
        [StringLength(255, ErrorMessage = "{0} không quá {1} ký tự.")]
        public string? Description { get; set; }
        [Display(Name = "Giá sản phẩm")]
        [Required(ErrorMessage = "{0} không được bỏ trống.")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} không nhỏ hơn {1}.")]
        public double Price { get; set; } = 0;

        [Display(Name = "Hình ảnh")]
        [StringLength(255)]
        public string? ImageUrl { get; set; }

        public string PriceFormated
        {
            get
            {
                return string.Format("{0:#,##0}", Price);
            }
        }
    }
}