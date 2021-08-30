using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.data.interfaces.models
{
    public interface IAuthenticate
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
