using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzeria.service.models
{
    public class Customer : User, ICustomer
    {
        public Address PrimaryAddress { get; set; }
        IAddress ICustomer.PrimaryAddress
        {
            get => PrimaryAddress;
            set => PrimaryAddress = (Address)value;
        }

        public IEnumerable<Address> Addresses { get; set; }
        IEnumerable<IAddress> ICustomer.Addresses
        {
            get => Addresses; 
            set => Addresses = value.Select(v => (Address)v); 
        }

    }
}
