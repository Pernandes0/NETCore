using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        public int UniversityId { get; set; }
       
        public virtual ICollection<Profilling> Profillings { get; set; }
       
        public virtual University University { get; set; }
    }
}
