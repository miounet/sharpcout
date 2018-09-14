using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using qzxxiEntity;
using System.Runtime.Serialization.Json;

namespace Core.Comm
{

    public class ApiClient
    {
        public static T JsonToObj<T>(string jsonString)
        {

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {

                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);

            }

        }
        public static string ToJson(object jsonObject)
        {

            using (var ms = new MemoryStream())
            {

                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);

                return Encoding.UTF8.GetString(ms.ToArray());

            }

        }

        /// <summary>
        /// 获取dict更新地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static UpdateDictEnt GetDictUpdate(string url)
        {
            UpdateDictEnt c = new UpdateDictEnt();
            try
            {
                c.DictVersion = Win.WinInput.DictVersion;
                c.ProductVer = Program.ProductVer;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.GetUpdateDict.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<UpdateDictEnt>(reponse.Content);
                }
            }
            catch
            {
                c.UpdateFalg = 0;
            }
            return c;
        }


        /// <summary>
        /// 获取dict更新地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ColdDictEntity GetCloudDictUpdate(string url)
        {
            ColdDictEntity c = new ColdDictEntity();
            try
            {
                c.Pos = Win.WinInput.Input.ClouddDit.Count;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.GetCloudDict.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<ColdDictEntity>(reponse.Content);
                }
            }
            catch
            {
                c.NextHave = false; ;
            }
            return c;
        }

        public static void StartDictUpdate(string url)
        {
            UpdateDictEnt c = new UpdateDictEnt();
            try
            {
                c.DictVersion = Win.WinInput.DictVersion;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.StartUpdateDict.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<UpdateDictEnt>(reponse.Content);
                }
            }
            catch
            {
                c.UpdateFalg = 0;
            }
             
        }
        public static void EndDictUpdate(string url)
        {
            UpdateDictEnt c = new UpdateDictEnt();
            try
            {
                c.DictVersion = Win.WinInput.DictVersion;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.EndUpdateDict.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<UpdateDictEnt>(reponse.Content);
                }
            }
            catch
            {
                c.UpdateFalg = 0;
            }

        }

        public static long LastMsgid=0;
        public static InfoEntity GetMyInfo(string url)
        {
            InfoEntity c = new InfoEntity();
            c.LastMsgid=LastMsgid;
            try
            {
            
                c.ProVer = Program.ProductVer;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.GetMyInfo.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<InfoEntity>(reponse.Content);
                    LastMsgid=c.LastMsgid;
                }
                else
                    c.InfoStr = string.Empty;
            }
            catch
            {
                c.InfoStr = string.Empty;

            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ProDictEntity GetProDict(string url, string key, List<UpdateDictEnt> pl=null)
        {
            ProDictEntity c = new ProDictEntity();


            try
            {
                c.DictList = pl;
             
                c.SearchKey = key;
                RequestEntity r = new RequestEntity();
                r.OptCommand = OptCom.GetProDict.ToString();
                r.Content = ToJson(c);
                r.Content = Security.EncryptCommon(r.Content);
                IAASResponse reponse = IAASRequest.Reauest(url, RequestMethod.POST, "", "", ToJson(r));
                if (reponse.StatusCode == HttpStatusCode.OK)
                {
                    c = JsonToObj<ProDictEntity>(reponse.Content);
                }
            }
            catch
            {
                c.DictList = new List<UpdateDictEnt>();
            }
            return c;
        }


    }

    public enum RequestMethod
    {

        POST,

        GET,

        DELETE,

        PUT,

    }
    public enum IAASResponseStatus
    {

        OK,

        Failed,

        Exception

    }

    public class IAASResponse
    {

        public IAASResponseStatus Stauts { get; set; }

        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string InputTxt { get; set; }

        public string RequestUrl { get; set; }

    }

    public class IAASRequest
    {

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        //<summary>
        //本地的代理
        //</summary>
        private static readonly IWebProxy SystemProxy = WebRequest.GetSystemWebProxy();


        public static IAASResponse Reauest(

            string url,

            RequestMethod method,

            string user,

            string password,

            string sendData)
        {

            return Request(url, method, user, password, sendData, 20000);

        }

        public static IAASResponse Request(

           string url,  RequestMethod method, string user,  string password,

           string sendData, int timeOut = 5000)
        {

            IAASResponse result;

            HttpWebRequest request;

            HttpWebResponse response = null;

            result = new IAASResponse();

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = method.ToString();

                request.ContentType = "application/json";

                request.UserAgent = DefaultUserAgent;

                request.Proxy = SystemProxy;
 
                request.Credentials = new NetworkCredential(user, password);

                if (timeOut > 0)

                    request.Timeout = timeOut;
         
                AddHttpRequestParams(request, sendData, method);
 
                response = (HttpWebResponse)request.GetResponse();
 
                using (StreamReader reader = new StreamReader(

                     response.GetResponseStream(), System.Text.Encoding.UTF8))
                {

                    result.Content = reader.ReadToEnd();
                    result.Content = Security.DecryptCommon(result.Content.Substring(1,result.Content.Length-2));
                    reader.Close();

                }

                switch (method)
                {

                    case RequestMethod.GET:

                        result.Stauts = (response.StatusCode == HttpStatusCode.OK)

                            ? IAASResponseStatus.OK : IAASResponseStatus.Failed;

                        break;

                    case RequestMethod.POST:

                        result.Stauts = (response.StatusCode == HttpStatusCode.OK ||

                            response.StatusCode == HttpStatusCode.Created)

                            ? IAASResponseStatus.OK : IAASResponseStatus.Failed;

                        break;

                    case RequestMethod.DELETE:

                        result.Stauts = (response.StatusCode == HttpStatusCode.OK)

                            ? IAASResponseStatus.OK : IAASResponseStatus.Failed;

                        break;
                    case RequestMethod.PUT:

                        result.Stauts = (response.StatusCode == HttpStatusCode.OK ||

                            response.StatusCode == HttpStatusCode.Created)

                            ? IAASResponseStatus.OK : IAASResponseStatus.Failed;

                        break;
                }
            }

            catch (System.Net.WebException e)
            {
 
                response = e.Response as HttpWebResponse;
 

                if (response != null)
                {

                    result.StatusCode = response.StatusCode;

                    try
                    {

                        using (StreamReader reader = new StreamReader(

                             response.GetResponseStream(), System.Text.Encoding.UTF8))
                        {
                            result.Content = reader.ReadToEnd();
                            
                            reader.Close();
                        }
                    }
                    catch
                    {
                        result.Content = null;
                    }
                }

                result.Stauts = IAASResponseStatus.Exception;
 
                if (string.IsNullOrEmpty(result.Content))

                    result.Content = string.Format(

                       "{0}\r\n{1}", e.Message, e.StackTrace);
            }
            catch (Exception e)
            {
                result.Stauts = IAASResponseStatus.Exception;
                result.Content = string.Format(

                   "{0}\r\n{1}", e.Message, e.StackTrace);
            }

            finally
            {

                if (response != null)
                {

                    result.StatusCode = response.StatusCode;

                }

                result.InputTxt = sendData;

                result.RequestUrl = url;

            }

            return result;

        }


        /// <summary> 
        /// 为HttpRequest添加参数
        /// </summary> 
        /// <param name="request"></param> 
        /// <param name="sendData"></param> 
        public static void AddHttpRequestParams(HttpWebRequest request, String sendData, RequestMethod method)
        {

            byte[] data = null;



            if (method == RequestMethod.POST

                 || method == RequestMethod.PUT)
            {

                if (string.IsNullOrEmpty(sendData))

                    data = new byte[0];

                else

                    data = Encoding.UTF8.GetBytes(sendData);

            }

            else
            {

                if (string.IsNullOrEmpty(sendData))

                    return;

            }



            //byte[] data = Encoding.UTF8.GetBytes(sendData);

            request.ContentLength = data.Length;

            Stream requestStream = null;

            using (requestStream = request.GetRequestStream())
            {

                requestStream.Write(data, 0, data.Length);

            }

            requestStream.Close();

        }

    }
 
}
