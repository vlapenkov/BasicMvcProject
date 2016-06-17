<%@ WebHandler Language="C#" Class="GenericHandler2" %>

using System;
using System.Web;
using MVC5.Services;
using MVC5.Models;
using MVC5;
using System.Text;
using Microsoft.Web.WebSockets;
using System.Net.WebSockets;


public class MyWebSockethandler : WebSocketHandler
{
    private static WebSocketCollection clients = new WebSocketCollection();
    private string name;

    public override void OnError()
    {
        
    }
    public override void OnOpen()
    {
      //  name = this.WebSocketContext.QueryString["chatName"];
        name = "test";
        clients.Add(this);
        clients.Broadcast(name + " has connected");
        
        //base.OnOpen();
    }

    public override void OnMessage(string message)
    {
        clients.Broadcast(message);
       // base.OnMessage(message);
    }

    public override void OnClose()
    {
        clients.Remove(this);
    }

}
public class GenericHandler2 : IHttpHandler {
    
    
    public void ProcessRequest (HttpContext context) {
        if (context.IsWebSocketRequest)
            context.AcceptWebSocketRequest(new MyWebSockethandler()); 

       /* context.Response.Write("<H1>This is an HttpHandler Test.</H1>");
        context.Response.Write("<p>Your Browser:</p>");
        context.Response.Write("Type: " + context.Request.Browser.Type + "<br>");
        context.Response.Write("Version: " + context.Request.Browser.Version); */
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}