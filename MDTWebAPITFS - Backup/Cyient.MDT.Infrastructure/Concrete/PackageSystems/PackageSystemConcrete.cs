using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Cyient.MDT.WebAPI.Core.Repository.SubSystem;
using Cyient.MDT.WebAPI.Core.Entities.SubSystem;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.BasicInput;
namespace Cyient.MDT.Infrastructure.Concrete.PackageSystems
{
    public class PackageSystemConcrete : ISubSystem
    {

        private IEnumerable<Supplier> Suppliers = null;
        public PackageSystemConcrete() { }




        /// <summary>
        /// It will return the Sub System details in landing page for sales based on Package ID
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        public MDTTransactionInfo GetPackageSystemDetails(int packageID)
        {

            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<PackageSystem> PackageSystems = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                SqlParameter package_Id = new SqlParameter("@PackageID", packageID);
                prm.Add(package_Id);
                SqlParameter status = new SqlParameter("@Status", 0);
                status.Direction = ParameterDirection.Output;
                prm.Add(status);
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getPackageSystemDetails, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        PackageSystems = from d in dt.AsEnumerable()
                                         select new PackageSystem
                                         {
                                             PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                             SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                             SYSTEM_VARIANT_ID = d.Field<int>("SYSTEM_VARIANT_ID"),
                                             DEPENDENT_ID = d.Field<int>("DEPENDENT_ID"),
                                             SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                             TYPE = d.Field<bool>("TYPE"),
                                             COST_TYPE = Convert.ToBoolean(d.Field<int>("COST_TYPE")),
                                             SYSTEM_IMAGE = d.Field<string>("SYSTEM_IMAGE"),
                                             DRAWING_PATH = d.Field<string>("DRAWING_PATH"),
                                             SYSTEM_NAME = d.Field<string>("SYSTEM_NAME"),
                                             EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                             ELECTRICAL_COST = d.Field<double>("ELECTRICAL_COST"),
                                             MECHANICAL_COST = d.Field<double>("MECHANICAL_COST"),
                                             COMMENTS = d.Field<string>("COMMENTS"),
                                             REMARKS = d.Field<string>("REMARKS"),
                                             SystemVariants = GetSystemVariants(d.Field<int>("SYSTEM_VARIANT_ID")).transactionObject as IEnumerable<SystemVariants>
                                         };

                        mdt = DatabaseSettings.GetTransObject(PackageSystems, statusValue, "Package System Details Fetched Successfully", ds);

                    }
                    else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }
                    
                }
                else if (statusValue == 5 || statusValue == 6)
                {
                    mdt = DatabaseSettings.GetTransObject(null, statusValue, "", ds);
                }
            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
            }
            return mdt;
        }
        /// <summary>
        /// It will return all the associated variants of systems
        /// </summary>
        /// <param name="VariantID"></param>
        /// <returns></returns>
        private MDTTransactionInfo GetSystemVariants(int variantID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {

            
            IEnumerable<SystemVariants> systemVariants = null;
            List<SqlParameter> prm = new List<SqlParameter>();
            SqlParameter System_Variant_ID = new SqlParameter("@System_Variant_ID", variantID);
            prm.Add(System_Variant_ID);
            SqlParameter status = new SqlParameter("@Status", 0);
            status.Direction = ParameterDirection.Output;
            prm.Add(status);
            int statusValue = 0;
            DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getSystemVariants, out statusValue, prm);
            DataTable dt;
            if (statusValue == 1)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    systemVariants = from d in dt.AsEnumerable()
                                     select new SystemVariants
                                     {
                                         PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                         SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                         SYSTEM_VARIANT_ID = d.Field<int>("SYSTEM_VARIANT_ID"),
                                         DEPENDENT_ID = d.Field<int>("DEPENDENT_ID"),
                                         SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                         TYPE = d.Field<bool>("TYPE"),
                                         COST_TYPE = Convert.ToBoolean(d.Field<int>("COST_TYPE")),
                                         SYSTEM_IMAGE = d.Field<string>("SYSTEM_IMAGE"),
                                         DRAWING_PATH = d.Field<string>("DRAWING_PATH"),
                                         SYSTEM_NAME = d.Field<string>("SYSTEM_NAME"),
                                         EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                         ELECTRICAL_COST = d.Field<double>("ELECTRICAL_COST"),
                                         MECHANICAL_COST = d.Field<double>("MECHANICAL_COST"),
                                         COMMENTS = d.Field<string>("COMMENTS"),
                                         REMARKS = d.Field<string>("REMARKS")
                                     };
                        mdt.msgCode = MessageCode.Success;
                        mdt.status = HttpStatusCode.OK;
                        mdt.message = "Record found";
                        mdt.transactionObject = systemVariants;
                    }
                else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }
            }
            else if (statusValue == 5 || statusValue == 6)
            {
                mdt = DatabaseSettings.GetTransObject(null, statusValue, "", ds);
            }

            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
            }
            return mdt;
        }

        /// <summary>
        /// It will return the Equipments detail with variants and their cost histroy
        /// </summary>
        /// <param name="PackageID"></param>
        /// <param name="SystemID"></param>
        /// <param name="ConfigurationID"></param>
        /// <returns></returns>
        public MDTTransactionInfo GetEquipmentsDetail(int packageID, int systemID, int configurationID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<EquipmentsDetail> equipmentsDetails = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                prm = DatabaseSettings.BindParamers(APIHelper.getEquipmentsDetailParameters, packageID.ToString() + "~||~" + systemID.ToString() + "~||~" + configurationID.ToString());
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getEquipmentsDetail, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        equipmentsDetails = from d in dt.AsEnumerable()
                                            select new EquipmentsDetail
                                            {
                                                SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                                PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                                SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                                EQUIPMENT_ID = d.Field<int>("EQUIPMENT_ID"),
                                                SUPPLIER_ID = d.Field<int>("SUPPLIER_ID"),
                                                CONFIGURATION_ID = d.Field<int>("CONFIGURATION_ID"),
                                                UPDT_USER_ID = d.Field<int>("UPDT_USER_ID"),
                                                IS_MANDATORY = d.Field<bool>("IS_MANDATORY"),
                                                EQUIPMENT_NAME = d.Field<string>("EQUIPMENT_NAME"),
                                                SUPPLIER_NAME = d.Field<string>("SUPPLIER_NAME"),
                                                EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                                EQPMNT_VARIANT_ID = d.Field<int>("EQPMNT_VARIANT_ID"),
                                                COMMENTS = d.Field<string>("COMMENTS"),
                                                EQUIPMENT_DESC = d.Field<string>("EQUIPMENT_DESC"),
                                                EQUIPMENT_TYPE = d.Field<string>("EQUIPMENT_TYPE"),
                                                EquipmentVariants = GetEquipmentVariants(d.Field<int>("PACKAGE_ID"), d.Field<int>("SYSTEM_ID"), d.Field<int>("CONFIGURATION_ID"), d.Field<int>("EQPMNT_VARIANT_ID")).transactionObject as IEnumerable<EquipmentVariants>,
                                                Documents = GetEquipmentDocuments(d.Field<int>("EQUIPMENT_ID")).transactionObject as IEnumerable<EquipmentDocuments>,
                                                SAPLinks = GetEquipmentSAPLinks(d.Field<int>("EQUIPMENT_ID")).transactionObject as IEnumerable<EquipmentSAPLink>,
                                                Suppliers = GetSuppliers(d.Field<int>("SYSTEM_ID"), d.Field<int>("EQUIPMENT_ID"), d.Field<int>("CONFIGURATION_ID"), d.Field<string>("EQUIPMENT_TYPE"))
                                            };

                        mdt = DatabaseSettings.GetTransObject(equipmentsDetails, statusValue, "System equipment details fetched successfully", ds);

                    }
                    else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }

                }

            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
                // Need to log error in logger from ex
            }
            return mdt;
        }

        /// <summary>
        /// It will return equipment variants with their cost history based on Variant ID
        /// </summary>
        /// <param name="VariantID">Pass the variant ID for associated equipment</param>
        /// <returns></returns>
        public MDTTransactionInfo GetEquipmentVariants(int packageID, int systemID, int configurationID, int variantID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<EquipmentVariants> equipmentVariants = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                prm = DatabaseSettings.BindParamers(APIHelper.getEquipmentVariantsParameters, packageID.ToString() + "~||~" + systemID.ToString() + "~||~" + configurationID.ToString() + "~||~" + variantID.ToString());
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getEquipmentVariants, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        equipmentVariants = from d in dt.AsEnumerable()
                                            select new EquipmentVariants
                                            {
                                                SELECT = Convert.ToBoolean(d.Field<int>("SELECT")),
                                                PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                                SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                                EQUIPMENT_ID = d.Field<int>("EQUIPMENT_ID"),
                                                SUPPLIER_ID = d.Field<int>("SUPPLIER_ID"),
                                                CONFIGURATION_ID = d.Field<int>("CONFIGURATION_ID"),
                                                UPDT_USER_ID = d.Field<int>("UPDT_USER_ID"),
                                                IS_MANDATORY = d.Field<bool>("IS_MANDATORY"),
                                                EQUIPMENT_NAME = d.Field<string>("EQUIPMENT_NAME"),
                                                SUPPLIER_NAME = d.Field<string>("SUPPLIER_NAME"),
                                                EQUIPMENT_COST = d.Field<double>("EQUIPMENT_COST"),
                                                EQPMNT_VARIANT_ID = d.Field<int>("EQPMNT_VARIANT_ID"),
                                                COMMENTS = d.Field<string>("COMMENTS"),
                                                EQUIPMENT_DESC = d.Field<string>("EQUIPMENT_DESC"),
                                                EQUIPMENT_TYPE = d.Field<string>("EQUIPMENT_TYPE"),
                                                Documents = GetEquipmentDocuments(d.Field<int>("EQUIPMENT_ID")).transactionObject as IEnumerable<EquipmentDocuments>,
                                                SAPLinks = GetEquipmentSAPLinks(d.Field<int>("EQUIPMENT_ID")).transactionObject as IEnumerable<EquipmentSAPLink>,
                                                Suppliers = Suppliers = GetSuppliers(d.Field<int>("SYSTEM_ID"), d.Field<int>("EQUIPMENT_ID"), d.Field<int>("CONFIGURATION_ID"), d.Field<string>("EQUIPMENT_TYPE")) //(d.Field<int>("SUPPLIER_ID")!=0)?Suppliers:null
                                            };

                        mdt = DatabaseSettings.GetTransObject(equipmentVariants, statusValue, "System equipment variants fetched successfully", ds);

                    }
                    else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }

                }

            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
                // Need to log error in logger from ex
            }
            return mdt;
        }


        /// <summary>
        /// It will return the list of Suppliers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Supplier> GetSuppliers(int systemID, int equipmentID, int configurationID, string equipment_Type)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            //Suppliers = new List<Supplier>();
            try
            {

                List<SqlParameter> prm = new List<SqlParameter>();
                prm = DatabaseSettings.BindParamers(APIHelper.getSuppiersParameters, systemID.ToString() + "~||~" + equipmentID.ToString() + "~||~" + configurationID.ToString() + "~||~" + equipment_Type.ToString());
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getSuppiers, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        Suppliers = from d in dt.AsEnumerable()
                                    select new Supplier
                                    {
                                        SUPPLIER_ID = d.Field<int>("SUPPLIER_ID"),
                                        EQUIPMENT_ID = d.Field<int>("EQUIPMENT_ID"),
                                        SYSTEM_ID = d.Field<int>("SYSTEM_ID"),
                                        SUPPLIER_NAME = d.Field<string>("SUPPLIER_NAME"),
                                        COST = d.Field<double>("COST"),
                                        VALID_FROM = d.Field<DateTime>("VALID_FROM"),
                                        VALID_TO = d.Field<DateTime>("VALID_TO")
                                    };

                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Technical Error in the system, please contact to administrator");
                // Need to log error in logger from ex
            }
            return Suppliers;
        }

        /// <summary>
        /// It will return equipment documents list for respective equipment
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public MDTTransactionInfo GetEquipmentDocuments(int equipmentID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<EquipmentDocuments> equipmentDocuments = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                prm = DatabaseSettings.BindParamers(APIHelper.getEquipmentDocumentsParameters, equipmentID.ToString());
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getEquipmentDocuments, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        equipmentDocuments = from d in dt.AsEnumerable()
                                             select new EquipmentDocuments
                                             {
                                                 DOCUMENT_ID = d.Field<int>("DOCUMENT_ID"),
                                                 EQUIPMENT_ID = d.Field<int>("EQUIPMENT_ID"),
                                                 NAME = d.Field<string>("NAME"),
                                                 DESCRIPTION = d.Field<string>("DESCRIPTION"),
                                                 DOCUMENT_PATH = d.Field<string>("DOCUMENT_PATH"),

                                             };

                        mdt = DatabaseSettings.GetTransObject(equipmentDocuments, statusValue, "System equipment documents fetched successfully", ds);

                    }
                    else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }

                }

            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
                // Need to log error in logger from ex
            }
            return mdt;
        }

        /// <summary>
        /// It will return respective equipment SAP links to navigate SAP
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>

        public MDTTransactionInfo GetEquipmentSAPLinks(int equipmentID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<EquipmentSAPLink> equipmentSAPLinks = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                prm = DatabaseSettings.BindParamers(APIHelper.getEquipmentSAPLinksParameters, equipmentID.ToString());
                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getEquipmentSAPLinks, out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        equipmentSAPLinks = from d in dt.AsEnumerable()
                                            select new EquipmentSAPLink
                                            {
                                                SAP_LINK_ID = d.Field<int>("SAP_LINK_ID"),
                                                EQUIPMENT_ID = d.Field<int>("EQUIPMENT_ID"),
                                                NAME = d.Field<string>("NAME"),
                                                DESCRIPTION = d.Field<string>("DESCRIPTION"),
                                                SAP_LINK_URL = d.Field<string>("SAP_LINK_URL"),

                                            };

                        mdt = DatabaseSettings.GetTransObject(equipmentSAPLinks, statusValue, "System equipment SAP links fetched successfully", ds);

                    }
                    else
                    {
                        mdt.msgCode = MessageCode.Failed;
                        mdt.status = HttpStatusCode.NoContent;
                        mdt.message = "No record found";
                    }

                }

            }
            catch (Exception ex)
            {
                mdt.status = HttpStatusCode.ExpectationFailed;
                mdt.msgCode = MessageCode.TechnicalError;
                mdt.message = "Technical Error in the system, please contact to administrator";
                // Need to log error in logger from ex
            }
            return mdt;
        }


    }

}
