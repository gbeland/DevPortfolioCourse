using Client.Static;
using Shared.Models;
using System.Net.Http.Json;

namespace Client.Services
{
    internal sealed class InMemoryDatabaseCache
    {
        private readonly HttpClient _httpClient = null;

        public InMemoryDatabaseCache(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private List<Category> _categories;

        internal List<Category> Categories
        {
            get { return _categories; } 

            set 
            { 
                _categories = value;

                NotifyCategroiesDataChanged();
            }

        }

        private bool _gettingGetCategoriesFromDatabaseAndCaching = false;
        internal async Task GetCategoriesFromDatabaseAndCache ()
        {
            // Only allow one Get request at a time
            if (_gettingGetCategoriesFromDatabaseAndCaching == false)
            {
                _gettingGetCategoriesFromDatabaseAndCaching = true;
                _categories = await _httpClient.GetFromJsonAsync<List<Category>>(APIEndpoints.s_categories);
                _gettingGetCategoriesFromDatabaseAndCaching = false;
            }
        }

        internal event Action OnCategoriesDataChanged;
        private void NotifyCategroiesDataChanged() => OnCategoriesDataChanged?.Invoke();    
    }
}
