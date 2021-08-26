using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserTestApplication.Models;

namespace UserTestApplication.Services
{
    public class UserServices : IUserServices
    {
        public async Task<List<UserModel>> GetUsers()
        {
            var userModels = new List<UserModel>();

            try
            {
                string apiUrl = $"https://gist.githubusercontent.com/erni-ph-mobile-team/c5b401c4fad718da9038669250baff06/raw/7e390e8aa3f7da4c35b65b493fcbfea3da55eac9/test.json";
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                HttpClient client = new HttpClient();
                httpResponse = await client.GetAsync(apiUrl);

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = JsonConvert.DeserializeObject<List<UserModel>>(await httpResponse.Content.ReadAsStringAsync());

                    if (response != null)
                        userModels = response;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

            return userModels;
        }
    }
}
