using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace Registro
{
    public partial class grafico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbAviso.Visible = false;

            if (!IsPostBack)
            {
                ComboModelo(string.Empty);
            }
        }

        private void ComboModelo(string tipo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();

                string sql = string.Empty;
                if (tipo == "FE")
                {
                    sql = @"SELECT DISTINCT SKUNO, TRIM(REPLACE(REPLACE(REPLACE(TRIM(CODENAME), 'FE',''),'-',''),'PCBA','' ))  CODENAME from sfccodelike where CATEGORY='PCBA'  ORDER BY SKUNO";
                }
                else if (tipo == "BE")
                {
                    sql = @"SELECT DISTINCT SKUNO, TRIM(REPLACE(REPLACE(REPLACE(TRIM(CODENAME), 'BE',''),'-',''),'PCBA','' ))  CODENAME from sfccodelike where CATEGORY='MODEL'  ORDER BY SKUNO";
                }
                else//RMA 
                {
                    sql = @"SELECT DISTINCT SKUNO, TRIM( REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CODENAME, 'BE',''),'-',''),'FE','' ),'SMT','' ),'PCBA','' )) CODENAME   from sfccodelike  ORDER BY SKUNO";
                }

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                cboModelo.DataSource = Objconn.Tabela;
                cboModelo.DataTextField = "SKUNO";
                cboModelo.DataValueField = "SKUNO";
                cboModelo.DataBind();
                cboModelo.Items.Insert(0, new ListItem("Selecione", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        protected void rdBtnLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdBtnLocal.SelectedValue == "FE")
            {
                ComboModelo("FE");
            }
            else if (rdBtnLocal.SelectedValue == "BE")
            {
                ComboModelo("BE");
            }
            else
            {
                ComboModelo(string.Empty);//RMA
            }
        }

        public void LimpaCampos()
        {
            cboModelo.SelectedIndex = 0;
            lbStatusImagem.Text = "-";
            lbStatusImagem.ForeColor = System.Drawing.Color.Black;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            #region UPLOAD IMAGEM
            //
            if (Page.IsValid)
            {
                if (uplImagem.HasFile)//verifica se ha arquivo selecionado
                {
                    string destinoUploadImagem = string.Empty;
                    //
                    if (rdBtnLocal.SelectedValue == "FE")
                    {
                        //DirectoryInfo Dir = new DirectoryInfo(ConfigurationManager.AppSettings["fe"]);
                        destinoUploadImagem = ConfigurationManager.AppSettings["fe"];//Dir.ToString();  //AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["fe"];
                    }
                    else if (rdBtnLocal.SelectedValue == "BE")
                    {
                        //DirectoryInfo Dir = new DirectoryInfo(ConfigurationManager.AppSettings["be"]);
                        destinoUploadImagem = ConfigurationManager.AppSettings["be"];//Dir.ToString();
                        //destinoUploadImagem = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["be"];
                    }
                    else
                    {
                        //DirectoryInfo Dir = new DirectoryInfo(ConfigurationManager.AppSettings["rma"]);
                        destinoUploadImagem = ConfigurationManager.AppSettings["rma"];//Dir.ToString();
                        //destinoUploadImagem = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["rma"];//RMA
                    }

                    //
                    string modelo = cboModelo.SelectedValue;
                    string imagem = modelo + Path.GetExtension(uplImagem.PostedFile.FileName);
                    //
                    if (Path.GetExtension(imagem) == ".png")
                    {
                        try
                        {  

                            //string lstProcs = Process.GetCurrentProcess().ProcessName;
                            //Encerra o processo
                            string nomeExecutavel = "WebDev.WebServer40";//"w3wp"; //".exe";
                            foreach (Process pr in Process.GetProcessesByName(nomeExecutavel))
                            {
                                if (!pr.HasExited)
                                    pr.Kill();
                            }
                            
                            if (File.Exists(destinoUploadImagem + imagem))
                                File.Delete(destinoUploadImagem + imagem);
                            //
                            uplImagem.PostedFile.SaveAs(destinoUploadImagem + imagem);
                            lbAviso.Visible = true;
                            lbAviso.Text = "Imagem salva com sucesso!";
                            //
                            LimpaCampos();
                        }
                        catch (Exception erro)
                        {
                            lbAviso.Visible = true;
                            lbAviso.Text = erro.Message + "_" + erro.Source;
                        }

                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.Text = "ERRO:: O arquivo permitido somente extenção .png";

                    }
                }
                else
                {
                    lbAviso.Visible = true;
                    lbAviso.Text = "ERRO:: Selecione a imagem do gráfico";
                }
            }

            #endregion
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("default");
        }

        protected void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string arquivo = string.Empty;
            string nomeImagem = cboModelo.SelectedValue;
            //
            if (!string.IsNullOrEmpty(nomeImagem))
            {
                nomeImagem = nomeImagem + ".png";
                //
                if (rdBtnLocal.SelectedValue == "FE")
                {
                    arquivo = ConfigurationManager.AppSettings["fe"] + nomeImagem;
                    if (File.Exists(arquivo))
                    {
                        lbStatusImagem.Text = "SIM";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lbStatusImagem.Text = "NÃO";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (rdBtnLocal.SelectedValue == "BE")
                {
                    arquivo = ConfigurationManager.AppSettings["be"] + nomeImagem;
                    if (File.Exists(arquivo))
                    {
                        lbStatusImagem.Text = "SIM";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lbStatusImagem.Text = "NÃO";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    arquivo = ConfigurationManager.AppSettings["rma"] + nomeImagem;
                    if (File.Exists(arquivo))
                    {
                        lbStatusImagem.Text = "SIM";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lbStatusImagem.Text = "NÃO";
                        lbStatusImagem.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                LimpaCampos();
            }
        }


    }
}