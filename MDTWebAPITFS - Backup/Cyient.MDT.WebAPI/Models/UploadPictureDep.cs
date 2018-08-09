using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Cyient.MDT.WebAPI.Models
{
    public class UploadPictureDep : IUploadPicture
    {
        UserAccountConcrete _service = new UserAccountConcrete();

        public IHttpActionResult UploadPic(int UserID)
        {
            MDTTransactionInfo tInfo = null;
            try
            {
                string ProfilePicPath;
                if (UserID == 0)
                    tInfo = new MDTTransactionInfo { msgCode = MessageCode.Failed, message = "Invalid Data", status = HttpStatusCode.BadRequest };
                else
                {
                    var httpRequest = HttpContext.Current.Request;
                    string Status = string.Empty;
                    if (httpRequest.Files.Count > 0)
                    {
                        var docfiles = new List<string>();
                        foreach (string file in httpRequest.Files)
                        {
                            var postedFile = httpRequest.Files[file];

                            string filename = "UserPic_" + UserID.ToString() + Path.GetExtension(postedFile.FileName).ToLower().Trim();
                            var filePath = HttpContext.Current.Server.MapPath("~/Uploads/ProfilePics/" + filename);
                            postedFile.SaveAs(filePath);
                            docfiles.Add(filePath);

                            ProfilePicPath = "Uploads/ProfilePics/" + filename;

                            tInfo = _service.UploadProfilePic(UserID, ProfilePicPath);
                        }
                    }
                    else
                    {
                        tInfo = new MDTTransactionInfo { msgCode = MessageCode.Success, message = "File uploading failed.", status = HttpStatusCode.OK };

                    }
                }

            }
            catch (Exception ex)
            {
                Logger Log = new Logger();
                Log.WriteErrorLog(ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine);
                Log.WriteErrorLog(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);

                tInfo = new MDTTransactionInfo { msgCode = MessageCode.Failed, message = ex.Message, status = HttpStatusCode.InternalServerError };
            }
            return null;
        }
    }
}