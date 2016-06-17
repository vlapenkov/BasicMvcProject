using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MVC5.Controllers;
using MVC5.Models;

namespace MVC5
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "TestService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы TestService.svc или TestService.svc.cs в обозревателе решений и начните отладку.
    public class TestService : ITestService
    {
        
        private IDbContext _dbContext;
       private IPartnerService _partnerService;

       /* public TestService()
        {
                
        } */
        public TestService(IDbContext dbContext,IPartnerService partnerService)
        {
           _dbContext = dbContext;
           _partnerService = partnerService;
        }
       

        public IEnumerable<Models.Producer> GetProducers()
        {
          //  return new AppDbContext().Producers.ToList();
                 return _dbContext.Producers.ToList();
        }

        public IEnumerable<Models.ProductDTO> GetProducts()
        {
            //  return new AppDbContext().Producers.ToList();
            return _dbContext.Products.Include(p=>p.Producer).Select(p=> new ProductDTO{Id = p.Id,Name = p.Name,ProducerName = p.Producer.Name,Price = p.Price, ReleaseDate = p.ReleaseDate}).ToList();
        }

        public string GetCulture()
        {
          
            return _partnerService.GetCulture();
        }

        public void DoWork()
        {
            throw new NotImplementedException();
        }


        public void CreateProducer(Producer producer)
        {
            _dbContext.Producers.Add(producer);
            _dbContext.SaveChanges();
        }
    }
}
