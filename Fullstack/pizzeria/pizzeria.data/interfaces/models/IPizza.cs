using System;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.models
{
    public interface IPizza: IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        List<byte[]> Pictures { get; set; }
        List<IPizzaTag> Tags { get; set; }
    }
}
