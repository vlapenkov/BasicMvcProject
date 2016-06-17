using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MVC5
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITestService2" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITestService2
    {
      //  [OperationContract]
       // void DoWork();

         [OperationContract]
        IEnumerable<Producer> GetProducers();
    }
}
