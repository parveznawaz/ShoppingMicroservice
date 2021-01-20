using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Infrastructure;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.ApiCollection.Models;
using AspnetRunBasics.Settings;
using Newtonsoft.Json;

namespace AspnetRunBasics.ApiCollection
{
    public class CatalogApi: BaseHttpClientWithFactory, ICatalogApi
    {
        private readonly IApiSettings _settings;


        public CatalogApi(IHttpClientFactory factory, IApiSettings settings) : base(factory)
        {
            _settings = settings;
        }
        
        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCatagory(string catagory)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath("GetProductByCategory")
                .AddToPath(catagory)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<IEnumerable<CatalogModel>>(message);
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath(id)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<CatalogModel>(message);
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<CatalogModel>(message);
        }
    }
}
