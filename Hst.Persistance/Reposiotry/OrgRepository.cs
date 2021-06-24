using Dapper;
using Hst.Model;
using Hst.Model.ViewModels;
using Hst.Persistance.Infrastructure;
using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hst.Model.Common;

namespace Hst.Persistance.Reposiotry
{
    public class OrgRepository : IOrgRepository
    {
        protected readonly IConnectionfactory _connection = null;
        private const string spCreateOrganisation = "[dbo].[SP_CreateOrganisation]";

        private const string spDeleteOrganisation = "[dbo].[SP_DeleteOrganisation]";

        private const string spGetOrganisationList= "[dbo].[SP_GetOrgList]";

        private const string spGetState = "[dbo].[SP_GetStates]";

        private const string spGetCity = "[dbo].[SP_GetCitesByState]";

        private const string spGetOrgByID = "[dbo].[SP_GetOrgByID]";

        public OrgRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }





        public async Task<ResponseData<List<Organisation>>> GetOrganisation()
        {
            using (var con = _connection.GetConnection())
            {
                var result =await con.QueryAsync<Organisation>(spGetOrganisationList, null, commandType: CommandType.StoredProcedure);
                var list = result.ToList();
               
                return new ResponseData<List<Organisation>>() { Data=list}; 
            }
        }

       
        public async Task<ResponseData<Organisation>> GetOrganisationByID(int id)
        {
            ResponseData<Organisation> organisation = new ResponseData<Organisation>();
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("id", id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Organisation>(
                    spGetOrgByID,
                     param,
                     commandType: CommandType.StoredProcedure);
                organisation.Data = data.FirstOrDefault();
                return organisation;
            }
        }


        public async Task<List<State>> GetStates()
        {
            using (var con = _connection.GetConnection())
            {
                var res = await con.QueryAsync<State>(spGetState,commandType:CommandType.StoredProcedure);
                return res.ToList();
            }
        }

        public async Task<List<City>> GetCities(int StateId)
        {
            using (var con = _connection.GetConnection())
            {
                var res = await con.QueryAsync<City>(spGetCity, new { StateId = StateId },commandType: CommandType.StoredProcedure);
                return res.ToList();
            }
        }







        public async Task<ResponseData<Organisation>> InsertUpdate(Organisation model)
        {
            try {
                ResponseData<Organisation> organisation = new ResponseData<Organisation>();
                using (var con = _connection.GetConnection())
                {
                    string Xml = ExtensionMethod.DataContractSerializeToString<Organisation>(model);
                    var param = new DynamicParameters();
                    param.Add("xml", Xml, dbType: DbType.Xml);
                    var data = await con.QueryAsync<Organisation>(
                         spCreateOrganisation,
                         param,
                         commandType: CommandType.StoredProcedure);
                    organisation.Data = data.FirstOrDefault();
                    //var result= data.FirstOrDefault();
                    return organisation;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }


       public async Task<ResponseData<Organisation>> Delete(int id)
        {
            ResponseData<Organisation> organisation = new ResponseData<Organisation>();
              using (var con = _connection.GetConnection())
              {
                var param = new DynamicParameters();
                param.Add("OrgId",id, dbType: DbType.Int32);
                var data = await con.QueryAsync<Organisation>(
                    spDeleteOrganisation,
                     param,
                     commandType: CommandType.StoredProcedure);
                organisation.Data = data.FirstOrDefault();
                return organisation;
            }
        }

        public Task<List<City>> GetCities()
        {
            throw new NotImplementedException();
        }





    }
}