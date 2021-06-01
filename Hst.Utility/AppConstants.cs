using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Utility
{
    public static class AppConstants
    {
        public const string SecureKey = "HST@Restro9458688";
        public const int ContentVersion = 1;
        public const string OtpMessageTemplate = "Your OTP is {0} to verify your mobile no.";
        public const string SomethingWentWrong = "Something went wrong. Please try again.";
        //public const string DoctorApproveSMS = @"Congratulation, {0}. Your profile has been verified on \""Zoolatry\"", recognised from Government Of India for digital veterinary platform. To check your unique verified profile visit https://zoolatrylife.com/verified/profile/signup . Thank you (Team Zoolatry)";

        public const string DoctorApproveSMS = @"Dear {0}, Your Profile has been {1} . Please login on www.zoolatrylife.com with registered number and go-through your details .
                    For any assistance please contact toll free number 1800 890 2095. Regards Team Zoolatry";
        public const string DoctorRejectSMS = @"Dear {0}, your profile has been rejected , please contact to zoolatry on toll free 1800 890 2095";

        public const string DoctorProfileStatusSMSTemplateId = "1207161936150299134";
        public const string DoctorRejectSMSTemplateId = "";
        public const string DoctorProfileStatusSMSEntityId = "1701159958452194346";
        //public const string DoctorRejectSMSEntityId = "";
    }
}
