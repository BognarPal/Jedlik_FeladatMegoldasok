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
        private Address primaryAddress;

        public Address PrimaryAddress
        {
            get { return primaryAddress; }
            set { primaryAddress = value; }
        }

        IAddress ICustomer.PrimaryAddress
        {
            get { return primaryAddress; }
            set { primaryAddress = (Address)value; }
        }

        private IEnumerable<Address> addresses;

        public IEnumerable<Address> Addresses
        {
            get { return addresses; }
            set { addresses = value; }
        }

        IEnumerable<IAddress> ICustomer.Addresses
        {
            get { return addresses; }
            set { addresses = value.Select(v => (Address)v); }
        }

    }
}
