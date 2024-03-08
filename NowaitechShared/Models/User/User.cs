using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowaitechShared.Models.User
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public virtual UserType UserType { get; set; }

        [ForeignKey(nameof(UserType))]
        public int UserTypeId { get; set; }
    }
}
