using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MVC5.Models;

namespace MVC5
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "TestService2" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы TestService2.svc или TestService2.svc.cs в обозревателе решений и начните отладку.
    public class TestService2 : ITestService2
    {
        private IDbContext _dbContext;

        public TestService2(IDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public IEnumerable<Models.Producer> GetProducers()
        {
            //  return new AppDbContext().Producers.ToList();
            return _dbContext.Producers.ToList();
        }
    }
}
