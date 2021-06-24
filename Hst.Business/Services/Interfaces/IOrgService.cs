using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services.Interfaces
{
    public interface IOrgService
    {
        Task<ResponseData<Organisation>> InsertUpdate(Organisation model);
        Task<ResponseData<Organisation>> Delete(int id);
        Task<ResponseData<Organisation>> GetOrganisationByID(int id);
        Task<ResponseData<List<Organisation>>> GetOrganisation();
        Task<List<State>> GetStates();
        Task<List<City>> GetCities(int StateId);
    }
}
