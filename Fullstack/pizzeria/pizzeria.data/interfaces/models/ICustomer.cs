using System.Collections.Generic;

namespace pizzeria.data.interfaces.models
{
    public interface ICustomer: IEntity, IUser
    {
        IAddress PrimaryAddress { get; set; }
        List<IAddress> Addresses { get; set; }
        List<IOrder> PreviousOrders { get; set; }
    }
}