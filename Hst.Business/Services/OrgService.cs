using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using Hst.Voter.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace Hst.Business.Services
{
   public class OrgService : IOrgService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;
        
        public OrgService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        public async Task<ResponseData<List<Organisation>>> GetOrganisation()
        {
            return await _unitOfWork.OrgRepository.GetOrganisation();
        }

        public async Task<ResponseData<Organisation>> GetOrganisationByID(int id)
        {
            return await _unitOfWork.OrgRepository.GetOrganisationByID(id);
        }

        public async Task<List<State>> GetStates()
        {
            return await _unitOfWork.OrgRepository.GetStates();
        }

        public async Task<List<City>> GetCities(int StateId)
        {
            return await _unitOfWork.OrgRepository.GetCities(StateId);
        }


        public async Task<ResponseData<Organisation>> InsertUpdate(Organisation model)
        {
            return await _unitOfWork.OrgRepository.InsertUpdate(model);
        }

        public async Task<ResponseData<Organisation>> Delete(int id)
        {
            return await _unitOfWork.OrgRepository.Delete(id);
        }






        //public void Delete(int id)
        //{
        //    _unitOfWork.OrgRepository.Delete(id);
        //}



    }
}
