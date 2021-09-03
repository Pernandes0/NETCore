using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_person")]
    public class Person
    {
        [Key]
        // [StringLength(16, ErrorMessage = "Anda memasukan lebih dari 16 angka")]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [Range(0, 20000000)]
        public int Salary { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [Required]
        public Gender gender { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
}
