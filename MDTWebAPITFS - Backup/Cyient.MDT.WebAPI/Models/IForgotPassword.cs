﻿using Cyient.MDT.WebAPI.Core.Common;
using Cyient.MDT.WebAPI.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Models
{
    public interface IForgotPassword
    {
        MDTTransactionInfo ForgotPassword(ForgotPassword forgotPassword);
    }
}