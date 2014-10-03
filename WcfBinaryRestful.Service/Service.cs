using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using WcfBinaryRestful.Contracts;

namespace WcfBinaryRestful.Service
{
    public class Service:IService
    {
        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        [WebInvoke(Method="*", UriTemplate="readimg")]
        public System.IO.Stream ReadImg()
        {
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, "down.jpg");
            System.IO.FileStream fs = new System.IO.FileStream(imgFilePath, System.IO.FileMode.Open);
            System.Threading.Thread.Sleep(2000);
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return fs;
        }
        [WebInvoke(Method="*", UriTemplate="receiveimg")]
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
            BodyStyle=WebMessageBodyStyle.Wrapped,
            RequestFormat=WebMessageFormat.Json,
            UriTemplate = "/receiveimage/{id}")]
        public void ReceiveImage(string id, System.IO.Stream stream)
        {
            Debug.WriteLine(WebOperationContext.Current.IncomingRequest.ContentType);
            System.Threading.Thread.Sleep(2000);
            string runDir = "d:\\";
            string imgFilePath = System.IO.Path.Combine(runDir, id+".jpg");
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
    }
}
