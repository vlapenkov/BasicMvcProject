using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using YstStore.Domain;

namespace MVC5.Models
{

    public partial class Order : BaseDocumentEntity<OrderDetail>
    {

        [StringLength(50)]
        public string Username { get; set; }
     

        [DefaultValue(5)]
        public int DepartmentId { get; set; }

     
        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        //   [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
        public System.DateTime OrderDate { get; set; }


        [DisplayName("Дата отгрузки")]
        [DataType(DataType.Date)]
        public System.DateTime? DeliveryDate { get; set; }

        /*    [DisplayName("Комментарий к заказу")]
            [StringLength(250)]
            public string Comments { get; set; } */

        [DisplayName("В резерв")]
        public bool isReserve { get; set; }
              
    }


    public class OrderDetail : BaseDocumentDetail
    {
        public virtual Order Order { get; set; }
    }

    public class Sale : BaseDocumentEntity<SaleDetail>
    {
        /*  [Key]
          public Guid GuidIn1S { get; set; }

          [StringLength(7)]
            
          public string PartnerId { get; set; }  // link to partner

          [DisplayName("Номер в учетной базе")]
          [StringLength(8)]
            
          public string NumberIn1S { get; set; } */


        [DisplayName("Сумма")]
        [DefaultValue(0)]
        public decimal Total { get; set; }

        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd.MM.yyyy")]
        public System.DateTime SaleDate { get; set; }


        public Guid GuidOfOrderIn1S { get; set; }

        [DefaultValue(5)]
        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string Driver { get; set; }

        [StringLength(20)]
        public string PhoneNumberOfDriver { get; set; }

        [StringLength(20)]
        public string BrandOfAuto { get; set; }

        [StringLength(20)]
        public string RegNumOfAuto { get; set; }

        [StringLength(150)]
        public string DischargePoint { get; set; }


        /*     [DisplayName("Комментарий к заказу")]
             [StringLength(200)]
            
             public string Comments { get; set; } 

             public virtual ICollection<SaleDetail> SaleDetails { get; set; } */

    }


    public class SaleDetail : BaseDocumentDetail
    {
        public virtual Sale Sale { get; set; }
    }
    
    
    public class Movie:IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }


     [DataContract(Namespace = "", Name = "Product")]
    public class Product : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }
          [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
      //  [DataType(DataType.Date)]
         [DataMember(Name = "releasedate", EmitDefaultValue = false)]
        public DateTime ReleaseDate { get; set; }

         [DataMember(Name = "price", EmitDefaultValue = false)]
        public decimal Price { get; set; }
        public  virtual Producer Producer { get; set; }


    }

     public class Post
     {

         public Post()
         {
             Active = true;
             BeginDate = DateTime.Now;
         }
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }

         [DataType(DataType.DateTime)]
         public System.DateTime BeginDate { get; set; }

         [StringLength(Byte.MaxValue)]
         public string Title { get; set; }

         [DisplayName("Активность")]
         [Index]
         [DefaultValue(1)]
         public bool Active { get; set; }

         [StringLength(Int32.MaxValue)]
         public string Contents { get; set; }
     }


    public class Producer : IEntity
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
       


    }

    public class Site : IEntity
    {

        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }



    }

    public class Partner : IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Culture { get; set; }

    }
    
    


    public interface IEntity {
        int Id { get; set; }
    }

    public interface IDbContext {

        /*  IDbSet<TEntity> Set<TEntity>() where TEntity : class,IEntity;*/
        void Update<TEntity>(TEntity entity) where TEntity : class,IEntity;


       

        IDbSet<Movie> Movies { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Producer> Producers { get; set; }
        IDbSet<Site> Sites { get; set; }
        IDbSet<Partner> Partners { get; set; }
        IDbSet<Post> Posts { get; set; }
                
        
        int SaveChanges(); 
    
    }

   
    public class MemoryDbContext : IDbContext
    {


        
       public MemoryDbContext()
        {
            Movies = new MemDbSet<Movie>();
            Products = new MemDbSet<Product>();
                        
        } 
        public IDbSet<Movie> Movies
        { get; set; }

        public IDbSet<Product> Products
        { get; set; }

        public IDbSet<Producer> Producers
        { get; set; }

        public IDbSet<Partner> Partners
        { get; set; }

        public IDbSet<Site> Sites
        { get; set; }

        public IDbSet<Post> Posts
        { get; set; }

        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }

        /// <summary>
        ///     Особняком идет update, т.к. к нему надо обращаться через DbContext
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
/*
        public void Update<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
            //TEntity result=this.Set<TEntity>().First(p=>p.Id==entity.Id);

            TEntity result = (this.Set<TEntity>() as MemDbSet<TEntity>).Find(entity.Id);

            if (result == null) throw new NullReferenceException("Not found records with given Id");
            // если это один объект в памяти, то возврат
            if (result.Equals(entity)) return;

            // иначе копируем свойства 
            // в последствии лучше реализовать MemberwiseClone, но пока не меняет , надо разбираться
            PropMapper<TEntity>.CopyObjectProperties(result, entity);
            result=entity.CloneObject();


        } 
    */


        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Можно инкапсулировать ObservableCollection<T>, но IList проще  
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MemDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
       where TEntity : class,IEntity
    {
        IList<TEntity> _data;
        IQueryable _query;

        public MemDbSet()
        {
            _data = new List<TEntity>();
            _query = _data.AsQueryable();
        }

        public TEntity Find(int Id)
        {
            
          return  _data.SingleOrDefault(p => p.Id == Id);

        } 

                       

        public override TEntity Add(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            _data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

     /*   public ObservableCollection<T> Local
        {
            get { return _data; }
        } */

       

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

       


        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }






        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class AppDbContext :  IdentityDbContext<ApplicationUser>, IDbContext
    {
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }
        public IDbSet<Sale> Sales { get; set; }
        public IDbSet<SaleDetail> SaleDetails { get; set; } 
       

        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Producer> Producers { get; set; }
        public IDbSet<Partner> Partners { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Site> Sites { get; set; }

        public void Update<TEntity>(TEntity entity) where TEntity : class,IEntity
        {
          /*  Entry(entity).State = EntityState.Modified;
            SaveChanges(); */

            if (entity == null)
            {
                throw new ArgumentException("Cannot add a null entity.");
            }

            var entry = this.Entry<TEntity>(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = this.Set<TEntity>();
                TEntity attachedEntity = set.Local.SingleOrDefault(e => e.Id == entity.Id);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = this.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
                SaveChanges();
            }
        }

       public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
       {
       //    this.Configuration.LazyLoadingEnabled = false;

       } 
      /*  public new IDbSet<TEntity> Set<TEntity>() where TEntity : class,IEntity
        {
            return base.Set<TEntity>();
        }
       */

       public static AppDbContext Create()
       {
           return new AppDbContext();
       }
        

    }
}