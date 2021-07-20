using pizzeria.data.interfaces.models;

namespace pizzeria.data.interfaces.operations
{
    public interface IAuthRepository<T> : IGenericRepository<T> where T : class, IUser
    {
        T AuthenticateUser(T user);
    }
}
