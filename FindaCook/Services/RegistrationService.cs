using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using FindaCook.Maui.Models;

namespace FindaCook.Services
{
    public class RegistrationService : IRegistrationRepository
    {
        public async Task<RegistrationResultClass> Register(string name, string email, string password, string retypePassword)
        {
            try
            {
                // Validate passwords match
                if (password != retypePassword)
                {
                    return new RegistrationResultClass { Success = false, ErrorMessage = "Passwords do not match." };
                }

                using (var client = new HttpClient())
                {
                    // Construct the registration URL
                    string apiUrl = "https://localhost:7224/api/User/Register";
                    client.BaseAddress = new Uri(apiUrl);

                    // Create a JSON object to send in the request body
                    var registrationData = new
                    {
                        UserName = name,
                        UserEmail = email,
                        UserPassword = password,
                        ConfirmPassword = retypePassword
                    };

                    // Serialize the object to JSON
                    string jsonData = JsonSerializer.Serialize(registrationData);

                    // Create a StringContent with the JSON data and set Content-Type
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the POST request with the JSON data in the body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Registration successful
                        return new RegistrationResultClass { Success = true };
                    }
                    else
                    {
                        // Registration failed, parse the error message from the response
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        return new RegistrationResultClass { Success = false, ErrorMessage = errorMessage };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
               
                return new RegistrationResultClass { Success = false, ErrorMessage = $"Network error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or perform additional actions
                return new RegistrationResultClass { Success = false, ErrorMessage = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
