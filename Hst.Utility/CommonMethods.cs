using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Hst.Utility
{
    public static class CommonMethods
    {
        public static string GenerateRandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static void SendSMS(string MobileNumber, string Message, string templateId, string entityId)
        {
            try
            {

                //Your authentication key  
                string authKey = "c290c3N5ZG9KRUVPdWxkcG1mck0=";

                //Multiple mobiles numbers separated by comma  
                //string mobileNumber = "+91" + MobileNumber;
                string mobileNumber = MobileNumber;
                //Sender ID,While using route4 sender id should be 6 characters long.  
                string senderId = "ZOLTRY";
                string p_entity_id = entityId;// "1701159958452194346";
                string TEMPID = templateId;//"1207160138133770926";// "1207161725403956739";
                                           //Your message to send, Add URL encoding here.  
                string message = HttpUtility.UrlEncode(Message);
                //Prepare you post parameters  
                StringBuilder sbPostData = new StringBuilder();
                sbPostData.AppendFormat("action={0}", "send-sms");
                sbPostData.AppendFormat("&api_key={0}", authKey);
                sbPostData.AppendFormat("&to={0}", mobileNumber);
                sbPostData.AppendFormat("&from={0}", senderId);
                sbPostData.AppendFormat("&sms={0}", message);
                sbPostData.AppendFormat("&p_entity_id={0}", p_entity_id);
                sbPostData.AppendFormat("&temp_id={0}", TEMPID);


                //Call Send SMS API  
                string sendSMSUri = @"https://login.99smsservice.com/sms/api";
                //Create HTTPWebrequest  
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data  
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method  
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response  
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response  
                reader.Close();

                response.Close();

            }
            catch (Exception ex)
            { 
            
            }
        }
    }
}
