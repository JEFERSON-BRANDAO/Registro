using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace Registro
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("default", "Home", "~/Default.aspx");
            routes.MapPageRoute("login", "Login", "~/login.aspx");
            //
            routes.MapPageRoute("listaObservacao", "ListaObservacao", "~/listaObservacao.aspx");
            routes.MapPageRoute("novaObservacao", "NovaObservacao", "~/novaObservacao.aspx");
            //
            routes.MapPageRoute("listaPlanejado", "ListaPlanejado", "~/listaPlanejado.aspx");
            routes.MapPageRoute("novoPlanejado", "NovoPlanejado", "~/novoPlanejado.aspx");
            //
            routes.MapPageRoute("aniversariante", "Aniversariante", "~/aniversariante.aspx");
            //
            routes.MapPageRoute("grafico", "Grafico", "~/grafico.aspx");

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
