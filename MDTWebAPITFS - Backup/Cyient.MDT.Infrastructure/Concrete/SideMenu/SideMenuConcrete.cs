using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.SideMenu;
using Cyient.MDT.WebAPI.Core.Repository.SideMenu;
namespace Cyient.MDT.Infrastructure.Concrete.SideMenu
{
    /// <summary>
    /// Repository entity for Side menu
    /// </summary>
    public class SideMenuConcrete : ISideMenu
    {
        public SideMenuConcrete() { }

        /// <summary>
        /// This will return the list side menu from Solution to Configuration
        /// </summary>
        /// <returns></returns>
        public MDTTransactionInfo GetSideMenu(int UserID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<SolutionList> solutionLists = null;
                //List<SqlParameter> prm = new List<SqlParameter>();
                //SqlParameter Status = new SqlParameter("@Status", 0);
                //Status.Direction = ParameterDirection.Output;
                //prm.Add(Status);

                List<SqlParameter> prm = DatabaseSettings.BindParamers();

                int StatusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getSolutions, out StatusValue, prm);
                DataTable dt;
                if (StatusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        solutionLists = from d in dt.AsEnumerable()
                                        select new SolutionList
                                        {
                                            SOLUTION_ID = d.Field<int>("SOLUTION_ID"),
                                            SOLUTION_NAME = d.Field<string>("SOLUTION_NAME"),
                                            Packages = GetPackageList(d.Field<int>("SOLUTION_ID"), UserID).transactionObject as IEnumerable<PackageList>
                                        };
                    }
                    //mdt = DatabaseSettings.GetTransObject(solutionLists, StatusValue, "Record Found", ds);
                    mdt.msgCode = MessageCode.Success;
                    mdt.status = HttpStatusCode.OK;
                    mdt.message = "Record found";
                    mdt.transactionObject = solutionLists;
                }
                else if (StatusValue == 5 || StatusValue == 6)
                {
                    mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
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
        /// It return all package list for respective Solution
        /// </summary>
        /// <param name="SolutionID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        private MDTTransactionInfo GetPackageList(int solutionID, int userID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<PackageList> packageLists = null;

                List<SqlParameter> prm = DatabaseSettings.BindParamers(APIHelper.getPackageListParameters, solutionID.ToString());

                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getPackageList, out statusValue, prm);
                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        packageLists = from d in dt.AsEnumerable()
                                       select new PackageList
                                       {
                                           PACKAGE_ID = d.Field<int>("PACKAGE_ID"),
                                           PACKAGE_NAME = d.Field<string>("PACKAGE_NAME"),
                                           SOLUTION_ID = d.Field<int>("SOLUTION_ID"),
                                           Configurations = GetConfigurationList(userID, d.Field<int>("PACKAGE_ID")).transactionObject as IEnumerable<ConfigurationList>
                                       };

                    }
                    mdt.msgCode = MessageCode.Success;
                    mdt.status = HttpStatusCode.OK;
                    mdt.message = "Record found";
                    mdt.transactionObject = packageLists;
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
        /// It will return Top 3 configurations for respective package and User
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        private MDTTransactionInfo GetConfigurationList(int userID, int packageID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<ConfigurationList> configLists = null;
                List<SqlParameter> prm = DatabaseSettings.BindParamers(APIHelper.getLatestConfigurationsParameters, userID.ToString() + "~||~" + packageID.ToString() + "~||~" + ConfigurationManager.AppSettings["NumberOfConfiguration"].ToString());
                int StatusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getLatestConfigurations, out StatusValue, prm);
                DataTable dt;
                if (StatusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        configLists = from d in dt.AsEnumerable()
                                      select new ConfigurationList
                                      {
                                          CONFIGURATION_ID = d.Field<int>("CONFIGURATION_ID"),
                                          CONFIGURATION_NAME = d.Field<string>("CONFIGURATION_NAME"),
                                          PACKAGE_ID = d.Field<int>("PACKAGE_ID")
                                      };
                    }
                    mdt.msgCode = MessageCode.Success;
                    mdt.status = HttpStatusCode.OK;
                    mdt.message = "Record found";
                    mdt.transactionObject = configLists;
                }
                else if (StatusValue == 5 || StatusValue == 6)
                {
                    mdt = DatabaseSettings.GetTransObject(null, StatusValue, "", ds);
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
    }
}
