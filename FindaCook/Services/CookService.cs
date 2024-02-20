using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FindaCook.Maui.Models;
using FindaCook.Models;
using Newtonsoft.Json;



namespace FindaCook.Services
{
    public class CookService : ICookRespository
    {

        public async Task<ICollection<CookProfile>> GetCookByCategory(string cat)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the URL for getting cooks by category
                    string apiUrl = $"https://localhost:7224/api/UserCook/search/bycategory?category={cat}";

                    // Send the GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {

                        string contentString = await response.Content.ReadAsStringAsync();
                        var dataContainer = JsonConvert.DeserializeObject<DataContainer>(contentString);

                        List<CookProfile> cooks = dataContainer.Data;

                        


                        return cooks;
                    }
                    else
                    {
                        // If the request was not successful, handle the error
                        var errorResponse = new
                        {
                            StatusCode = (int)response.StatusCode,
                            Message = "An error occurred while retrieving cooks",
                            ErrorDetails = await response.Content.ReadAsStringAsync()
                        };

                        throw new Exception(JsonConvert.SerializeObject(errorResponse));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<RegistrationResultClass> RegisterCook(Person p, QualificationInfo q, ProfessionalInfoModel prof)
        {
            try
            {
              

                using (var client = new HttpClient())
                {
                    // Construct the registration URL
                    string apiUrl = "https://localhost:7224/api/UserCook/RegisterCook";
                    client.BaseAddress = new Uri(apiUrl);


                    var Email = Preferences.Get("UserEmail", string.Empty);
                    var Password = Preferences.Get("UserPassword", string.Empty);

                    // Create a JSON object to send in the request body
                    var registrationCookData = new
                    {
                        Email = Email,
                        UserPassword= Password,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        ContactNumber = p.ContactNumber,
                        WhatsappNumber = p.WhatsappNumber,
                        CurrentAddress = p.CurrentAddress,
                        PermanentAddress = p.PermanentAddress,
                        EligibleToWork = p.EligibleToWork,
                        HasCulinaryDegree = !string.IsNullOrEmpty(q.Degree), // Check if the degree is provided
                        Degree = q.Degree,
                        Certificates = q.Certificates,
                        CulinarySchoolName = q.SchoolName,
                        ExperienceYears = prof.Experience,
                        SkillsAndSpecialties = prof.Skills,
                        SignatureDishes = prof.SignatureDishes,
                        ServicesProvided = prof.Services,
                    };
                    
                    // Serialize the object to JSON
                    string jsonData = System.Text.Json.JsonSerializer.Serialize(registrationCookData);

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

        public async Task<bool> SendOrder(Orders order)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the order sending URL
                    string apiUrl = "https://localhost:7224/api/Order/SendOrder";
                    client.BaseAddress = new Uri(apiUrl);

        

                    // Create a JSON object to send in the request body
                    var orderData = new
                    {
                        Service = order.SelectedService,
                        Description = order.Description,
                        ContactNumber = order.ContactNumber,
                        Address = order.Address,
                        Price = order.Price,
                        Date = order.SelectedDate.Add(order.SelectedTime), // Combine date and time
                    };

                    // Serialize the object to JSON
                    string jsonData = System.Text.Json.JsonSerializer.Serialize(orderData);

                    // Create a StringContent with the JSON data and set Content-Type
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Send the POST request with the JSON data in the body
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the order sending was successful
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle network-related errors
                // Log the exception or perform additional actions
                return false;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or perform additional actions
                return false;
            }
        }

    }
}
