using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FindaCook.Maui.Models;
using FindaCook.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;




namespace FindaCook.Services
{
    public class CookService : ICookRespository
    {
        public async Task<bool> AddToFavorites(string cookName, CookProfile cookProfile)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://localhost:7224/api/Favourites/favourite/post";

                    var userId = Preferences.Get("UserID", string.Empty);

                    var favouriteData = new
                    {
                        userId = userId,
                        cookInfoId = cookProfile.Id
                    };


                    string jsonData = System.Text.Json.JsonSerializer.Serialize(favouriteData);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or perform additional actions
                return false;
            }
        }


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



        public async Task<ICollection<FavouriteCookDetails>> getFavourites()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var id = Preferences.Get("UserID", string.Empty);
                    string apiUrl = $"https://localhost:7224/api/Favourites/favourites/user/{id}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentString = await response.Content.ReadAsStringAsync();
                        var responseContainer = JsonConvert.DeserializeObject<ResponseContainer>(contentString);

                        var favouriteCooks = JsonConvert.DeserializeObject<List<FavouriteCookDetails>>(JsonConvert.SerializeObject(responseContainer.Data));
                        return favouriteCooks;
                    }
                    else
                    {
                        var errorResponse = new
                        {
                            StatusCode = (int)response.StatusCode,
                            Message = "An error occurred while retrieving favorites",
                            ErrorDetails = await response.Content.ReadAsStringAsync()
                        };

                        throw new Exception(JsonConvert.SerializeObject(errorResponse));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public class ResponseContainer
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public List<FavouriteCookDetails> Data { get; set; }
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
                        Id ="null",
                        Email = Email,
                        UserPassword = Password,
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

        public async Task<bool> SendOrder(Orders order, string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = "https://localhost:7224";
                    string apiEndpoint = "/api/OrderRequests/request/post";
                    client.BaseAddress = new Uri(baseUrl);

                    var UserId = Preferences.Get("UserID", string.Empty);

                    // Assuming SelectedDate is a DateTime that includes the date and SelectedTime is a TimeSpan
                    DateTime fullDateTime = order.SelectedDate.Add(order.SelectedTime);

                    var orderData = new
                    {
                        desc = order.Description,
                        date = fullDateTime.ToString("o"), // "o" specifier for ISO 8601 format
                        time = fullDateTime.ToString("o"), // Same here, assuming the API truly needs the full datetime in both fields
                        selectedService = order.SelectedService,
                        price = order.Price,
                        userContact = order.ContactNumber,
                        userAddress = order.Address,
                        userId = UserId,
                        cookInfoId = id
                    };

                    string jsonData = System.Text.Json.JsonSerializer.Serialize(orderData);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, content);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle network-related errors
                throw new Exception($"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }


        public async Task<List<SimpleOrderDTO>> GetOrderRequests()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://localhost:7224/api/OrderRequests/request/getUserRequest";
                    client.BaseAddress = new Uri(apiUrl);

                    var UserId = Preferences.Get("UserID", string.Empty);
                    string requestUrl = $"{apiUrl}/{UserId}";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentString = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(contentString);
                        if (apiResponse != null && apiResponse.StatusCode == 200)
                        {
                            return apiResponse.Data;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                // It's a good practice to log the exception
                System.Diagnostics.Debug.WriteLine($"Error in GetOrderRequests: {ex}");
                return null;
            }
        }



        public async Task<List<SimpleOrderDTO>> GetOrders()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = "https://localhost:7224/api/Orders/order/get";
                    client.BaseAddress = new Uri(apiUrl);

                    var UserId = Preferences.Get("UserID", string.Empty);
                    string requestUrl = $"{apiUrl}/{UserId}";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentString = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(contentString);
                        if (apiResponse != null && apiResponse.StatusCode == 200)
                        {
                            return apiResponse.Data;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                // It's a good practice to log the exception
                System.Diagnostics.Debug.WriteLine($"Error in GetOrders: {ex}");
                return null;
            }
        }
        public class ApiResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public List<SimpleOrderDTO> Data { get; set; }
        }

        public async Task<ICollection<CookProfile>> SearchCook(string SelectedFilter,string SearchText)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:7224/api/UserCook/search/by/multiple?searchTerm={SearchText}";

                    if (SelectedFilter == "Category")
                    {
                        apiUrl = $"https://localhost:7224/api/UserCook/search/bycategory?category={SearchText}";

                    }
                    else if (SelectedFilter == "Name")
                    {
                        apiUrl = $"https://localhost:7224/api/UserCook/search/bycook?cookName={SearchText}";


                    }
                    else if(SelectedFilter == "Skills")
                    {
                        apiUrl = $"https://localhost:7224/api/UserCook/search/by/skills?skills={SearchText}";

                    }

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

        public async Task<ICollection<CookProfile>> SearchCooks(string SearchText)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string apiUrl = $"https://localhost:7224/api/UserCook/search/by/multiple?searchTerm={SearchText}";

                    

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

    }
}
