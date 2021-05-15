using System;
using System.Collections.Generic;
using System.Text;

namespace pizzeria.data.interfaces.models
{
    public interface IPizzaPrice: IEntity
    {
        IPizza Pizza { get; set; }
        DateTime FromDate { get; set; }
        DateTime? ToDate { get; set; }
        decimal Price { get; set; }
    }
}
