using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_profilling")]
    public class Profilling
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public int EducationId { get; set; }
        public virtual Account Account { get; set; }
        
        public virtual Education Education { get; set; }
    }
}
