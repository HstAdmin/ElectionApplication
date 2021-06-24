using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Hst.Business.Services
{
    public class ElectionService:IElectionService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;

        public ElectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResponseData<List<Election>>> GetElection()
        {
            return await _unitOfWork.ElectionRepository.GetElection();
        }

        public async Task<ResponseData<Election>> GetElectionByID(int Id)
        {
            return await _unitOfWork.ElectionRepository.GetElectionByID(Id);
        }

        public async Task<ResponseData<Election>> InsertUpdate(Election model)
        {
            return await _unitOfWork.ElectionRepository.InsertUpdate(model);
        }


        public async Task<List<Organisation>> GetOrg()
        {
            return await _unitOfWork.ElectionRepository.GetOrg();
        }

            public async Task<ResponseData<Election>> Delete(int Id)
        {
            return await _unitOfWork.ElectionRepository.Delete(Id);
        }
    }
}
