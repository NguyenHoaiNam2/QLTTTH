namespace BaiThiwed.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string TeacherCode { get; set; } // Mã số giáo viên
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
