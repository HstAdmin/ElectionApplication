using AutoMapper;
using Hst.Business.Services.Interfaces;
using Hst.Business.UnitOfWork;
using Hst.Cryptography;
using Hst.Model.ViewModels;
using Hst.Utility;
using Hst.Utility.EmailHelper;
using Hst.Utility.SmsHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = null;
        private readonly IMapper _mapper = null;
        private readonly IEmailHelper _emailHelper = null;
        private readonly ISmsHelper _smsHelper = null;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEmailHelper emailHelper, ISmsHelper smsHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailHelper = emailHelper;
            _smsHelper = smsHelper;
        }



        public async Task<UserModel> ValidateUser(string userName, string password)
        {
            string encryptedPass = Cryptography.Cryptography.Encrypt(password, AppConstants.SecureKey);
            var result = await _unitOfWork.UserRepository.ValidateUser(userName, encryptedPass);
            return result;
        }





        //public UserViewModel GetById(int id)
        //{
        //    var data = _unitOfWork.UserRepository.GetByID(id);
        //    UserViewModel result = _mapper.Map<UserViewModel>(data);
        //    return result;
        //}

        public async Task<UserModel> SaveUser(UserModel objModel)
        {
            string password = string.Empty;
            if (objModel.Id == 0)
            {
                objModel.Password = Cryptography.Cryptography.Encrypt(objModel.Password, AppConstants.SecureKey);
            }
            var userVM = await _unitOfWork.UserRepository.SaveUser(objModel);
            return userVM;
        }


        public async Task<UserModel> CheckUserExisting(UserModel model)
        {
            return await _unitOfWork.UserRepository.CheckUserExisting(model);
        }
        public async Task<bool> SendOtp(UserModel model)
        {
            model.Otp = "123456";//GenerateOtp();
            var res = await _unitOfWork.UserRepository.SendOtp(model);
            //var smsResponse = _smsHelper.SendMessage(new SmsModel()
            //{
            //    Mobile = model.Mobile,
            //    Message = string.Format(AppConstants.OtpMessageTemplate, model.Otp)
            //});
            //await _unitOfWork.UserRepository.SaveSmsHistory(smsResponse);
            return res;
        }


        public async Task<UserModel> GenerateOTP(UserModel model)
        { 
            return await _unitOfWork.UserRepository.GenerateOTP(model);
        }




        public async Task<UserModel> CheckOtp(UserModel model)
        {
            return await _unitOfWork.UserRepository.CheckOtp(model);
        }

        public async Task<ChangePasswordModel> ChangePassword(ChangePasswordModel objModel)
        {
            objModel.Password = Cryptography.Cryptography.Encrypt(objModel.Password, AppConstants.SecureKey);
            objModel.CurrentPassword = Cryptography.Cryptography.Encrypt(objModel.CurrentPassword, AppConstants.SecureKey);
            return await _unitOfWork.UserRepository.ChangePassword(objModel);
        }

        public async Task<ChangePasswordModel> ForgotPassword(ChangePasswordModel objModel)
        {
            objModel.Code = Cryptography.Cryptography.Encrypt(CommonMethods.GenerateRandomString(10), AppConstants.SecureKey).Replace(" ", "+");
            var res = await _unitOfWork.UserRepository.ForgotPassword(objModel);
            if (res != null && res.Id > 0 && !string.IsNullOrEmpty(res.Email))
            {

                string body = File.ReadAllText(objModel.EmailTemplatePath);
                body = body.Replace("{UserName}", objModel.UserName);
                body = body.Replace("{Url}", objModel.Url + "?code=" + objModel.Code);
                Task.Run(() =>
                {
                    _emailHelper.SendMail(new List<string>() { res.Email }, "Forgot Password", body);
                });
            }
            objModel.Success = res != null && res.Id > 0;
            return objModel;
        }

        public async Task<UserModel> CheckForgotPasswordCode(ChangePasswordModel objModel)
        {
            objModel.Code = objModel.Code.Replace(" ", "+");
            var result = await _unitOfWork.UserRepository.CheckForgotPasswordCode(objModel);
            return _mapper.Map<UserModel>(result);
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _unitOfWork.UserRepository.GetUserById(id);
        }

        public async Task<bool> SaveProfileImage(UserModel model)
        {
            return await _unitOfWork.UserRepository.SaveProfileImage(model);
        }
    }
}
