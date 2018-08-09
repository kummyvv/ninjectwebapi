using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cyient.MDT.Infrastructure.Concrete.Account;
using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.Account;

namespace Cyient.MDT.WebAPI.Models
{
    public class ForgotPasswordDep : IForgotPassword
    {
        UserAccountConcrete _service = new UserAccountConcrete();

        public MDTTransactionInfo ForgotPassword(ForgotPassword forgotPassword)
        {
            return _service.ForgotPassword(forgotPassword);
        }
    }
}