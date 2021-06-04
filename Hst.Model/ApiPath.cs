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


        public static class User
        {
            public static string GenerateOTP { get { return "api/User/GenerateOTP"; } }
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
