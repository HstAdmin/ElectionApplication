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
    public class ElecPostCandiRepository:IElectionPostCandiRepository
    {
        protected readonly IConnectionfactory _connection = null;

        public ElecPostCandiRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }
        private const string spGetElectionCandidates = "[sp_GetElectionCandidates]";


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
                    post.electionCandidates = electionCandidate.Where(r => r.PostID == post.PostID).ToList();
                }
                foreach (ElectionCandidatePost election in electionPostCandidate)
                {
                    election.electionPosts = electionPost.Where(s => s.ElectionID == election.ElectionID).ToList();
                }
                ecp.Data = electionPostCandidate.FirstOrDefault();
                return ecp;
                
            }
        }
    }
}

