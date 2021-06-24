using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.IRepository
{
    public interface IElectionPostCandiRepository
    {
        Task<ResponseData<ElectionCandidatePost>> GetElectionCandidates(int Id);
    }
}
