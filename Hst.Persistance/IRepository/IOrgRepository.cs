using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.IRepository
{
    public interface IOrgRepository
    {
        Task<ResponseData<Organisation>> InsertUpdate(Organisation model);
        Task<ResponseData<Organisation>> Delete(Organisation model);
        Task<ResponseData<List<Organisation>>> GetOrganisation();
        Task<List<State>> GetStates();

        Task<List<City>> GetCities(int StateId);
        
    }
}