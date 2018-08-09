using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.Infrastructure
{
    /// <summary>
    /// This an helper class which will contain all the static fields
    /// </summary>
    public static class APIHelper
    {
        public static int StatusValue = 0;

        /// <summary>
        /// Stored Procedure List
        /// </summary>
        public static string getLoginDetails = "sp_LoginUser";
        public static string updatePassword = "sp_UpdatePassword";
        public static string getBasicInput = "sp_GetBasicInput";
        public static string getBasicInputDetails = "sp_GetBasicInputDetails";
        public static string forgotPassword = "sp_ForgotPassword";
        public static string getPackageSystemDetails = "sp_GetPackageSystems";
        public static string getSystemVariants = "sp_GetSystemVariants";
        public static string getSolutions = "sp_GetSolutions";
        public static string getPackageList = "sp_GetPackageList";
        public static string getLatestConfigurations = "sp_GetLatestConfiguration";
        public static string uploadProfilePic = "sp_UpdateProfilePic";
        public static string checkLogin = "sp_CheckLogin";
        public static string getEquipmentsDetail = "sp_GetEquipmentDetails";
        public static string getEquipmentVariants = "sp_GetEquipmentVariants";
        public static string getSuppiers = "sp_GetEquipmentSupplier";
        public static string getEquipmentDocuments = "sp_GetEquipmentDocuments";
        public static string getEquipmentSAPLinks = "sp_GetEquipmentSAPLinks";
        public static string getconfigurationsystem = "SP_GET_SYSTEM_SEARCH"; //Added by suraj on date 01/08/2018

       
        /// <summary>
        /// Parameters list. Parameters are related to sequencially as per Stored procedure list above.
        /// </summary>
        public static string getLoginDetailsParameters = "@email~||~@pwd";
        public static string updatePasswordParameters = "@email~||~@oldPwd~||~@newPwd";
        public static string getBasicInputdParameters = "@packageID~||~@configurationID";
        public static string getBasicInputDetailsParameters = "@BASIC_INPUT_ID";
        public static string forgotPasswordParameters = "@email";
        public static string getPackageSystemDetailsParameters = "@PackageID";
        public static string getSystemVariantsParameters = "@System_Variant_ID";
        public static string getSolutionsParameters = "";
        public static string getPackageListParameters = "@SolutionID";
        public static string getLatestConfigurationsParameters = "@UserID~||~@PackageID~||~@NoofConfiguration";
        public static string uploadProfilePicParameters = "@User_ID~||~@ProfilePic";
        public static string checkLoginParameters = "@username~||~@password";
        public static string getEquipmentsDetailParameters = "@PACKAGE_ID~||~@SYSTEM_ID~||~@ConfigurationID";
        public static string getEquipmentVariantsParameters = "@PackageID~||~@SystemID~||~@ConfigurationID~||~@VariantID";
        public static string getSuppiersParameters = "@SystemID~||~@EquipmentID~||~@ConfigurationID~||~@Equipment_Type"; 
        public static string getEquipmentDocumentsParameters = "@EQUIPMENT_ID";
        public static string getEquipmentSAPLinksParameters = "@EQUIPMENT_ID";

       

    }
}
