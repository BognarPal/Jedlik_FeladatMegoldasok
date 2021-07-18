using System.Collections.Generic;

namespace pizzeria.data.interfaces.models
{
    public interface IPizza
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<byte[]> Pictures { get; set; }
        IEnumerable<IPizzaTag> Tags { get; set; }
        IEnumerable<IPizzaPrice> Prices { get; set; }
    }
}
