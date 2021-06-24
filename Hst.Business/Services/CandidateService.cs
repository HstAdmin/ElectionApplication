using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hst.Business.Services
{
    public class CandidateService:ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;

        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResponseData<List<Candidate>>> GetCandidate()
        {
            return await _unitOfWork.CandidateRepository.GetCandidate();
        }

        public async Task<ResponseData<Candidate>> GetCandidateByID(int Id)
        {
            return await _unitOfWork.CandidateRepository.GetCandidateByID(Id);
        }

        public async Task<ResponseData<Candidate>> InsertUpdate(Candidate model)
        {
            return await _unitOfWork.CandidateRepository.InsertUpdate(model);
        }


        public async Task<List<Post>> GetPost()
        {
            return await _unitOfWork.CandidateRepository.GetPost();
        }

        public async Task<ResponseData<Candidate>> Delete(int Id)
        {
            return await _unitOfWork.CandidateRepository.Delete(Id);
        }
    }

}

