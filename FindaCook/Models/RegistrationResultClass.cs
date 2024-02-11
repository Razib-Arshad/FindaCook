using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindaCook.Maui.Models;

public class RegistrationResultClass
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

  
    public bool PasswordsMatch { get; set; }
    public bool EmailAlreadyExists { get; set; }

}
