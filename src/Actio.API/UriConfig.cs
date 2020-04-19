using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.API
{
    public class UrlsConfig
    {
        public class ActivityOperations
        {
            public static string GetItemById(int id) => $"/api/v1/catalog/items/{id}";
            public static string GetItemsById(IEnumerable<int> ids) => $"/api/v1/catalog/items?ids={string.Join(',', ids)}";
        }

        public class UserOperations
        {
            public static string GetItemById(string id) => $"/api/v1/basket/{id}";
            public static string UpdateBasket() => "/api/v1/basket";
        }

        
        public string Basket { get; set; }
        public string Catalog { get; set; }
        public string Orders { get; set; }
    }
}
