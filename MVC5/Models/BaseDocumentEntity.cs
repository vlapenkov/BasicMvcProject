using MVC5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace YstStore.Domain
{
    /// <summary>
    /// interface for documents
    /// </summary>
   public interface IBaseDocumentEntity
    {
        Guid GuidIn1S { get; set; }
        string NumberIn1S { get; set; }
         DateTime Date { get; set; }
    }

    /// <summary>
    /// One base class for all entities
    /// </summary>
   public abstract class BaseDocumentEntity<TDetails> where TDetails : BaseDocumentDetail

   {
       /// <summary>
       /// create details in document entity
       /// </summary>
       public BaseDocumentEntity()
       {
           Details = new List<TDetails>();
       }

       [Key]
     public  Guid GuidIn1S { get; set; }

       [StringLength(8)]
     public string NumberIn1S { get; set; }
     public DateTime Date { get; set; }

     [Index]
     [MaxLength(7)]
     public string PartnerId { get; set; }

     public decimal TotalSum { get; set; }

     [MaxLength(200)]
     public string Comments { get; set; }

     public virtual ICollection<TDetails> Details { get; set; }

     [NotMapped]
     public int NumberOfItems
     { get {
         return Details.Count;
     } }

      [NotMapped]
     public decimal TotalSumOfDetails
     {
         get
         {
             return Details.Sum(d=>d.Count*d.Price);
         } 
     
     }
     
   }

   public abstract class BaseDocumentDetail
   {
       //  public long OrderDetailId { get; set; }
       [Key, Column(Order = 1)]
       public Guid GuidIn1S { get; set; }

       [Key, Column(Order = 2)]
       public int RowNumber { get; set; }
       [ForeignKey("Product")]
       public int ProductId { get; set; }
       public int Count { get; set; }
       public decimal Price { get; set; }
       [ForeignKey("ProductId")]
       public virtual Product Product { get; set; }
       //public virtual BaseDocumentEntity Document { get; set; }


   }

}
