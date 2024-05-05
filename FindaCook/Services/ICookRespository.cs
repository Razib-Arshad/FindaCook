using FindaCook.Maui.Models;
using FindaCook.Models;
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
        
        Task<ICollection<CookProfile>> getFavourites(string id);



    }
}
