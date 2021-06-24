using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.IRepository
{
    public interface ICandidateRepository
    {
        Task<ResponseData<List<Candidate>>> GetCandidate();
        Task<ResponseData<Candidate>> GetCandidateByID(int Id);
        Task<ResponseData<Candidate>> InsertUpdate(Candidate model);
        Task<ResponseData<Candidate>> Delete(int Id);
        Task<List<Post>> GetPost();
    }
}
