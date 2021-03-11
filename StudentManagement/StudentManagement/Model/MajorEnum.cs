using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Model
{
    public enum MajorEnum
    {
        [Display(Name = "未分配")]
        None,

        [Display(Name = "一年級")]
        FirstGrade,

        [Display(Name = "二年級")]
        SecondGrade,

        [Display(Name = "三年級")]
        GradeThree
    }
}
