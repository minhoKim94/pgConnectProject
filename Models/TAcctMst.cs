namespace PGproject.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class TAcctMst
    {
        public byte SiteCode { get; set; }
        public int UserNo { get; set; }
        public long MstUserNo { get; set; }

        [Required(ErrorMessage = "ID is required.")]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\\W]).{8,20}$", 
            ErrorMessage = "Your password must be at least 8 characters long, contain at least one one lower case letter, one upper case letter, one digit and one special character, Valid special characters.")]
        public string UserPwd { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("UserPwd", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }
        public Nullable<byte> GenderCode { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        [Display(Name ="Email Address")]
        public string EmailAddr { get; set; }

        public string CountryCode { get; set; }
        public Nullable<System.DateTime> MemberRegDate { get; set; }
        public string UseFlag { get; set; }
        public System.DateTime RegDate { get; set; }
        public Nullable<System.DateTime> UpdDate { get; set; }

        public virtual TSiteCodeMgmt TSiteCodeMgmt { get; set; }
    }
}
