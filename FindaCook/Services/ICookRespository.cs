using FindaCook.Maui.Models;
using FindaCook.Models;
using FindaCook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindaCook.Services
{
    
    public interface ICookRespository
    {
       public Task<RegistrationResultClass> RegisterCook(Person p,QualificationInfo q,ProfessionalInfoModel prof);
       
        Task<ICollection<CookProfile>> GetCookByCategory(string cat);
        Task<Boolean> SendOrder(Orders order,string id);
        Task<bool> AddToFavorites(string cookName,CookProfile cook);
        
        Task<ICollection<FavouriteCookDetails>> getFavourites();
        Task<ICollection<SimpleOrderDTO>> GetOrderRequests();
     
        Task<ICollection<SimpleOrderDTO>> GetOrders();

        Task<ICollection<CookProfile>> SearchCook(string SelectedFilter, string SearchText);

        Task<ICollection<CookProfile>> SearchCooks(string SearchText);

        //Cook interfaces
        Task<ICollection<SimpleOrderDTO>> GetAllOrderRequests();
        Task<ICollection<SimpleOrderDTO>> GetAcceptedCookOrderRequests();
        Task<ICollection<SimpleOrderDTO>> GetDeclinedCookOrderRequests();
        Task<int[]> GetAllCounts();  


    }
}
