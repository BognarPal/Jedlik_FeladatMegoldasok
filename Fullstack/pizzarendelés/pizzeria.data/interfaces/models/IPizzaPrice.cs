using System;

namespace pizzeria.data.interfaces.models
{
    public interface IPizzaPrice
    {
        public int Id { get; set; }
        IPizza Pizza { get; set; }
        DateTime FromDate { get; set; }
        DateTime? ToDate { get; set; }
        decimal Price { get; set; }
    }
}
