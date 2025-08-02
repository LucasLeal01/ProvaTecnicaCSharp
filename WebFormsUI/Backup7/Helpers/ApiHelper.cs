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
        private static readonly string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "http://localhost:5000/api";
        private static readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        private static string SerializeToCamelCase(object obj)
        {
            var serializer = new JavaScriptSerializer();
            var dict = new Dictionary<string, object>();
            
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    var camelCaseName = char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);
                    
                    if (value is DateTime dateTime)
                    {
                        dict[camelCaseName] = dateTime.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dict[camelCaseName] = value;
                    }
                }
            }
            
            return serializer.Serialize(dict);
        }

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var url = $"{baseUrl}/{endpoint}";
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    return jsonSerializer.Deserialize<T>(json);
                }
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
                using (var httpClient = CreateHttpClient())
                {
                    var json = SerializeToCamelCase(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = $"{baseUrl}/{endpoint}";
                    
                    var response = await httpClient.PostAsync(url, content);
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"HTTP {response.StatusCode}: {errorContent}");
                    }
                    
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return jsonSerializer.Deserialize<T>(responseJson);
                }
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
                using (var httpClient = CreateHttpClient())
                {
                    var json = SerializeToCamelCase(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = $"{baseUrl}/{endpoint}";
                    var response = await httpClient.PutAsync(url, content);
                    response.EnsureSuccessStatusCode();
                }
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
                using (var httpClient = CreateHttpClient())
                {
                    var url = $"{baseUrl}/{endpoint}";
                    var response = await httpClient.DeleteAsync(url);
                    response.EnsureSuccessStatusCode();
                }
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
                using (var httpClient = CreateHttpClient())
                {
                    var url = $"{baseUrl}/{endpoint}";
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao baixar PDF de {0}: {1}", endpoint, ex.Message));
            }
        }
    }
}

