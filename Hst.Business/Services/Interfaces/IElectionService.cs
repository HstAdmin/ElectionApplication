using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services.Interfaces
{
   public interface IElectionService
    {
        Task<ResponseData<List<Election>>> GetElection();
        Task<ResponseData<Election>> GetElectionByID(int Id);
        Task<ResponseData<Election>> Delete(int Id);
        Task<ResponseData<Election>> InsertUpdate(Election model);
        Task<List<Organisation>> GetOrg();
    }
}
