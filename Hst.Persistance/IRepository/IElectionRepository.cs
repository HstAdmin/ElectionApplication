using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.IRepository
{
   public interface IElectionRepository
    {
        Task<ResponseData<List<Election>>> GetElection();
        Task<ResponseData<Election>> GetElectionByID(int Id);
        Task<ResponseData<Election>> InsertUpdate(Election model);
        Task<ResponseData<Election >> Delete(int Id);
        Task<List<Organisation>> GetOrg();
    }
}
