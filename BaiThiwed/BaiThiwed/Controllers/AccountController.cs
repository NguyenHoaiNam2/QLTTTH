using BaiThiwed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiThiwed.Data;


namespace BaiThiwed.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash password before saving
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                var user = new User
                {
                    Username = model.Username,
                    Password = passwordHash,
                    Email = model.Email
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to Login
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tìm user trong cơ sở dữ liệu dựa trên tên đăng nhập
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

                // Kiểm tra xem người dùng có tồn tại và mật khẩu có đúng không
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    // Đăng nhập thành công, chuyển hướng đến trang chủ hoặc trang mong muốn
                    return RedirectToAction("Index", "Home");
                }

                // Nếu sai thông tin đăng nhập, thêm thông báo lỗi
                ModelState.AddModelError(string.Empty, "Sai tài khoản hoặc mật khẩu, vui lòng nhập lại.");
            }

            // Nếu có lỗi, trả về view với thông báo lỗi
            return View(model);
        }


        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Xử lý đăng xuất (xóa cookie, session, etc.)
            return RedirectToAction("Login");
        }
    }
}
