using FindaCook.Maui.Models;
using FindaCook.Views;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
<<<<<<< HEAD
using Microsoft.Maui.Storage;
=======
>>>>>>> 817c5030796fd7c2983d6ceb52e742998fed081d

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

                        User user = await response.Content.ReadFromJsonAsync<User>();

<<<<<<< HEAD
                        Preferences.Set("UserEmail", user.UserEmail);

                        var userEmail = Preferences.Get("UserEmail", string.Empty);
                        /*if (!string.IsNullOrEmpty(userEmail))
                        {
                            // Use the userEmail for your API calls
                        }*/

                        // Ensure that the MainPage is set only in a Xamarin.Forms context
                        if (Application.Current != null && Application.Current.MainPage != null)
                        {
                            Application.Current.MainPage = new Personal_info();
=======
                        // Ensure that the MainPage is set only in a Xamarin.Forms context
                        if (Application.Current != null && Application.Current.MainPage != null)
                        {
                            Application.Current.MainPage = new HomePage();
>>>>>>> 817c5030796fd7c2983d6ceb52e742998fed081d
                        }

                        return user;
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
    }
}
