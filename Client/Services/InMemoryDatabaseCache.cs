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

        internal async Task GetCategoriesFromDatabaseAndCache ()
        {
            _categories = await _httpClient.GetFromJsonAsync<List<Category>>("endpoint");
        }

        internal event Action OnCategoriesDataChanged;
        private void NotifyCategroiesDataChanged() => OnCategoriesDataChanged?.Invoke();    
    }
}
