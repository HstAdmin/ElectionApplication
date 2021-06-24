using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services
{
    public class ElecPostCandiService : IElecPostCandiService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;

        public ElecPostCandiService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResponseData<ElectionCandidatePost>> GetElectionCandidates(int Id)
        {
            return await _unitOfWork.ElecPostCandiRepository.GetElectionCandidates(Id);
        }
    }
}
