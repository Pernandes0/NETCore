using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_accountrole")]
    public class AccountRole
    {
        [Required]
        public string AccountNIK { get; set; } // FK AccountNIK ke NIK account
        [Required]
        public int RoleId { get; set; } // FK RoleId ke Id di Role
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}
