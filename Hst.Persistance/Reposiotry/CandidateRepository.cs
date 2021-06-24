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
    public class CandidateRepository : ICandidateRepository
    {
        protected readonly IConnectionfactory _connection = null;

        public CandidateRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }
        private const string spGetCandidateList = "[dbo].[SP_GetCandidateList]";

        private const string spCreateCandidate = "[dbo].[SP_CreateCandidate]";

        private const string spGetCandidateByID = "[dbo].[SP_GetCandidateByID]";

        private const string spDeleteCandidate = "[dbo].[SP_DeleteCandidate]";

        private const string spGetPost = "[dbo].[SP_GetPosts]";


        public async Task<ResponseData<List<Candidate>>> GetCandidate()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Candidate>(spGetCandidateList, null, commandType: CommandType.StoredProcedure);
                var list = result.ToList();

                return new ResponseData<List<Candidate>>() { Data = list };
            }
        }

        public async Task<ResponseData<Candidate>> GetCandidateByID(int Id)
        {
            ResponseData<Candidate> candidate = new ResponseData<Candidate>();
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("Id", Id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Candidate>(
                    spGetCandidateByID,
                     param,
                     commandType: CommandType.StoredProcedure);
                candidate.Data = data.FirstOrDefault();
                return candidate;
            }
        }

        public async Task<List<Post>> GetPost()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Post>(spGetPost, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    

        public async Task<ResponseData<Candidate>> InsertUpdate(Candidate model)
        {
            try
            {
                ResponseData<Candidate> candidate = new ResponseData<Candidate>();
                using (var con = _connection.GetConnection())
                {
                    string Xml = ExtensionMethod.DataContractSerializeToString<Candidate>(model);
                    var param = new DynamicParameters();
                    param.Add("xml", Xml, dbType: DbType.Xml);
                    var data = await con.QueryAsync<Candidate>(
                         spCreateCandidate, param, commandType: CommandType.StoredProcedure);
                    candidate.Data = data.FirstOrDefault();
                    return candidate;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<ResponseData<Candidate>> Delete(int Id)
        {
            ResponseData<Candidate> candidate = new ResponseData<Candidate>();
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("Id", Id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Candidate>(
                    spDeleteCandidate,
                     param,
                     commandType: CommandType.StoredProcedure);
                candidate.Data = data.FirstOrDefault();
                return candidate;
            }
        }


    }
}