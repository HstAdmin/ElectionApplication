using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<ResponseData<List<Candidate>>> GetCandidate();
        Task<ResponseData<Candidate>> GetCandidateByID(int Id);
        Task<ResponseData<Candidate>> Delete(int Id);
        Task<ResponseData<Candidate>> InsertUpdate(Candidate model);
        Task<List<Post>> GetPost();
    }
}
