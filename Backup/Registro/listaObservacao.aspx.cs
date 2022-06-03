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
    public partial class listaObservacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComboModelo("SMT");
                ComboLinha(cboProducao.SelectedValue, cboModelo.SelectedValue);
                ComboHorario();
                //
                ComboLado(cboProducao.SelectedValue, cboModelo.SelectedValue);
                CarregaGrid(string.Empty);
            }

        }

        private void ComboModelo(string TipoProducao)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                string sql = string.Empty;
                //
                if (TipoProducao.Equals("SMT"))
                {
                    sql = @"SELECT DISTINCT SKUNO, CODENAME from sfccodelike where CATEGORY='PCBA'";
                }
                else
                {
                    sql = @"SELECT DISTINCT SKUNO, CODENAME from sfccodelike where CATEGORY='MODEL'";
                }

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                cboModelo.DataSource = Objconn.Tabela;
                cboModelo.DataTextField = "CODENAME";
                cboModelo.DataValueField = "SKUNO";
                cboModelo.DataBind();
                cboModelo.Items.Insert(0, new ListItem("[Selecione]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        private void ComboLado(string TipoProducao, string modelo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                string sql = string.Empty;
                //string modelo = cboModelo.SelectedValue;
                //
                if (TipoProducao.Equals("SMT"))
                {
                    sql = @"SELECT a.EVENTPOINT  FROM SFCROUTEDEFB a
                                      INNER JOIN SFCCODELIKE  b  ON a.ROUTEID = b.SFCROUTE 
                                      WHERE b.SKUNO ='" + modelo + "' " +
                                      "AND a.EVENTPOINT IN ('SMT1', 'SMT2') " +
                                      "ORDER BY a.EVENTSEQNO";
                }
                else
                {
                    string SQL_ROKU = string.Empty;
                    SQL_ROKU = modelo.Equals("RU9026000643") ? " AND a.EVENTPOINT IN ('PT', 'FT', 'LASER') " : string.Empty;
                    //
                    sql = @"SELECT a.EVENTPOINT  FROM SFCROUTEDEFB a
                                      INNER JOIN SFCCODELIKE  b  ON a.ROUTEID = b.AUTOROUTE 
                                      WHERE b.SKUNO ='" + modelo + "' "
                                      + SQL_ROKU +
                                      "ORDER BY a.EVENTSEQNO";
                }

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                cboLado.DataSource = Objconn.Tabela;
                cboLado.DataTextField = "EVENTPOINT";
                cboLado.DataValueField = "EVENTPOINT";
                cboLado.DataBind();
                cboLado.Items.Insert(0, new ListItem("[Selecione]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        private void ComboLinha(string TipoProducao, string modelo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();

                string sql = string.Empty;
                if (TipoProducao.Equals("SMT"))
                {
                    sql = @"SELECT DISTINCT LINE_NAME FROM C_GW28_CONFIG WHERE SCADA_ID ='SMO-SMT' ORDER BY LINE_NAME";
                }
                else
                {
                    sql = @"SELECT DISTINCT LINE_NAME FROM C_GW28_CONFIG WHERE SCADA_ID  ='SMO-NEMO' ORDER BY LINE_NAME";
                }

                Objconn.SetarSQL(sql);
                //
                Objconn.Executar();
                //
                cboLinha.DataSource = Objconn.Tabela;
                cboLinha.DataTextField = "LINE_NAME";
                cboLinha.DataValueField = "LINE_NAME";
                cboLinha.DataBind();
                cboLinha.Items.Insert(0, new ListItem("[Selecione]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        private void ComboHorario()
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                string modelo = cboModelo.SelectedValue;
                //string lado = cboLado.SelectedValue;
                //string linha = cboLinha.SelectedValue;

                //string data = "";
                //data = txtDataProducao.Text.Trim();
                //if (!string.IsNullOrEmpty(data))
                //    data = DateTime.Parse(data).ToString("yyyy-MM-dd");


                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //string sql = @" SELECT distinct b.DATA2 modelo, a.uphdate, a.hourperiod, a.Eventpoint, a.productionline " +
                //                        "FROM SFCUPHRATEDETAIL a, R_AP_TEMP b  " +
                //                        "WHERE a.uphdate= '" + data + "' " +
                //                        "AND b.DATA1='ARRIS_PLAN' " +
                //                        "AND b.DATA2='" + modelo + "' " +
                //                        "AND a.eventpoint='" + lado + "' " +
                //                        "AND a.productionline='" + linha + "' " +
                //                        "AND a.WORKORDERNO IN (SELECT WORKORDERNO FROM MFWORKORDER  WHERE SKUNO IN (SELECT DISTINCT SKUNO FROM " +
                //                                           "(SELECT * FROM MFWORKORDER WHERE JOBSTARTED=1 AND CLOSED=0 AND SOFTWARE='SMTLOADING' " +
                //                                           "AND SKUNO = '" + modelo + "' " + " ORDER BY WORKORDERDATE DESC))) " +
                //                                      "GROUP BY a.Eventpoint,  a.uphdate, a.hourperiod, a.productionline,  b.DATA2, b.DATA5 " +
                //                       "order by a.hourperiod";
                string sql = @"select data4 as hourperiod from  R_AP_TEMP 
                                      where DATA1='ARRIS_PLAN' 
                                      and DATA2='" + modelo + "' " +
                                      "order by data4";
                Objconn.SetarSQL(sql);
                //
                Objconn.Executar();
                //
                cboHorario.DataSource = Objconn.Tabela;
                cboHorario.DataTextField = "hourperiod";
                cboHorario.DataValueField = "hourperiod";
                cboHorario.DataBind();
                cboHorario.Items.Insert(0, new ListItem("[Selecione]", string.Empty));
            }
            finally
            {
                Objconn.Desconectar();
            }

        }

        protected void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataProducao.Enabled = true;            
            cboLinha.Enabled = false;
            cboHorario.Enabled = false;
            //
            string modelo = cboModelo.SelectedValue;
            string producao = cboProducao.SelectedValue;
            ComboLado(producao, modelo);
            //
            CarregaGrid(modelo);
        }

        private bool CamposValidos()
        {
            if ((RegularExpressionValidatorDataProducao.Page.IsValid) && (!string.IsNullOrEmpty(txtDataProducao.Text)) && (!string.IsNullOrEmpty(txtObservacao.Text)))
                return true;
            else
                return false;
        }

        protected void txtDataProducao_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDataProducao.Text))
            {
                cboLado.Enabled = false;

                cboLinha.Enabled = false;
                ComboLinha(cboProducao.SelectedValue, cboModelo.SelectedValue);

                cboHorario.Enabled = false;
                ComboHorario();
            }
            else
            {
                cboLado.Enabled = true;
            }

        }

        protected void cboLado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLinha.Enabled = true;
            ComboLinha(cboProducao.SelectedValue, cboModelo.SelectedValue);
        }

        protected void cboLinha_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboHorario.Enabled = true;
            ComboHorario();
        }

        private void CarregaGrid(string modelo)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sqlModelo = string.IsNullOrEmpty(modelo) ? string.Empty : "  WHERE MODEL='" + modelo + "' ";
                string sql = @"SELECT MODEL AS modelo, LINE AS linha, SIDE AS lado, HOUR AS horario,
                                      DATE1 AS dataProducao, DESCRIPTION AS observacao, DATE_UPDATE AS dataRegistro, USER_UPDATE AS usuario, 
                                      CASE tipo_obs
                                        WHEN 0 THEN 'EFICIÊNCIA'
                                        WHEN 1 THEN 'YIELD RATE'
                                      ELSE '-'   
                                      END tipo_obs
                                   FROM  SFCRUNTIME.SFC_TV_FAIL " +
                                   sqlModelo + "ORDER BY DATE1 DESC, LINE, HOUR, SIDE";

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                int rowCount = Objconn.Tabela.Rows.Count;
                if (rowCount > 0)
                {
                    gridDados.DataSource = Objconn.Tabela;
                    gridDados.DataBind();

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
            if (CamposValidos())
            {
                bool existente = false;
                //
                if (gridDados.Rows.Count > 0)
                {
                    for (int index = 0; index < gridDados.Rows.Count; index++)
                    {
                        string modelo = gridDados.Rows[index].Cells[0].Text;
                        string lado = gridDados.Rows[index].Cells[1].Text;
                        string linha = gridDados.Rows[index].Cells[2].Text;
                        string horario = gridDados.Rows[index].Cells[3].Text;
                        string data = gridDados.Rows[index].Cells[4].Text;
                        string tipoObs = gridDados.Rows[index].Cells[6].Text;
                        //
                        if (modelo == cboModelo.SelectedValue
                            && lado == cboLado.SelectedValue
                            && linha == cboLinha.SelectedValue
                            && horario == cboHorario.SelectedValue
                            && tipoObs == rdbtTipoObs.SelectedValue
                            && data == DateTime.Parse(txtDataProducao.Text).ToString("yyyy-MM-dd"))
                        {
                            existente = true;
                            break;
                        }


                    }
                }
                if (existente)
                {
                    #region UPDATE

                    OLEDBConnect Objconn = new OLEDBConnect();
                    //
                    try
                    {
                        string modelo = cboModelo.SelectedValue;
                        string linha = cboLinha.SelectedValue;
                        string lado = cboLado.SelectedValue;
                        string hora = cboHorario.SelectedValue;
                        string dataProducao = txtDataProducao.Text;
                        dataProducao = DateTime.Parse(dataProducao).ToString("yyyy-MM-dd");
                        string observacao = txtObservacao.Text;
                        string usuario = (string)Session["id"];
                        //
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        string sql = @"UPDATE SFCRUNTIME.SFC_TV_FAIL SET
                                                 MODEL = '" + modelo + "'," +
                                               " LINE = '" + linha + "'," +
                                               " SIDE = '" + lado + "'," +
                                               " HOUR = '" + hora + "'," +
                                               " DATE1 = '" + dataProducao + "'," +
                                               " DESCRIPTION = '" + observacao + "', " +//Server.HtmlDecode(necessidade)
                                               " DATE_UPDATE = SYSDATE," +
                                               " USER_UPDATE = '" + usuario + "'" +
                                               " WHERE MODEL = '" + modelo + "'" +
                                               " AND LINE = '" + linha + "'" +
                                               " AND SIDE = '" + lado + "'" +
                                               " AND HOUR = '" + hora + "'" +
                                               " AND DATE1 = '" + dataProducao + "'";

                        Objconn.SetarSQL(sql);
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
                        lbAviso.Text = "Registro atualizado com sucesso!";
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        //
                        string modelo = cboModelo.SelectedValue;
                        CarregaGrid(modelo);
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.ForeColor = System.Drawing.Color.Red;
                        lbAviso.Text = "ERRO: " + Objconn.Message;
                    }

                    #endregion
                }
                else
                {
                    #region INSERT

                    OLEDBConnect Objconn = new OLEDBConnect();
                    //
                    try
                    {
                        int tipo_Obs = Convert.ToInt32(rdbtTipoObs.SelectedValue);//0 EFICIÊNCIA; 1 YIELD RATE
                        string modelo = cboModelo.SelectedValue;
                        string linha = cboLinha.SelectedValue;
                        string lado = cboLado.SelectedValue;
                        string hora = cboHorario.SelectedValue;
                        string dataProducao = txtDataProducao.Text;
                        dataProducao = DateTime.Parse(dataProducao).ToString("yyyy-MM-dd");
                        string observacao = txtObservacao.Text;
                        string usuario = (string)Session["id"];
                        //
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        string sql = @"INSERT INTO SFCRUNTIME.SFC_TV_FAIL (MODEL, LINE, SIDE, HOUR, DATE1, DESCRIPTION, DATE_UPDATE, USER_UPDATE, TIPO_OBS)
                                                     VALUES('" + modelo + "', '" + linha + "', '" + lado + "',  '" + hora + "', '" + dataProducao + "', '" + observacao + "', SYSDATE, '" + usuario + "'," + tipo_Obs + ")";
                        Objconn.SetarSQL(sql);
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
                        lbAviso.Text = "Registro salvo com sucesso!";
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        //
                        string modelo = cboModelo.SelectedValue;
                        CarregaGrid(modelo);
                    }
                    else
                    {
                        lbAviso.Visible = true;
                        lbAviso.ForeColor = System.Drawing.Color.Red;
                        lbAviso.Text = "ERRO: " + Objconn.Message;
                    }

                    #endregion
                }
            }

        }

        protected void gridDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region REMOVE
            //
            if (e.CommandName == "remover")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //
                string modelo = string.Empty;
                string lado = string.Empty;
                string linha = string.Empty;
                string horario = string.Empty;
                string dataProducao = string.Empty;
                string observacao = string.Empty;
                //
                OLEDBConnect Objconn = new OLEDBConnect();
                if (gridDados.Rows.Count > 0)
                {
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        modelo = gridDados.Rows[index].Cells[0].Text;
                        lado = gridDados.Rows[index].Cells[1].Text;
                        linha = gridDados.Rows[index].Cells[2].Text;
                        horario = gridDados.Rows[index].Cells[3].Text;
                        dataProducao = gridDados.Rows[index].Cells[4].Text;
                        observacao = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtObservacao")).Text) ? "" : ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtObservacao")).Text.Trim();
                        //
                        string Sql = @"DELETE FROM SFCRUNTIME.SFC_TV_FAIL 
                                              WHERE  MODEL = '" + modelo +
                                              "' AND LINE = '" + linha +
                                              "' AND SIDE = '" + lado +
                                              "' AND HOUR = '" + horario +
                                              "' AND DATE1 = '" + dataProducao + "'";
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
                        //                       
                        CarregaGrid(modelo);
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
                OLEDBConnect Objconn = new OLEDBConnect();
                //
                try
                {

                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    //
                    string modelo = string.Empty;
                    string lado = string.Empty;
                    string linha = string.Empty;
                    string horario = string.Empty;
                    string dataProducao = string.Empty;
                    string observacao = string.Empty;
                    //  
                    try
                    {
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        modelo = gridDados.Rows[index].Cells[0].Text;
                        lado = gridDados.Rows[index].Cells[1].Text;
                        linha = gridDados.Rows[index].Cells[2].Text;
                        horario = gridDados.Rows[index].Cells[3].Text;
                        dataProducao = gridDados.Rows[index].Cells[4].Text;
                        observacao = ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtObservacao")).Text;
                        //
                        string Sql = @"UPDATE SFCRUNTIME.SFC_TV_FAIL 
                                              SET  DESCRIPTION  ='" + observacao + "'" +
                                              " WHERE  MODEL = '" + modelo +
                                              "' AND LINE = '" + linha +
                                              "' AND SIDE = '" + lado +
                                              "' AND HOUR = '" + horario +
                                              "' AND DATE1 = '" + dataProducao + "'";
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
                        CarregaGrid(modelo);//refresh no grid
                        lbAviso.Text = "Alteração realizada com sucesso!";
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        lbAviso.Visible = true;

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

            #endregion

        }

        protected void gridDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDados.PageIndex = e.NewPageIndex;
            string modelo = cboModelo.SelectedValue;
            CarregaGrid(modelo);
            //
            //gridDados.DataSource = (DataTable)Session["refresh"];
            //gridDados.DataBind();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("default");
        }

        protected void cboProducao_SelectedIndexChanged(object sender, EventArgs e)
        {
            string producao = cboProducao.SelectedValue;
            string modelo = cboModelo.SelectedValue;
            //
            ComboModelo(producao);
            ComboLado(producao, modelo);
            ComboLinha(cboProducao.SelectedValue, cboModelo.SelectedValue);

            CarregaGrid(string.Empty);
        }

    }
}