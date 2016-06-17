<%@ WebHandler Language="C#" Class="GenericHandler1" %>

using System;
using System.Web;

public class GenericHandler1 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
      /*  var db = new AppDbContext();
        var service = new ProductService(db);
        var products = service.GetProducts(_ => true);

        var builder = new StringBuilder();
        foreach (var product in products)
        {
            builder.AppendFormat("{0};{1};{2};{4}", product.Id, product.Name, product.Price, product.Producer.Name).AppendLine();
        } */
        context.Response.Clear();
        context.Response.ContentType = "text/csv";
        context.Response.AddHeader("Content-Disposition", "attachment;filename=myfilename.csv");
        context.Response.Write(builder.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}