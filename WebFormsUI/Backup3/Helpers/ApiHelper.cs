using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WebFormsUI.Helpers
{
    public static class ApiHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "http://localhost:5000/api";
        private static readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        static ApiHelper()
        {
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return jsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao fazer GET para {0}: {1}", endpoint, ex.Message));
            }
        }

        public static async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = jsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                return jsonSerializer.Deserialize<T>(responseJson);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao fazer POST para {0}: {1}", endpoint, ex.Message));
            }
        }

        public static async Task PutAsync(string endpoint, object data)
        {
            try
            {
                var json = jsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao fazer PUT para {0}: {1}", endpoint, ex.Message));
            }
        }

        public static async Task DeleteAsync(string endpoint)
        {
            try
            {
                var response = await httpClient.DeleteAsync(endpoint);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao fazer DELETE para {0}: {1}", endpoint, ex.Message));
            }
        }

        public static async Task<byte[]> GetPdfAsync(string endpoint)
        {
            try
            {
                var response = await httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao baixar PDF de {0}: {1}", endpoint, ex.Message));
            }
        }
    }
}

