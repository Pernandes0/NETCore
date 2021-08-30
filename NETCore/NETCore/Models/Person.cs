using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NETCore.Models
{
    [Table("tb_m_person")]
    public class Person
    {
        [Key] 
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Email { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        [Required]
        public Gender gender { get; set; }
        public virtual Account Account { get; set; }
    }
}
