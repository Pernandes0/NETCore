﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModel
{
    public class GetPersonVM
    {
        
        public string NIK { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public int Salary { get; set; }
        
        public string Email { get; set; }
        public enum Gender
        {
            Male,
            Female
        }
        
        public Gender gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
    }
}
