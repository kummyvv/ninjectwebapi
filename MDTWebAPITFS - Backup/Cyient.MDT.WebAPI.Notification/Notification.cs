using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cyient.MDT.WebAPI.Notification.Interface;
using Cyient.MDT.WebAPI.Notification.Product;
namespace Cyient.MDT.WebAPI.Notification
{
    public class Notification
    {
        private IMessager _messager;
        public Notification(IMessager messager)
        {
            _messager = messager;
        }

        public bool DoNotify(SendMailRequest sendMailRequest)
        {
            try
            {
                return _messager.SendNotification(sendMailRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}