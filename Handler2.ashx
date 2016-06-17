<%@ WebHandler Language="C#" Class="GenericHandler1" %>

using System;
using System.Web;

public class GenericHandler1 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Привет всем!");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}