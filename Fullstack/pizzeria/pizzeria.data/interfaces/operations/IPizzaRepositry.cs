using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.operations
{
    interface IPizzaRepositry: IGenericRepository<IPizza>
    {
        List<IPizza> GetByTags(IEnumerable<IPizzaTag> tags);
        IPizza UpdatePrice(int pizzaId, DateTime fromDate, decimal newPrice);
        IPizza RemoveLastPrice(int pizzaId);
        List<IPizzaPrice> GetPrices(int pizzaId);
        decimal? GetPrice(int pizzaId, DateTime date);
    }
}
