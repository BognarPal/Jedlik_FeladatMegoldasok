namespace pizzeria.data.interfaces.models
{
    public interface IAddress: IEntity
    {
        string City { get; set; }
        string StreetAndHouseNumber { get; set; }
        string FloorAndDoor { get; set; }
        string Comment { get; set; }
    }
}