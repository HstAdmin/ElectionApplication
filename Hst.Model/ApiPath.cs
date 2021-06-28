using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Model
{
    public static class ApiPath
    {
        public static string APIBaseUrl { get; set; }
        public static string WebBaseUrl { get; set; }
        public static string ImageBasePath { get; set; }


        public static class Organisation
        {
            public static string GetStates { get { return "api/Org/GetStates"; } }
            public static string GetCities(int StateId) { return "api/Org/GetCities/" + StateId; }
            public static string GetOrganisation { get { return "api/Org/GetAll"; }} 
            public static string GetOrganisationByID(int id) { return "api/Org/GetOrgByID?id=" + id; }
            public static string InsertUpdate { get { return "api/Org/InsertUpdate"; } }
            public static string Delete(int id) { return "api/Org/Delete?id=" + id; } 
        }

        public static class Election
        {
            public static string GetElection { get { return "api/Election/GetAll"; } }
            public static string GetElectionByID(int Id) { return "api/Election/GetElectionByID?Id=" + Id; }
            public static string InsertUpdate { get { return "api/Election/InsertUpdate"; } }
            public static string GetOrg { get { return "api/Election/GetOrg"; } }
            public static string Delete(int Id) { return "api/Election/Delete?Id=" + Id; }
        }
        public static class Post
        {
            public static string GetPost { get { return "api/PostAPI/GetAll"; } }
            public static string GetPostByID(int Id) { return "api/PostAPI/GetPostByID?Id=" + Id; }
            public static string InsertUpdate { get { return "api/PostAPI/InsertUpdate"; } }
            public static string GetElec { get { return "api/PostAPI/GetElec"; } }
            public static string Delete(int Id) { return "api/PostAPI/Delete?Id=" + Id; }
        }


        public static class Candidate
        {
            public static string GetCandidate { get { return "api/Candi/GetAll"; } }
            public static string GetCandidateByID(int Id) { return "api/Candi/GetCandidateByID?Id=" + Id; }
            public static string InsertUpdate { get { return "api/Candi/InsertUpdate"; } }
            public static string GetPost { get { return "api/Candi/GetPost"; } }
            public static string Delete(int Id) { return "api/Candi/Delete?Id=" + Id; }
        }
         
        public static class ECP
        {
            public static string GetElectionCandidates(int Id) { return "api/ElecPostCandi/GetElectionCandidates?Id="+Id; }
            public static string SaveData { get { return "api/ElecPostCandi/SaveData"; } }
            public static string GetResult { get { return "api/ElecPostCandi/GetAll"; } }
        }


        public static class User
        {
            public static string GenerateOTP { get { return "api/User/GenerateOTP"; } }
            public static string IsUserExist { get { return "api/User/IsUserExist"; } }
            public static string VerifyOTP { get { return "api/User/ValidateOTP"; } }
            public static string CheckUserExisting { get { return "api/User/CheckUserExisting"; } }
            public static string SendOtp { get { return "api/User/SendOtp"; } }
            public static string CheckOtp { get { return "api/User/CheckOtp"; } }
            public static string SaveUser { get { return "api/User/SaveUser"; } }
            public static string ValidateUser { get { return "api/User/ValidateUser"; } }
            
            public static string GetUserById(int id) { return "api/User/GetUserById/" + id; }

            public static string GetUsers { get { return "api/User/GetUsers"; } }
        }
    }
}
