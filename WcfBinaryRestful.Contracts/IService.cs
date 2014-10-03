using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfBinaryRestful.Contracts
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string Hello(string hi);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        System.IO.Stream ReadImg();

        [OperationContract]
        void ReceiveImg(System.IO.Stream stream);

        [OperationContract]
        void ReceiveImage(string id, System.IO.Stream stream);


        [OperationContract]
        System.IO.Stream ReadImage(string filename);
    }

}
