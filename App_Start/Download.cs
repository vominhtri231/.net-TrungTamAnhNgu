using System;
using System.Web;
using System.Web.Routing;

namespace TrungTamAnhNgu.App_Start
{
    public class Download : IHttpHandler
    {
        public RequestContext requestContext;

        public Download(RequestContext requestContext)
        {
            this.requestContext = requestContext;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
           
            HttpResponse response = HttpContext.Current.Response;
            string fileName = HttpContext.Current.Request.QueryString["file"];
            string filePath = "~/Content/";
           
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition",
                               "attachment; filename=" + fileName + ";");
            try
            {
                response.TransmitFile(context.Server.MapPath(filePath + fileName));
            }
            catch
            {

            }
            
            response.Flush();
            response.End();
           
            
        }

     
    }
}
