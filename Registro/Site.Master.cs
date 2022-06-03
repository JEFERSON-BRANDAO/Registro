using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 01/06/2020
// DESCRIPTION  : Sistema para registro das observações de falhas bot e top e cadastro/edição valor planejado
// SPECIAL NOTES:
// ===============================
// Change History: Inclusa opção para usuário diferenciar valor planejado para lado BOT e TOP
// Date:05/29/2020 
//==================================

namespace Compras
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);//limpa cache das paginas     

            int anoCriacao = 2020;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            //
            lbRodape.Text = "Copyright © " + anoCriacao + texto + " v1.0.0.1";

            string saudacao = "";

            if (DateTime.Now.Hour >= 00 && DateTime.Now.Hour <= 11)
            {
                saudacao = "Bom dia, ";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour <= 17)
            {
                saudacao = "Boa tarde, ";
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 23)
            {
                saudacao = "Boa noite, ";

            }
            //
            LbUsuario.Text = saudacao + " " + getUsuarioLogado();
            //
            if (!IsPostBack)
            {
                lbData.Text = DateTime.Now.ToString("D");

            }
            //
            MontarMenu();

        }

        public void MontarMenu()
        {
            #region MENU

            //NavigationMenu.Items.Add(new MenuItem("Cadastro", "cadastro", null, null));

            //NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo usuário", "", null, "listaUsuario.aspx"));
            //NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo tipo usuário", "", null, "listaTipoUsuario.aspx"));
            //NavigationMenu.FindItem("cadastro").ChildItems.Add(new MenuItem("Novo Centro de custo", "", null, "listaCentroCusto.aspx"));


            string Id = "0";

            try
            {
                Id = (string)Session["id"];
            }
            catch (Exception)
            {
                Session["id"] = "0";
                Response.RedirectToRoute("login");
            }

            Permissao acessoMenu = new Permissao();
            List<Classes.Menu> lista = new List<Classes.Menu>();
            lista = ((List<Classes.Menu>)acessoMenu.ListaMenu(Id.ToString()));
            for (int i = 0; i < lista.Count; i++)
            {
                string menu = lista[i].menu.ToString();
                string menuItem = lista[i].menuItem.ToString();
                string pagina = lista[i].Pagina.ToString().Replace(".aspx", string.Empty);//remove extensão da página refenciando na GLOBAL.asax
                string titulo = lista[i].Titulo.ToString();

                string MenuPrincipal = string.Empty;
                //
                if (NavigationMenu.Items.Count == 0)//se menu principal ainda nao foi criado
                {
                    NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                }
                else
                {
                    try
                    {
                        MenuPrincipal = NavigationMenu.FindItem(menuItem).Text;
                    }
                    catch
                    {
                        NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                        MenuPrincipal = NavigationMenu.FindItem(menuItem).Text;
                    }

                    if (MenuPrincipal.ToUpper() != menu.ToUpper())// se menu principal ja existe, nao deixa criar
                    {
                        NavigationMenu.Items.Add(new MenuItem(menu, menuItem, null, null));
                    }
                }
                //item menu
                NavigationMenu.FindItem(menuItem).ChildItems.Add(new MenuItem(titulo, "", null, pagina));
                //NavigationMenu.GetRouteUrl("listaUsuario", "~/listaUsuario.aspx");
            }


            #endregion
        }

        protected string getUsuarioLogado()
        {
            string Id = "0";
            //
            try
            {
                Id = (string)Session["id"];
            }
            catch (Exception)
            {
                Session["id"] = "0";
                //
                Response.RedirectToRoute("login");
            }
            //
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            Objconn.Conectar();
            Objconn.Parametros.Clear();
            Objconn.SetarSQL("SELECT LOGONNAME AS LOGIN FROM EUSER WHERE LOGONNAME = '" + Id + "'");
            Objconn.Executar();
            Objconn.Desconectar();
            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return Objconn.Tabela.Rows[0]["LOGIN"].ToString();
            }

            Session["id"] = "0";
            //Redireciona para Login.aspx
            Response.RedirectToRoute("login");

            return "Não Logado.";

        }

        protected bool UsuarioLogado()
        {
            string Id = "0";
            //
            try
            {
                Id = Session["id"].ToString();
            }
            catch (Exception)
            {
                Session["id"] = "0";
                //Redireciona para Login.aspx
                Response.RedirectToRoute("login");
            }
            //verifica se usuario está logado
            if (Id == "1")
            {
                return true;
            }
            else
            {
                Session["id"] = "0";
                //Redireciona para Login.aspx
                Response.RedirectToRoute("login");
                return false;
            }

        }
    }
}
