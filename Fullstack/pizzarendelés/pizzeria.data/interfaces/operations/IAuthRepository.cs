using pizzeria.data.interfaces.models;

namespace pizzeria.data.interfaces.operations
{
    public interface IAuthRepository
    {
        IUser AuthenticateUser(IAuthenticate model);
        IUser AuthenticateUser(IAuthenticate model, string jwtSecretString, int jwtValidityMinute);
    }
}
