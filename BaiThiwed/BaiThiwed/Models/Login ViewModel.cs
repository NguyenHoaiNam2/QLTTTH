using System.ComponentModel.DataAnnotations;

namespace BaiThiwed.Models
{
    public class Login_ViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
    }
}
