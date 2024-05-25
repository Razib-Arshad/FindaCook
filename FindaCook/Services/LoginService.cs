using FindaCook.Maui.Models;
using FindaCook.Views;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace FindaCook.Services
{
    public class LoginService : ILoginRespository
    {
        public async Task<User> Login(string email, string password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the login URL
                    string apiUrl = "https://localhost:7224/api/UserCook/login";
                    client.BaseAddress = new Uri(apiUrl);

               
                    // Create a JSON object to send in the request body
                    var loginData = new
                    {
                        email = email,
                        password = password,
                        rememberMe = true
                    };

                    // Serialize the object to JSON
                    string jsonData = JsonSerializer.Serialize(loginData);

                    // Create a StringContent with the JSON data and set Content-Type
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the POST request with the JSON data in the body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string rawContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Raw Content: {rawContent}");

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(rawContent, options); 

                        if (loginResponse != null)
                        {
                            User user = loginResponse.User;

                            string token = loginResponse.Token;
                            string message = loginResponse.Message;

                            Preferences.Set("UserEmail", user.UserEmail);
                            Preferences.Set("UserID", user.Id);
                            Preferences.Set("UserName", user.UserName);
                            Preferences.Set("UserPassword", password);

                            Preferences.Set("Token", token);

                            return user;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error Status Code: {response.StatusCode}");
                        Console.WriteLine($"Error Reason Phrase: {response.ReasonPhrase}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        public async Task<bool> ResetPassword(string oldPassword, string newPassword)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the login URL
                    string apiUrl = "https://localhost:7224/api/UserCook/password/change";
                    client.BaseAddress = new Uri(apiUrl);

                    var email = Preferences.Get("UserEmail", string.Empty);

                    var requestData = new {
                        email=email,
                        oldPassword = oldPassword,
                        newPassword = newPassword
                    };

                    // Serialize the object to JSON format
                    string jsonData = JsonSerializer.Serialize(requestData);

                    // Create a StringContent with the JSON data and set Content-Type
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(apiUrl, content);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // Password reset was successful
                    }
                    else
                    {
                        return false;
                    }

                 }
            }
            catch (Exception ex)
            {

            }
            return true;// Placeholder, replace with actual logic
        }

        public async Task<bool> CheckUserRegisteredAsCookByEmail(string email)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:7224/api/UserCook/users/checkcookemail/{email}";
                    client.BaseAddress = new Uri(apiUrl);

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string rawContent = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };

                        var checkResponse = JsonSerializer.Deserialize<CheckCookResponse>(rawContent, options);

                        if (checkResponse != null && checkResponse.StatusCode == 200)
                        {
                            Preferences.Set("CookInfoId", checkResponse.CookInfoId);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return false;
        }

        public class CheckCookResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string CookInfoId { get; set; }
        }


    }
}
