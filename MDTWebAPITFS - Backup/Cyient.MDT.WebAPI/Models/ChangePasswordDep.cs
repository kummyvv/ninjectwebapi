using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cyient.MDT.WebAPI.Core.Entities.Account;
namespace Cyient.MDT.WebAPI.Models
{
    public class ChangePasswordDep : IChangePassword
    {
        UserAccountConcrete _service = new UserAccountConcrete();
        
       

        public MDTTransactionInfo ChangePassword(Core.Entities.Account.ChangePassword changePassword)
        {
            return _service.ChangePassword(changePassword);
        }
    }
}