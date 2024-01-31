using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.Model
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }

        
        public string Credit { get; set; }


        
        public string SelectedGender { get; set; }

        
        public string Phone { get; set; }


       
        public string Address { get; set; }

        		
		public string AboutMe { get; set; }

        public string Photo { get; set; }

		public string? AuthenticationToken { get; set; }

	}
		
}
