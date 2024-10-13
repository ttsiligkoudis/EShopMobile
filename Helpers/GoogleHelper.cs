
using Client;
using DataModels.Dtos;
using Enums;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Helpers
{
    public static class GoogleHelper
    {
        public static async Task<(CustomerDto,UserDto)> GetSSOResponse(ClaimsIdentity identity, string accessToken, IClient client)
        {
            UserDto user = null;
            CustomerDto customer = null;

            var claims = identity.Claims
                .Select(claims => new
                {
                    claims.Type,
                    claims.Value
                });

            var schema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
            var customerName = identity.Name;
            var customerEmail = claims.FirstOrDefault(w => w.Type == schema + "emailaddress")?.Value;
            var id = claims.FirstOrDefault(w => w.Type == schema + "nameidentifier")?.Value;

            user = await client.GetAsync<UserDto>("Users/GetUserByEmail/?email=" + customerEmail);

            if (user == null)
            {
                var password = Guid.NewGuid().ToString()[..6];
                user = new UserDto
                {
                    Email = customerEmail,
                    Password = password,
                    RegDate = DateTime.UtcNow,
                    UserType = UserType.User,
                    LoginDate = DateTime.UtcNow
                };

                (var city, var address) = await GetPersonFields(id, accessToken);

                user = await client.PostAsync(user, "Users");
                user.Password = password;

                customer = new CustomerDto
                {
                    Name = customerName,
                    Email = customerEmail,
                    RegDate = user.RegDate,
                    UserId = user.Id,
                    Address = address,
                    City = city
                };

                customer = await client.PostAsync(customer, "Customers");

                var message = new MessageDto
                {
                    Email = user.Email,
                    Subject = "New User Created",
                    Body = EmailHelper.NewUserCreatedHtml(customer, user, "New User Created")
                };

                await client.PostAsync(message, $"Messages/SendMessage");
            }
            else
            {
                user.LoginDate = DateTime.UtcNow;
                await client.PutAsync(user, $"Users/{user.Id}");
                customer = await client.GetAsync<CustomerDto>($"Customers/User/{user.Id}");
            }

            return (customer, user);
        }

        public static async Task<(string, string)> GetPersonFields(string id, string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var personFields = "addresses,ageRanges,biographies,birthdays,clientData,coverPhotos,emailAddresses,externalIds,genders,locales,locations,memberships,names,nicknames,occupations,organizations,phoneNumbers,photos,relations,sipAddresses,skills,urls,userDefined";
            var response = await httpClient.GetAsync($"https://people.googleapis.com/v1/people/{id}?personFields={personFields}");
            var content = await response.Content.ReadAsStringAsync();

            var city = string.Empty;
            var address = string.Empty;
            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
            {
                dynamic data = JsonConvert.DeserializeObject(content);

                city = data?.locations != null ? data.locations[0].value : string.Empty;
                address = data?.addresses != null ? data.addresses[0].formattedValue : string.Empty;
            }
            return (city, address);
        }
    }
}
