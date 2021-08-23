using pizzeria.data.interfaces.models;

namespace pizzeria.data.interfaces.operations
{
    public interface IEmployeeRepository<T> : IGenericRepository<T> where T : class, IUser
    {

    }
}
