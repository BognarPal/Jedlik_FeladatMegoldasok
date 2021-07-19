using pizzeria.data.interfaces.models;

namespace pizzeria.data.interfaces.operations
{
    public interface IAuthRepository : IGenericRepository<IUser>
    {
        IUser AuthenticateUser(IUser user);
    }
}
