using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.data.interfaces.models
{
    public interface ICustomer: IUser
    {
        IAddress PrimaryAddress { get; set; }
        IEnumerable<IAddress> Addresses { get; set; }
    }
}
