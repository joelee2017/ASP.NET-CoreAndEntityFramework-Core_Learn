using System.ComponentModel.DataAnnotations;

namespace StudentManagementDataAccess.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage ="請輸入名字"), MaxLength(50, ErrorMessage = "名字的長度不能超過50个字")]
        public string Name { get; set; }
    
        [Display(Name = "主修科目")]
        [Required(ErrorMessage = "請選擇主修科目")]
        public MajorEnum? Major { get; set; }

        [Display(Name = "電子信箱")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "信箱的格式不正確")]
        [Required(ErrorMessage = "請輸入電子信箱")]
        public string Email { get; set; }
    }
}
