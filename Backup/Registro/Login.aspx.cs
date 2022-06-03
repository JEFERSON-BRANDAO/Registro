using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data;

namespace Compras
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAviso.Visible = false;
            txtLogin.Focus();
            //
            if (!IsPostBack)
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
                    Response.RedirectToRoute("login");
                }
                //
                txtLogin.Focus();

            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                Session["id"] = "0";
            IniciaSessao();

        }

        protected void IniciaSessao()
        {
            try
            {
                string id = "0";
                id = Session["id"].ToString();
                if (id == "0")
                {
                }
            }
            catch (Exception msgERRO)
            {
                Session["id"] = "0";
                lbAviso.Text = msgERRO.Message; //mostra erro de excessao
            }

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            string disable = string.Empty;
            string usuario = txtLogin.Text.Trim().ToUpper();
            string senha = txtSenha.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(usuario))
            {
                lbAviso.Visible = true;
                lbAviso.Text = "Usuário ou senha não podem ser vázio.";
            }
            else
            {
                //criptografa a senha
                Criptografia objCript = new Criptografia();
                senha = objCript.Criptografar(senha);

                //
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT UPPER(LOGONNAME) AS ID,                                    
                                      DISABLED
                               FROM EUSER   
                               WHERE LOGONNAME = '" + usuario + "' AND PASSWORD = '" + senha + "'";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        string Id = Objconn.Tabela.Rows[0]["ID"].ToString();//LOGIN
                        Session["id"] = Id;
                        disable = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["DISABLED"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["DISABLED"].ToString();

                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "Usuário ou senha inválido.";
                    }
                }
                finally
                {
                    Objconn.Desconectar();
                }

                //
                if (disable.Equals("0"))
                {
                    // redireciona para Default.aspx (conforme a rota) 
                    Response.RedirectToRoute("default");

                }
                else if (disable.Equals("1"))
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "Usuário desativado.";
                }
            }
        }
    }
}