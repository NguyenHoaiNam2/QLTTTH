using BaiThiwed.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaiThiwed.Models; // Thêm namespace của model Teacher nếu cần thiết

namespace BaiThiwed.Controllers
{
    public class QLTeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext vào controller
        public QLTeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách giáo viên (Read) với khả năng tìm kiếm
        public IActionResult TeacherIndex(string searchTerm)
        {
            var teachers = _context.Teachers.AsQueryable();

            // Nếu có từ khóa tìm kiếm, lọc danh sách giáo viên
            if (!string.IsNullOrEmpty(searchTerm))
            {
                teachers = teachers.Where(t => t.Name.Contains(searchTerm) || t.TeacherCode.Contains(searchTerm));
                ViewData["SearchTerm"] = searchTerm; // Để giữ lại từ khóa trong ô tìm kiếm
            }

            return View(teachers.ToList());
        }

        // GET: Hiển thị form để thêm giáo viên mới (Create)
        public IActionResult CreateTeacher()
        {
            return View();
        }

        // POST: Thêm giáo viên mới vào cơ sở dữ liệu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TeacherIndex));
            }
            return View(teacher);
        }

        // GET: Chi tiết giáo viên (Read)
        public async Task<IActionResult> Details(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // GET: Hiển thị form để chỉnh sửa giáo viên (Update)
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Cập nhật thông tin giáo viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TeacherIndex));
            }
            return View(teacher);
        }

        // GET: Hiển thị form để xác nhận xóa giáo viên (Delete)
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Xóa giáo viên khỏi cơ sở dữ liệu
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(TeacherIndex));
        }

        // Kiểm tra xem giáo viên có tồn tại hay không
        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
