using AutoMapper;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services.Interfaces
{
    public interface IElecPostCandiService
    {
        Task<ResponseData<ElectionCandidatePost>> GetElectionCandidates(int Id);
    }
}
