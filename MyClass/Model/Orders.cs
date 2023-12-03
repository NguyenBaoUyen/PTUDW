using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Order")]
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên người nhận không để trống")]
        [Display(Name = "Tên người nhận")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Địa chỉ người nhận không để trống")]
        [Display(Name = "Địa chỉ người nhận")]
        public string ReceiverAdress { get; set; }

        [Required(ErrorMessage = "Email người nhận không để trống")]
        [Display(Name = "Email người nhận")]
        public string ReceiverEmail { get; set; }
        [Required(ErrorMessage = "Số điện thoại người nhận không để trống")]
        [Display(Name = "Số điện thoại người nhận")]
        public string ReceiverPhone { get; set; }
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }
        [Required(ErrorMessage = "Ngày tạo không để trống")]
        [Display(Name = "Ngày tạo")]
        public DateTime CreateAt { get; set; }
        [Required(ErrorMessage = "Người cập nhật không để trống")]
        [Display(Name = "Người cập nhật")]
        public int UpdateBy { get; set; }
        [Required(ErrorMessage = "Ngày cập nhật không để trống")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdateAt { get; set; }
        [Required(ErrorMessage = "Trạng thái không để trống")]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
    }
}
