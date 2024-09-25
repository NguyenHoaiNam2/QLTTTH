using BaiThiwed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BaiThiwed.Controllers
{
    public class IndexAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IndexAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IdexAccount/IndexAccount
        public async Task<IActionResult> IndexAccount()
        {
            // Lấy tất cả các tài khoản từ cơ sở dữ liệu
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}
