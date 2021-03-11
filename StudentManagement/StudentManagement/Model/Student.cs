using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Model
{
    public class Student
    {
        [BindNever]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required, MaxLength(50, ErrorMessage = "名字的長度不能超過50个字")]
        public string Name { get; set; }

        public MajorEnum Major { get; set; }

        [Display(Name = "電子郵件")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "信箱的格式不正確")]
        [Required]
        public string Email { get; set; }
    }
}
