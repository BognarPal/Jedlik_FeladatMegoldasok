using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.data.interfaces.models
{
    public interface ICustomer: IEntity
    {
        string Name { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Phone { get; set; }
        IAddress PrimaryAddress { get; set; }
        IEnumerable<IAddress> Addresses { get; set; }
    }
}
