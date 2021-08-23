using pizzeria.data.interfaces.models;

namespace pizzeria.data.interfaces.operations
{
    public interface ICustomerRepository<T> : IGenericRepository<T> where T: class, ICustomer
    {

    }
}
