using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using WcfBinaryRestful.Contracts;

namespace WcfBinaryRestful.Service
{
    public class Service : IService
    {
        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method = "*", UriTemplate = "readimg")]
        public System.IO.Stream ReadImg()
        {
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, "down.jpg");
            System.IO.FileStream fs = new System.IO.FileStream(imgFilePath, System.IO.FileMode.Open);
            System.Threading.Thread.Sleep(2000);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return fs;
        }
        [WebInvoke(Method = "*", UriTemplate = "receiveimg")]
        public void ReceiveImg(System.IO.Stream stream)
        {
            Debug.WriteLine(WebOperationContext.Current.IncomingRequest.ContentType);
            System.Threading.Thread.Sleep(2000);
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, "up.jpg");
            Image bmp = Bitmap.FromStream(stream);
            bmp.Save(imgFilePath);
        }

        [WebInvoke(Method = "*",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/receiveimage/{id}")]
        public void ReceiveImage(string id, System.IO.Stream stream)
        {
            Debug.WriteLine(WebOperationContext.Current.IncomingRequest.ContentType);
            System.Threading.Thread.Sleep(2000);
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, id + ".jpg");
            Image bmp = Bitmap.FromStream(stream);
            bmp.Save(imgFilePath);
        }

        [WebInvoke(Method = "*",
      BodyStyle = WebMessageBodyStyle.Wrapped,
      RequestFormat = WebMessageFormat.Json,
      UriTemplate = "/readimage/{filename}")]
        public System.IO.Stream ReadImage(string filename)
        {
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, filename);
            System.IO.FileStream fs = new System.IO.FileStream(imgFilePath, System.IO.FileMode.Open);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return fs;
        }

        [WebInvoke(Method = "*", 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            RequestFormat = WebMessageFormat.Json, 
            UriTemplate = "/hello/{hi}")]
        public string Hello(string hi)
        {
            string methodname = GetMethodName();

            OperationContext context = OperationContext.Current;
            MessageProperties properties = context.IncomingMessageProperties;
            
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            
            Console.WriteLine(string.Format("{3} - Hello {0},You are from {1}:{2}", hi, endpoint.Address, endpoint.Port, methodname));
            return string.Format("{3} - Hello {0},You are from {1}:{2}", hi, endpoint.Address, endpoint.Port, methodname);
        }

        public static string GetMethodName()
        {
            var method = new StackFrame(1).GetMethod(); // 这里忽略1层堆栈，也就忽略了当前方法GetMethodName，这样拿到的就正好是外部调用GetMethodName的方法信息
            var property = (
                      from p in method.DeclaringType.GetProperties(
                               BindingFlags.Instance |
                               BindingFlags.Static |
                               BindingFlags.Public |
                               BindingFlags.NonPublic)
                      where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                      select p).FirstOrDefault();
            return property == null ? method.Name : property.Name;
        }
    }
}
