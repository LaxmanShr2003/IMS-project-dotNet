using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IMS.web.Models
{
    public class ApplicationUser: IdentityUser
    {

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int StoreId { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int UserRoleId { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePicture {  get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ModifiedBy { get; set; }
        






    }
}
