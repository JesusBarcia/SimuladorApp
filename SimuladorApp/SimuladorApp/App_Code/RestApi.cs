using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace SimuladorApp
{
    class RestApi
    {
        static public async Task<T> Get<T>(string Url)
        {
            try
            {
                var response = await App._globalhttpclient.GetAsync(Url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    var retorno = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring);
                    return retorno;
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }

        static public async Task<string> GetString(string Url)
        {
            try
            {
                if (App._globalhttpclient is null)
                {
                    App._globalhttpclient = new System.Net.Http.HttpClient();
                }
                var response = await App._globalhttpclient.GetAsync(Url);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    return jsonstring;
                }
                else
                {
                    return "ERROR: " + response.StatusCode.ToString();
                }
            }
            catch
            {
                return "ERROR AL ACCEDER A API " + Url;
            }
        }

    }
}
