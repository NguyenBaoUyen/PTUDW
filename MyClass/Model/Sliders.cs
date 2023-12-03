using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Sliders")]
    public class Sliders
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên slider không để trống")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Đường dẫn")]
        public string Url { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Img{ get; set; }
        [Display(Name = "Sắp xếp")]
        public int? Order { get; set; }
        [Display(Name = "Chức vụ")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Người tạo không để trống")]
        [Display(Name = "Người tạo")]
        public int CreateBy { get; set; }
        [Required(ErrorMessage = "Ngày tạo không để trống")]
        [Display(Name = "Ngày tạo")]
        public DateTime CreateAt { get; set; }
       
        [Display(Name = "Người cập nhật")]
        public int? UpdateBy { get; set; }
        
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdateAt { get; set; }
        [Required(ErrorMessage = "Trạng thái không để trống")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}
