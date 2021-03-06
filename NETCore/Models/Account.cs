using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }
        [JsonIgnore]
        public virtual Profilling Profilling { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
