using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Cyient.MDT.WebAPI.Core.Entities.BasicInput;
using Cyient.MDT.WebAPI.Core.Repository.BasicInput;
using Cyient.MDT.WebAPI.Core.Common;
using System.Net;

namespace Cyient.MDT.Infrastructure.Concrete.BasicInputs
{
    public class BasicInputConcrete : IBasicInputOptions
    {

        public BasicInputConcrete() { }
        /// <summary>
        /// This will return list of Basic input options available in database
        /// </summary>
        /// <returns></returns>
        public MDTTransactionInfo GetBasicInputs(int packageID, int configurationID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<BasicInput> basicInputs = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                //SqlParameter status = new SqlParameter("@Status", 0);
                //status.Direction = ParameterDirection.Output;
                //prm.Add(status);

                prm = DatabaseSettings.BindParamers(APIHelper.getBasicInputdParameters, packageID.ToString() + "~||~" + configurationID.ToString());

                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet(APIHelper.getBasicInput, out statusValue, prm);

                DataTable dt;

                if (statusValue == 1)
                {
                    mdt = new MDTTransactionInfo();
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        basicInputs = from d in dt.AsEnumerable()
                                      select new BasicInput
                                      {
                                          BASIC_INPUT_ID = d.Field<int>("BASIC_INPUT_ID"),
                                          DEFAULT_INPUT_OPT_ID = d.Field<int>("DEFAULT_INPUT_OPT_ID"),
                                          CONFIGURATION_ID = d.Field<int>("CONFIGURATION_ID"),
                                          UPDT_USER_ID = d.Field<int>("UPDT_USER_ID"),
                                          BASIC_INPUT_NAME = d.Field<string>("BASIC_INPUT_NAME"),

                                          BasicInputOptions = GetBasicInputOptions(d.Field<int>("BASIC_INPUT_ID")).transactionObject as IEnumerable<BasicInputOptions>
                                      };
                    }
                    if (basicInputs != null)
                    {
                        mdt.transactionObject = basicInputs;
                        mdt.status = HttpStatusCode.OK;
                        mdt.msgCode = MessageCode.Success;
                        mdt.message = "Fetched basic input detail successfully";
                    }
                    else
                    {
                        mdt.transactionObject = null;
                        mdt.status = HttpStatusCode.NotFound;
                        mdt.msgCode = MessageCode.Failed;
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
        private MDTTransactionInfo GetBasicInputOptions(int basicInputID)
        {
            MDTTransactionInfo mdt = new MDTTransactionInfo();
            try
            {
                IEnumerable<BasicInputOptions> basicInputOptions = null;
                List<SqlParameter> prm = new List<SqlParameter>();
                //SqlParameter Basic_Input_ID = new SqlParameter("@BASIC_INPUT_ID", basicInputID);
                //prm.Add(Basic_Input_ID);
                //SqlParameter status = new SqlParameter("@Status", 0);
                //status.Direction = ParameterDirection.Output;
                //prm.Add(status);

                prm = DatabaseSettings.BindParamers(APIHelper.getBasicInputDetailsParameters, basicInputID.ToString());

                int statusValue = 0;
                DataSet ds = DatabaseSettings.GetDataSet("sp_GetBasicInputDetails", out statusValue, prm);

                DataTable dt;
                if (statusValue == 1)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        basicInputOptions = from d in dt.AsEnumerable()
                                            select new BasicInputOptions
                                            {
                                                BASIC_INPUT_OPTN_ID = d.Field<int>("BASIC_INPUT_OPTN_ID"),
                                                INPUT_OPT_NAME = d.Field<string>("INPUT_OPT_NAME"),
                                                BASIC_INPUT_ID = d.Field<int>("BASIC_INPUT_ID"),
                                                //CONFIGURATION_ID = d.Field<int>("CONFIGURATION_ID"),
                                                //CONFIG_INPUT_OPT_ID = d.Field<int>("CONFIG_INPUT_OPT_ID"),
                                                //UPDT_USER_ID = d.Field<int>("UPDT_USER_ID")
                                            };
                    }
                    mdt.msgCode = MessageCode.Success;
                    mdt.status = HttpStatusCode.OK;
                    mdt.message = "Record found";
                    mdt.transactionObject = basicInputOptions;
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
    }
}
