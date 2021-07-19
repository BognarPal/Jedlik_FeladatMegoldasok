using System.Collections.Generic;

namespace pizzeria.data.interfaces.models
{
    public interface IUser : IEntity
    {
        string Name { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Phone { get; set; }
        IEnumerable<IRole> Roles { get; set; }
    }
}
