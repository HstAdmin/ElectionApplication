using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Model;
using Hst.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hst.Business.Services
{
    public class PostService:IPostService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<ResponseData<List<Post>>> GetPost()
        {
            return await _unitOfWork.PostRepository.GetPost();
        }

        public async Task<ResponseData<Post>> GetPostByID(int Id)
        {
            return await _unitOfWork.PostRepository.GetPostByID(Id);
        }

        public async Task<ResponseData<Post>> InsertUpdate(Post model)
        {
            return await _unitOfWork.PostRepository.InsertUpdate(model);
        }

        public async Task<List<Election>> GetElec()
        {
            return await _unitOfWork.PostRepository.GetElec();
        }

        public async Task<ResponseData<Post>> Delete(int Id)
        {
            return await _unitOfWork.PostRepository.Delete(Id);
        }
    }

}

   
