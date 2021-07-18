using System;

namespace pizzeria.data.interfaces.models
{
    public interface IPizzaPrice: IEntity
    {
        DateTime FromDate { get; set; }
        DateTime? ToDate { get; set; }
        decimal Price { get; set; }
    }
}
