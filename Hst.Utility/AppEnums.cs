using System;
using System.Collections.Generic;
using System.Text;

namespace Hst.Utility
{
    public enum UserTypes
    {
        Admin = 1,
        School,
        Parents,
        Teacher,
        Student
    }

    public enum UserClaims
    {
        UserName,
        UserId,
        Mobile,
        DefaultSchoolId,
        Schools,
        Name,
        ProfilePicture,
        UserTypeId
    }

    public enum MasterTypes
    {
        BoardTypes = 1,
        Mediums,
        SchoolTypes,
        Classes,
        WeekDays,
        SchoolHoursType,
        CampusFacilityTypes,
        SchoolCategories,
        EnquiryTypes,
        EnquiryStatus,
        ApprovalStatus,
        Degrees,
        Universities
    }

    public enum Menus
    {
        UserManageSchool,
        UserMyProfile,
        UserMyWishlist,
        UserAdmissionEnquires,
        UserVisitRequest,
        UserRatingAndReviews,
        SchoolSuperProfile,
        SchoolPrincipalDesk,
        SchoolOperatingHours,
        SchoolOfficeHours,
        SchoolAdmission,
        SchoolAcademics,
        SchoolCampus,
        SchoolMediaAndGallery,
        SchoolSocial,
        SchoolSupportTicket,
        SchoolMyPlans,
        SchoolPaymentHistory,

        TeacherProfessionalProfile
    }

    public enum SchoolHourTypes
    {
        OperatingHours = 1,
        OfficeHours
    }

    public enum ActionType
    {
        Get = 1,
        Save,
        Delete
    }

    public enum Settings
    {
        TOP_MAR_QUE_TEXT = 1
    }

    public enum EnquiryTypes
    {
        AdmissionApplication = 1,
        SchoolVisit,
        AdmissionEnquiryForSchool,
        GetInTouch
    }

    public enum SchoolApprovalStatus
    {
        NotApproved = 1,
        UnderApproval,
        Rejected,
        Approved
    }

    public enum AdminMenus
    {
        Dashboard,
        Doctors,
        LSA,
        ChangePassword
    }

    public enum CustomerTypes
    {
        Doctor = 1,
        LSA = 5
    }
}
