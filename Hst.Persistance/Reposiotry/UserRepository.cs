using Dapper;
using Hst.Model.Common;
using Hst.Model.ViewModels;
using Hst.Persistance.Infrastructure;
using Hst.Persistance.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hst.Persistance.Reposiotry
{
    public class UserRepository : IUserRepository
    {
        protected readonly IConnectionfactory _connection = null;
        private const string spValidateUser = "hst.ValidateUser";
        private const string spGenerateOTP = "[dbo].[spGenerateOTP]";
        public UserRepository(IConnectionfactory connectionfactory)
        {
            _connection = connectionfactory;
        }


        public async Task<UserModel> GenerateOTP(UserModel model)
        {
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("MobileNo", model.Mobile, DbType.String, size: 20);
                var data = await con.QueryAsync<UserModel>(
                     spGenerateOTP,
                     param,
                     commandType: CommandType.StoredProcedure);
                var result = data.FirstOrDefault();
                //if (result != null)
                //    result.ErrorMessage = param.Get<string>("ErrorMessage");
                return result;
            }
        }

     public async Task<UserModel> ValidateUser(string userName, string password)
        {
            using (var con = _connection.GetConnection())
            {
                var param = new DynamicParameters();
                param.Add("LoginName", userName,DbType.String,size:50);
                param.Add("password", password, DbType.String, size: 50);
                param.Add("ErrorMessage", "", DbType.String,direction:ParameterDirection.Output, size: 50);
                var data = await con.QueryAsync<UserModel>(
                    spValidateUser,
                    param,
                    commandType: CommandType.StoredProcedure);

                 
                var result= data.FirstOrDefault();
                if(result!=null)
                    result.ErrorMessage= param.Get<string>("ErrorMessage");
                return result;

            }
        }

        public async Task<UserModel> CheckUserExisting(UserModel model)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.QueryFirstOrDefaultAsync<UserModel>("");
                return data;
            }
        }

        public async Task<UserModel> SaveUser(UserModel objModel)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.QueryFirstOrDefaultAsync<UserModel>("",null, commandType: CommandType.StoredProcedure);
                if (data != null)
                {
                    data.Message = "Registred successfully.";
                }
                else
                {
                    data = new UserModel() { Message = "Something went wrong." };
                }
                return data;
            }
        }

     

        public async Task<bool> SendOtp(UserModel model)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.ExecuteAsync(@"UPDATE MobileOtpHistory SET IsActive = 0 WHERE IsActive=1 AND Mobile=@mobile
                        INSERT INTO MobileOtpHistory(Mobile,Otp,ExpireTime,CreatedOn,IsActive)
                        VALUES (@mobile,@otp,@expire,GETUTCDATE(),1)",
                    new { mobile = model.Mobile, otp = model.Otp, expire = DateTime.UtcNow.AddMinutes(15) });
                return data > 0;
            }
        }


      


    public async Task<UserModel> CheckOtp(UserModel model)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.QueryFirstOrDefaultAsync<UserModel>("SELECT * FROM MobileOtpHistory WHERE IsActive=1 AND Mobile=@mobile AND Otp=@otp",
                    new { mobile = model.Mobile, otp = model.Otp });
                if (data != null)
                {
                    if (data.ExpireTime > DateTime.UtcNow)
                    {
                        model.Status = true;
                    }
                    else
                    {
                        model.Message = "OTP expired";
                    }
                }
                else
                {
                    model.Message = "Invalid OTP";
                }
                return model;
            }
        }

     

        public async Task<ChangePasswordModel> ChangePassword(ChangePasswordModel objModel)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.QueryFirstOrDefaultAsync<ChangePasswordModel>(
                    "",
                    new
                    {
                        CurrentPassword = objModel.CurrentPassword,
                        Password = objModel.Password,
                        UserId = objModel.UserId,
                        Code = objModel.Code
                    },
                    commandType: CommandType.StoredProcedure);

                objModel.IsCurrentPasswordValid = true;
                if (Convert.ToInt32(objModel.UserId) > 0)
                {
                    if (!Convert.ToBoolean(data.IsCurrentPasswordValid))
                    {
                        objModel.IsCurrentPasswordValid = false;
                        return objModel;
                    }
                }

                objModel.Success = data.Success;
                return objModel;
            }
        }

        public async Task<UserModel> ForgotPassword(ChangePasswordModel objModel)
        {
            using (var con = _connection.GetConnection())
            {
                var res = await con.QueryFirstOrDefaultAsync<UserModel>(
                    @"UPDATE Users SET ForgotPasswordCode=@ForgotPasswordCode WHERE UserName=@UserName AND IsActive=1
                        SELECT * FROM Users WHERE UserName=@UserName AND IsActive=1
                        "
                , new
                {
                    ForgotPasswordCode = objModel.Code,
                    UserName = objModel.UserName
                });
                return res;
            }
        }

        public async Task<UserModel> CheckForgotPasswordCode(ChangePasswordModel objModel)
        {
            using (var con = _connection.GetConnection())
            {
                var res = await con.QueryFirstOrDefaultAsync<UserModel>("SELECT * FROM Users WHERE ForgotPasswordCode=@ForgotPasswordCode AND IsActive=1", new
                {
                    ForgotPasswordCode = objModel.Code
                });
                return res;
            }
        }

        public async Task<UserModel> GetUserById(int id)
        {
            using (var con = _connection.GetConnection())
            {
                var res = await con.QueryFirstOrDefaultAsync<UserModel>(
                    @"SELECT * FROM Users WHERE Id=@Id"
                , new
                {
                    Id = id,
                });
                return res;
            }
        }

        public async Task<bool> SaveProfileImage(UserModel model)
        {
            using (var con = _connection.GetConnection())
            {
                var data = await con.ExecuteAsync(@"
                        UPDATE Users SET ProfilePicture=@ProfilePicture WHERE Id=@Id 
                        ", model);
                return data > 0;
            }
        }
    }
}

