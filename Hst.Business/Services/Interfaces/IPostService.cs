using Hst.Model;
using Hst.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Hst.Business.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResponseData<List<Post>>> GetPost();
        Task<ResponseData<Post>> GetPostByID(int Id);
        Task<ResponseData<Post>> Delete(int Id);
        Task<ResponseData<Post>> InsertUpdate(Post model);
        Task<List<Election>> GetElec();
    }
}
