using Hst.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Persistance.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel> VerifyOTP(UserModel model);
        Task<UserModel> GenerateOTP(UserModel model);
        Task<UserModel> ValidateUser(string userName, string password);
        Task<UserModel> CheckUserExisting(UserModel model);
        Task<UserModel> SaveUser(UserModel objModel);
        Task<bool> SendOtp(UserModel model);
        Task<UserModel> CheckOtp(UserModel model);
        Task<ChangePasswordModel> ChangePassword(ChangePasswordModel objModel);
        Task<UserModel> ForgotPassword(ChangePasswordModel objModel);
        Task<UserModel> CheckForgotPasswordCode(ChangePasswordModel objModel);
        Task<UserModel> GetUserById(int id);
        Task<bool> SaveProfileImage(UserModel model);
    }
}
