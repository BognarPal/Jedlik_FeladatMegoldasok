namespace pizzeria.data.interfaces.models
{
    public interface IEmployee : IEntity
    {
        string Name { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Phone { get; set; }
    }
}
