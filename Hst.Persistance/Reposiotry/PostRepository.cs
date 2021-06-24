using Dapper;
using Hst.Model;
using Hst.Model.Common;
using Hst.Persistance.Infrastructure;
using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.Reposiotry
{
    public class PostRepository:IPostRepository
    {
        protected readonly IConnectionfactory _connection = null;

        public PostRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }
        private const string spGetPostList = "[dbo].[SP_GetPostList]";

        private const string spCreatePost = "[dbo].[SP_CreatePost]";

        private const string spGetPostByID = "[dbo].[SP_GetPostByID]";

        private const string spDeletePost = "[dbo].[SP_DeletePost]";

        private const string spGetElection = "[dbo].[SP_GetElections]";


        public async Task<ResponseData<List<Post>>> GetPost()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Post>(spGetPostList, null, commandType: CommandType.StoredProcedure);
                var list = result.ToList();

                return new ResponseData<List<Post>>() { Data = list };
            }
        }

        public async Task<ResponseData<Post>> GetPostByID(int Id)
        {
            ResponseData<Post> candidate = new ResponseData<Post>();
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("Id", Id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Post>(
                    spGetPostByID,
                     param,
                     commandType: CommandType.StoredProcedure);
                candidate.Data = data.FirstOrDefault();
                return candidate;
            }
        }

        public async Task<List<Election>> GetElec()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Election>(spGetElection, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }


        public async Task<ResponseData<Post>> InsertUpdate(Post model)
        {
            try
            {
                ResponseData<Post> post = new ResponseData<Post>();
                using (var con = _connection.GetConnection())
                {
                    string Xml = ExtensionMethod.DataContractSerializeToString<Post>(model);
                    var param = new DynamicParameters();
                    param.Add("xml", Xml, dbType: DbType.Xml);
                    var data = await con.QueryAsync<Post>(
                         spCreatePost, param, commandType: CommandType.StoredProcedure);
                    post.Data = data.FirstOrDefault();
                    return post;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResponseData<Post>> Delete(int Id)
        {
            ResponseData<Post> post = new ResponseData<Post>();
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("Id", Id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Post>(
                    spDeletePost,
                     param,
                     commandType: CommandType.StoredProcedure);
                post.Data = data.FirstOrDefault();
                return post;
            }
        }


    }
}
    