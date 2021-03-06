using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_profilling")]
    public class Profilling
    {
        [Key]
        //[StringLength(16, ErrorMessage = "Anda memasukan lebih dari 16 angka")]
        public string NIK { get; set; }
        public int EducationId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
    }
}
