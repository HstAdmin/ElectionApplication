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
    public class ElecPostCandiRepository : IElectionPostCandiRepository
    {
        protected readonly IConnectionfactory _connection = null;

        public ElecPostCandiRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }
        private const string spGetElectionCandidates = "[sp_GetElectionCandidates]";
        private const string spSaveResult = "[sp_SaveResult]";
        private const string spGetResult = "[dbo].[SP_GetResult]";




        public async Task<ResponseData<ElectionCandidatePost>> GetElectionCandidates(int Id)
        {
            ResponseData<ElectionCandidatePost> ecp = new ResponseData<ElectionCandidatePost>();

            using (var con = _connection.GetConnection())
            {
                var p = new DynamicParameters();
                p.Add("ElectionId", Id, dbType: DbType.Int32);
                var reader = await con.QueryMultipleAsync(spGetElectionCandidates, param: p, commandType: CommandType.StoredProcedure);
                var electionPostCandidate = reader.Read<ElectionCandidatePost>();
                var electionPost = reader.Read<ElectionPosts>();
                var electionCandidate = reader.Read<ElectionCandidate>();
                foreach (ElectionPosts post in electionPost)
                {
                    post.ElectionCandidates = electionCandidate.Where(r => r.PostID == post.PostID).ToList();
                }
                foreach (ElectionCandidatePost election in electionPostCandidate)
                {
                    election.ElectionPosts = electionPost.Where(s => s.ElectionID == election.ElectionID).ToList();
                }
                ecp.Data = electionPostCandidate.FirstOrDefault();
                return ecp;

            }
        }


        public async Task<ResponseData<List<Result>>> GetResult()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Result>(spGetResult, null, commandType: CommandType.StoredProcedure);
                var list = result.ToList();

                return new ResponseData<List<Result>>() { Data = list };
            }
        }


        public async Task<ResponseData<Result>> SaveData(List<Result> model)
        { 
                try
                {
                    ResponseData<Result> result = new ResponseData<Result>();
                    using (var con = _connection.GetConnection())
                    {
                        string Xml = ExtensionMethod.DataContractSerializeToString<List<Result>>(model);
                        var param = new DynamicParameters();
                        param.Add("xml", Xml, dbType: DbType.Xml);
                        var data = await con.QueryAsync<Result>(
                             spSaveResult, param, commandType: CommandType.StoredProcedure);
                        result.Data = data.FirstOrDefault();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }
}