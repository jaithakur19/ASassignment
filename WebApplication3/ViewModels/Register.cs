using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class Register
    {

        [Required(ErrorMessage = "Please enter your full name.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Only letters are allowed in the full name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your credit card information.")]
        [DataType(DataType.CreditCard, ErrorMessage = "Invalid credit card format.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Credit card must be exactly 16 numeric characters.")]
        public string Credit { get; set; }


        [Required(ErrorMessage = "Please select a gender.")]
        public string SelectedGender { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be exactly 8 digits.")]
        public string Phone { get; set; }


        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$", ErrorMessage = "Password must be at least 12 characters long and include at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string AboutMe { get; set;}

		[Required(ErrorMessage = "Photo is required")]
		[DataType(DataType.Upload)]
		[FileExtensions(Extensions = "jpg", ErrorMessage = "Only JPG files are allowed")]
		public string Photo { get; set; }
	}
}
