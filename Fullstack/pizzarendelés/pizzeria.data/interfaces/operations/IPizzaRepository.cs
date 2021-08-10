using pizzeria.data.interfaces.models;
using System;
using System.Collections.Generic;

namespace pizzeria.data.interfaces.operations
{
    public interface IPizzaRepository<T> : IGenericRepository<T> where T : class, IPizza
    {
        IEnumerable<T> GetByTags(IEnumerable<string> tags);
        T UpdatePrice(int pizzaId, DateTime fromDate, decimal newPrice);
        T RemoveLastPrice(int pizzaId);
        IEnumerable<IPizzaPrice> GetPrices(int pizzaId);
        decimal? GetPrice(int pizzaId, DateTime date);
    }
}
