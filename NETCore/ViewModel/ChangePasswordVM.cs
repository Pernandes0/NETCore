using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.ViewModel
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public string Password { get; set; } //password dari email atau password terbaru sebelum diganti
        public string NewPassword { get; set; } //password baru
    }
}
