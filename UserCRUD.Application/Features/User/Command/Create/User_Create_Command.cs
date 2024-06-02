using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserCRUD.Application.Models.DTOs.User;

namespace UserCRUD.Application.Features.User.Command.Create
{
    public class User_Create_Command : IRequest<User_Create_Response_Dto>
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string LastName { get; set; }

        [Display(Name = "کد پرسنلی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MinLength(4, ErrorMessage = "کد پرسنلی حداقل 4 رقم میبایست داشته باشد")]
        [MaxLength(10, ErrorMessage = "کد پرسنلی حداکثر 10 رقم میبایست داشته باشد")]
        public string PersonalCode { get; set; }

        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]

        public string NationalCode { get; set; }
        [Display(Name = "فایل های مدارک تحصیلی")]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public IEnumerable<IFormFile> MultipleFiles { get; set; }
    }
}
