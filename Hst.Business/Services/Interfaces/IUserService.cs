
using Hst.Model;
using Hst.Model.ViewModels;
using Hst.Voter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services.Interfaces
{
    public interface IUserService
    {
      
        Task<UserViewModel> VerifyOTP(UserViewModel model);
        Task<UserModel> GenerateOTP(UserModel model);
        Task<UserModel> ValidateUser(string userName, string password);
        Task<UserModel> SaveUser(UserModel objModel);
        Task<UserModel> CheckUserExisting(UserModel model);
        Task<bool> SendOtp(UserModel model);
        Task<UserModel> CheckOtp(UserModel model);
        Task<ChangePasswordModel> ChangePassword(ChangePasswordModel objModel);
        Task<ChangePasswordModel> ForgotPassword(ChangePasswordModel objModel);
        Task<UserModel> CheckForgotPasswordCode(ChangePasswordModel objModel);
        Task<UserModel> GetUserById(int id);
        Task<bool> SaveProfileImage(UserModel model);
    }
}
