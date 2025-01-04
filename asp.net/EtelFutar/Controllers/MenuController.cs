using EtelFutar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EtelFutar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        // GET: api/menu
        [HttpGet]
        public IEnumerable<MenuModel>? Get()
        {

            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);
        }

        // GET api/menu/3
        [HttpGet("{id}")]
        public MenuModel? Get(int id)
        {
            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            var menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);
            return menuItems?.SingleOrDefault(m => m.Id == id); 
        }

        /*
        // POST api/menu
        [HttpPost]
        public MenuModel? Post([FromBody] MenuModel value)
        {
            //Feltételezzük a JSON fájl létezik és nem üres
            //TODO: kezelni kellene, ha nem létezik a fájl
            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            var menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);

            var nextID = menuItems?.Max(m => m.Id) + 1;
            value.Id = nextID ?? 1;
            menuItems?.Add(value);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(menuItems);
            System.IO.File.WriteAllText(@"Data\menu.json", json);

            return Get(value.Id);
        }
        */

        // POST api/menu
        [HttpPost]
        public MenuModel? Post([FromBody] dynamic value)
        {
            MenuModel? newItem;
            try
            {
                newItem = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuModel>(value.ToString());
                if (newItem == null)
                    throw new Exception("Invalid JSON");
            }
            catch (Exception ex)
            {
                throw;
            }

            //Feltételezzük a JSON fájl létezik és nem üres
            //TODO: kezelni kellene, ha nem létezik a fájl
            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            var menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);

            var nextID = menuItems?.Max(m => m.Id) + 1;
            newItem.Id = nextID ?? 1;
            menuItems?.Add(newItem);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(menuItems);
            System.IO.File.WriteAllText(@"Data\menu.json", json);

            return Get(newItem.Id);
        }

        // PUT api/menu/5
        [HttpPut("{id}")]
        public MenuModel? Put(int id, [FromBody] dynamic value)
        {
            MenuModel? item;
            try
            {
                item = Newtonsoft.Json.JsonConvert.DeserializeObject<MenuModel>(value.ToString());
                if (item == null)
                    throw new Exception("Invalid JSON");
                if (item.Id != id)
                    throw new Exception("Invalid ID");
            }
            catch (Exception ex)
            {
                throw;
            }

            //Feltételezzük a JSON fájl létezik és nem üres
            //TODO: kezelni kellene, ha nem létezik a fájl
            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            var menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);

            var itemToChange = menuItems?.SingleOrDefault(m => m.Id == id);
            if (itemToChange == null)
                throw new Exception("Item not found");

            int index = menuItems.IndexOf(itemToChange);
            menuItems[index] = item;

            json = Newtonsoft.Json.JsonConvert.SerializeObject(menuItems);
            System.IO.File.WriteAllText(@"Data\menu.json", json);

            return Get(id);
        }

        // DELETE api/menu/3
        [HttpDelete("{id}")]
        public MenuModel? Delete(int id)
        {
            //Feltételezzük a JSON fájl létezik és nem üres
            //TODO: kezelni kellene, ha nem létezik a fájl
            var json = System.IO.File.ReadAllText(@"Data\menu.json");
            var menuItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuModel>>(json);

            var itemToDelete = menuItems?.SingleOrDefault(m => m.Id == id);
            if (itemToDelete == null)
                throw new Exception("Item not found");

            menuItems?.Remove(itemToDelete);

            json = Newtonsoft.Json.JsonConvert.SerializeObject(menuItems);
            System.IO.File.WriteAllText(@"Data\menu.json", json);

            return itemToDelete;
        }
    }
}
