using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data;

namespace Registro
{
    public partial class aniversariante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaGrid(string.Empty);
        }

        private bool ExiteFuncionario(string matricula)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"SELECT REGISTRATION AS MATRICULA 
                                    FROM SFCRUNTIME.NIVER WHERE REGISTRATION='" + matricula + "'";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
            }
            finally
            {
                Objconn.Desconectar();
            }

            if (Objconn.Tabela.Rows.Count > 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void CarregaGrid(string matricula)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                string mes = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                //
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                string sql = string.Empty;
                //
                if (string.IsNullOrEmpty(matricula))
                {
                    sql = @"SELECT NAME AS NOME, REGISTRATION AS MATRICULA, DEPARTMENT AS SETOR, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'DD/MM/YYYY') AS DATAANIVERSARIO, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'MM/DD') AS MESDIAANIVERSARIO 
                                    FROM SFCRUNTIME.NIVER ORDER BY NOME, BDATE";
                                   // WHERE to_char(to_date(BDATE,'YYYY-MM-DD'), 'MM')='" + mes + "'";
                }
                else
                {
                    sql = @"SELECT NAME AS NOME, REGISTRATION AS MATRICULA, DEPARTMENT AS SETOR, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'DD/MM/YYYY') AS DATAANIVERSARIO, 
                                    to_char(to_date(BDATE,'YYYY-MM-DD'), 'MM/DD') AS MESDIAANIVERSARIO 
                                    FROM SFCRUNTIME.NIVER
                                    WHERE REGISTRATION='" + matricula + "' "+
                                    "ORDER BY NOME, BDATE";
                }

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    gridDados.DataSource = Objconn.Tabela;
                    gridDados.DataBind();
                    //
                    for (int index = 0; index < Objconn.Tabela.Rows.Count; index++)
                    {
                        for (int linha = 0; linha < gridDados.Rows.Count; linha++)
                        {
                            if (Objconn.Tabela.Rows[index]["setor"].ToString() == ((Label)gridDados.Rows[linha].Cells[0].FindControl("lbSetor")).Text)
                            {
                                ((DropDownList)gridDados.Rows[linha].Cells[0].FindControl("cboSetor")).SelectedValue = Objconn.Tabela.Rows[index]["setor"].ToString();
                            }
                        }

                        //grid dados
                        //GridViewRow gvRow = gridDados.Rows[index];
                        //((DropDownList)gvRow.FindControl("cboSetor")).DataSource = ComboSetor();
                        //((DropDownList)gvRow.FindControl("cboSetor")).DataTextField = "Descricao";
                        //((DropDownList)gvRow.FindControl("cboSetor")).DataValueField = "Id";
                        //((DropDownList)gvRow.FindControl("cboSetor")).DataBind();
                        //((DropDownList)gvRow.FindControl("cboSetor")).Items.Insert(0, new ListItem("Selecione", string.Empty));
                        //((DropDownList)gvRow.FindControl("cboSetor")).SelectedValue = Objconn.Tabela.Rows[index]["setor"].ToString();


                        //
                        //((TextBox)gvRow.FindControl("txtMatricula")).Text = Objconn.Tabela.Rows[index]["matricula"].ToString();
                        //((TextBox)gvRow.FindControl("txtNome")).Text = Objconn.Tabela.Rows[index]["nome"].ToString();
                        //((TextBox)gvRow.FindControl("txtDataAniversario")).Text = Objconn.Tabela.Rows[index]["dataAniversario"].ToString();

                    }
                }
                else
                {
                    gridDados.DataSource = null;
                    gridDados.DataBind();
                }

            }
            finally
            {
                Objconn.Desconectar();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string matricula = txtMatricula.Text.Trim();
            //
            lbAviso.Visible = false;
            //
            if (Page.IsValid)
            {
                if (!ExiteFuncionario(matricula))
                {
                    string nome = txtNome.Text.ToUpper();
                    string setor = cboSetor.SelectedValue;
                    string dataAniversario = txtDataAniversario.Text;
                    dataAniversario = DateTime.Parse(dataAniversario).ToString("yyyy-MM-dd");
                    //
                    OLEDBConnect Objconn = new OLEDBConnect();
                    //
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        string sql = @"INSERT INTO SFCRUNTIME.NIVER (NAME, REGISTRATION, DEPARTMENT, BDATE)VALUES('" + nome + "','" + matricula + "','" + setor + "','" + dataAniversario + "')";
                        Objconn.SetarSQL(sql);
                        //
                        Objconn.Executar();

                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }

                    if (Objconn.Isvalid)
                    {
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        lbAviso.Visible = true;
                        lbAviso.Text = "Registro realizado com sucesso.";
                        CarregaGrid(string.Empty);//refresh
                        //
                        LimpaDados();
                    }
                    else
                    {
                        lbAviso.ForeColor = System.Drawing.Color.Red;
                        lbAviso.Visible = true;
                        lbAviso.Text = "ERRO: " + Objconn.Message;
                    }
                }
                else
                {
                    lbAviso.ForeColor = System.Drawing.Color.Red;
                    lbAviso.Visible = true;
                    lbAviso.Text = "ERRO: Matrícula existente.";
                    CarregaGrid(matricula);//refresh

                }
            }

        }

        public void LimpaDados() 
        {
            cboSetor.SelectedIndex = 0;
            txtMatricula.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtDataAniversario.Text = string.Empty;
        }

        protected void gridDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region REMOVE
            //
            if (e.CommandName == "remover")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //
                OLEDBConnect Objconn = new OLEDBConnect();
                if (gridDados.Rows.Count > 0)
                {
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        string matricula = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtMatricula")).Text) ? "" : ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtMatricula")).Text.Trim();

                        string Sql = @"DELETE FROM  SFCRUNTIME.NIVER
                                              WHERE  REGISTRATION = '" + matricula + "'";

                        //
                        Objconn.SetarSQL(Sql);
                        Objconn.Executar();

                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }

                    //
                    if (Objconn.Isvalid)
                    {
                        lbAviso.Visible = true;
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        lbAviso.Text = "Registro deletado com sucesso!";
                        CarregaGrid(string.Empty);//refresh
                        //
                        LimpaDados(); 
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.ForeColor = System.Drawing.Color.Red;
                        lbAviso.Text = "ERRO: " + Objconn.Message;

                    }
                }

            }
            //
            #endregion
            //
            #region EDITA
            //
            if (e.CommandName == "editar")
            {
                if (Page.IsValid)
                {
                    OLEDBConnect Objconn = new OLEDBConnect();
                    //
                    try
                    {

                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        //
                        string matricula = string.Empty;
                        string setor = string.Empty;
                        string nome = string.Empty;
                        string dataAniversario = string.Empty;
                        //
                        setor = string.IsNullOrEmpty(((DropDownList)gridDados.Rows[index].Cells[0].FindControl("cboSetor")).SelectedValue) ? "" : ((DropDownList)gridDados.Rows[index].Cells[0].FindControl("cboSetor")).SelectedValue;
                        matricula = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtMatricula")).Text) ? "" : ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtMatricula")).Text.Trim();
                        nome = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtNome")).Text) ? "" : ((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtNome")).Text.Trim();
                        dataAniversario = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtDataAniversario")).Text) ? "" : ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtDataAniversario")).Text.Trim();
                        dataAniversario = DateTime.Parse(dataAniversario).ToString("yyyy-MM-dd");
                        //
                        try
                        {

                            Objconn.Conectar();
                            Objconn.Parametros.Clear();
                            //
                            string Sql = @"UPDATE  SFCRUNTIME.NIVER 
                                             SET REGISTRATION  ='" + matricula.Trim() + "', " +
                                                     "DEPARTMENT   ='" + setor + "', " +
                                                     "NAME         ='" + nome.ToUpper() + "', " +
                                                     "BDATE        ='" + dataAniversario + "'" +
                                                  " WHERE REGISTRATION   = '" + matricula + "'";

                            Objconn.SetarSQL(Sql);
                            //
                            Objconn.Executar();

                        }
                        finally
                        {
                            Objconn.Desconectar();
                        }

                        if (Objconn.Isvalid)
                        {
                            lbAviso.Text = "Alteração realizada com sucesso!";
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            lbAviso.Visible = true;
                            CarregaGrid(string.Empty);//refresh no grid
                            //
                            LimpaDados();

                        }
                        else
                        {
                            lbAviso.Text = "ERRO: " + Objconn.Message;
                            lbAviso.ForeColor = System.Drawing.Color.Red;
                            lbAviso.Visible = true;
                        }


                    }
                    catch (Exception erro)
                    {
                        lbAviso.Visible = true;
                        lbAviso.ForeColor = System.Drawing.Color.Red;
                        lbAviso.Text = "Erro::" + erro.Message;
                    }

                }
            }

            #endregion
        }

        protected void gridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDados.PageIndex = e.NewPageIndex;
            CarregaGrid(string.Empty);//refresh
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("default");
        }

    }
}