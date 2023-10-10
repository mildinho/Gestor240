using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


/*
 * https://gist.github.com/mykeels/59d1774b94248ded2dfe34daa45e8381
 * https://stackoverflow.com/questions/37329354/how-to-use-ihttpcontextaccessor-in-static-class-to-set-cookies
 */


namespace Web.Biblioteca.Notification
{
    public class AlertHandler
    {

        private const string _key = "alert.notification";
        private static IHttpContextAccessor _sessao;


        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            _sessao = accessor;
        }


        public static void Add(AlertNotification notification)
        {
            string obj = JsonConvert.SerializeObject(notification);
            _sessao.HttpContext.Session.SetString(_key, obj);


        }

        private static AlertNotification Get_Notification()
        {
            string obj = _sessao.HttpContext.Session.GetString(_key);

            if (obj != null)
                return JsonConvert.DeserializeObject<AlertNotification>(obj);

            return new AlertNotification();
        }

        public static string RenderNotifications()
        {
            var obj = Get_Notification();
            string ret = "";

            if (obj.Message != null)
            {
                ret = "<script>\n" +
                                "$(document).ready(function () {\n" +
                                String.Join("", "alertsw('" + obj.Message + "','" + obj.Type.ToString().ToLower() + "');") +
                                "});\n" +
                            "</script>";

            }
            _sessao.HttpContext.Session.Remove(_key);
            return ret;
        }

    }

    public class AlertNotification
    {
        public string? Message { get; set; }
        public NotificationType Type { get; set; }

        public enum NotificationType
        {
            Error,
            Success,
            Warning,
            Info,
            Question
        }

        public static void Success(string message)
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Success;
            alertObj.Message = message;
            AlertHandler.Add(alertObj);
        }

        public static void Warning(string message)
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Warning;
            alertObj.Message = message;
            AlertHandler.Add(alertObj);
        }

        public static void Error(string message)
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Error;
            alertObj.Message = message;
            AlertHandler.Add(alertObj);
        }

        public static void Info(string message)
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Info;
            alertObj.Message = message;
            AlertHandler.Add(alertObj);
        }

        public static void Question(string message)
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Question;
            alertObj.Message = message;
            AlertHandler.Add(alertObj);
        }

        public static void Clear()
        {
            var alertObj = new AlertNotification();
            alertObj.Type = NotificationType.Question;
            alertObj.Message = null;
            AlertHandler.Add(alertObj);
        }

    }
}
