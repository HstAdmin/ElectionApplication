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
    public class ElectionRepository : IElectionRepository
    {
        protected readonly IConnectionfactory _connection = null;

        public ElectionRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }
        private const string spGetElectionByID = "[dbo].[SP_GetElectionByID]";

        private const string spCreateElection = "[dbo].[SP_CreateElection]";

        private const string spGetElectionList = "[dbo].[SP_GetElectionList]";

        private const string spDeleteElection = "[dbo].[SP_DeleteElection]";

        private const string spGetOrganisation = "[dbo].[SP_GetOrganisations]";


        public async Task<ResponseData<List<Election>>> GetElection()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Election>(spGetElectionList, null, commandType: CommandType.StoredProcedure);
                var list = result.ToList();

                return new ResponseData<List<Election>>() { Data = list };
            }
        }

        public async Task<ResponseData<Election>> GetElectionByID(int Id)
        {
                ResponseData<Election> election = new ResponseData<Election>();
                using (var con = _connection.GetConnection())
                {
                    var param = new DynamicParameters();
                    param.Add("Id", Id, dbType: DbType.Int32);
                    var data = await con.QueryAsync<Election>(
                        spGetElectionByID,
                         param,
                         commandType: CommandType.StoredProcedure);
                election.Data = data.FirstOrDefault();
                    return election;
                }
            }

        public async Task<List<Organisation>> GetOrg()
        {
            using (var con = _connection.GetConnection())
            {
                var result = await con.QueryAsync<Organisation>(spGetOrganisation, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }


        public async Task<ResponseData<Election>> InsertUpdate(Election model)
        {
            try
            {
                ResponseData<Election> election = new ResponseData<Election>();
                using (var con = _connection.GetConnection())
                {
                    string Xml = ExtensionMethod.DataContractSerializeToString<Election>(model);
                    var param = new DynamicParameters();
                    param.Add("xml", Xml, dbType: DbType.Xml);
                    var data = await con.QueryAsync<Election>(
                         spCreateElection, param, commandType: CommandType.StoredProcedure);
                    election.Data = data.FirstOrDefault();
                    return election;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     

        public async Task<ResponseData<Election>> Delete(int Id)
    {
        ResponseData<Election> election = new ResponseData<Election>();
        using (var con = _connection.GetConnection())
        {
            var param = new DynamicParameters();
            param.Add("Id", Id, dbType: DbType.Int32);
            var data = await con.QueryAsync<Election>(
                spDeleteElection,
                 param,
                 commandType: CommandType.StoredProcedure);
            election.Data = data.FirstOrDefault();
            return election;
        }
    }


}
}
