using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace Registro
{
    public partial class listaPlanejado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtValor.Attributes.Add("onkeypress", "return isNumberKey(event);");
            //
            if (!IsPostBack)
            {
                ComboModelo("SMT");
                CarregaListaLinha();
                CarregaGrid(string.Empty, string.Empty);
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

        private void CarregaListaLinha()
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                string sql = @"SELECT DISTINCT LINE_NAME FROM C_GW28_CONFIG WHERE SCADA_ID ='SMO-SMT' ORDER BY LINE_NAME";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    chkListaLinha.DataTextField = "LINE_NAME";
                    chkListaLinha.DataValueField = "LINE_NAME";
                    chkListaLinha.DataSource = Objconn.Tabela;
                    chkListaLinha.DataBind();

                }

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
                                      INNER JOIN SFCCODELIKE  b  ON a.ROUTEID = b.AUTOROUTE 
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
                //
                string sql = string.Empty;
                //modelo = modelo.Contains("266") ? "SMO-" + modelo.Remove(0, 4).Remove(4) : modelo;
                modelo = "SMO-" + modelo.Replace("BE", string.Empty).Trim();

                if (TipoProducao.Equals("SMT"))
                {
                    sql = @"SELECT DISTINCT LINE_NAME FROM C_GW28_CONFIG WHERE SCADA_ID ='SMO-SMT' ORDER BY LINE_NAME";
                }
                else
                {
                    sql = @"SELECT DISTINCT LINE_NAME FROM C_GW28_CONFIG WHERE SCADA_ID  ='" + modelo + "' ORDER BY LINE_NAME";
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

        private string EventSeqno(string modelo, string lado)
        {
            string estacao = string.Empty;

            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                string sql = @"  SELECT a.EVENTSEQNO FROM SFCROUTEDEFB a
                                 INNER JOIN SFCCODELIKE  b ON  a.ROUTEID = b.AUTOROUTE  
                                  WHERE b.SKUNO ='" + modelo + "'" +
                                  "AND a.EVENTPOINT='" + lado + "'";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    estacao = Objconn.Tabela.Rows[0]["EVENTSEQNO"].ToString();
                }

            }
            finally
            {
                Objconn.Desconectar();
            }

            return estacao;
        }

        private void CarregaGrid(string modelo, string lado)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string estacao = cboLado.SelectedValue;
                string sqlModelo = string.IsNullOrEmpty(modelo) ? string.Empty : " AND DATA2 ='" + modelo + "' ";
                string sql = @"SELECT DATA2 AS MODELO,
                                     CASE DATA3
                                        WHEN 0 THEN 'BOT'
                                        WHEN 1 THEN 'TOP'
                                      ELSE '" + estacao + @"'
                                      END LADO,
                                      DATA5 AS VALORL1,
                                      DATA4 AS HORARIO,
                                      DATA6 AS VALORL2,
                                      DATA8 AS VALORL3,
                                      DATA9 AS VALORL4,
                                      DATA10 AS VALORL5
                                  FROM r_ap_temp WHERE DATA1 ='ARRIS_PLAN' AND DATA3 =" + lado + sqlModelo +
                                  " ORDER BY DATA2, DATA4";

                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    gridDados.DataSource = Objconn.Tabela;
                    gridDados.DataBind();
                    //
                    for (int index = 0; index < gridDados.Rows.Count; index++)
                    {
                        //grid dados
                        GridViewRow gvRow = gridDados.Rows[index];

                        //permite inserir somente números
                        ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtValorL1")).Attributes.Add("onkeypress", "return isNumberKey(event);");
                        ((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtValorL2")).Attributes.Add("onkeypress", "return isNumberKey(event);");
                        ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Attributes.Add("onkeypress", "return isNumberKey(event);");
                        ((TextBox)gridDados.Rows[index].Cells[4].FindControl("txtValorL4")).Attributes.Add("onkeypress", "return isNumberKey(event);");
                        ((TextBox)gridDados.Rows[index].Cells[5].FindControl("txtValorL5")).Attributes.Add("onkeypress", "return isNumberKey(event);");
                        //preenche os textbox
                        ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtValorL1")).Text = Objconn.Tabela.Rows[index]["VALORL1"].ToString();
                        ((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtValorL2")).Text = Objconn.Tabela.Rows[index]["VALORL2"].ToString();
                        ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Text = Objconn.Tabela.Rows[index]["VALORL3"].ToString();
                        ((TextBox)gridDados.Rows[index].Cells[4].FindControl("txtValorL4")).Text = Objconn.Tabela.Rows[index]["VALORL4"].ToString();
                        ((TextBox)gridDados.Rows[index].Cells[5].FindControl("txtValorL5")).Text = Objconn.Tabela.Rows[index]["VALORL5"].ToString();

                        if (lado.Equals("0") || lado.Equals("1"))//smt1, smt2
                        {
                            ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Enabled = true;
                        }
                        else
                        {
                            ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Enabled = false;
                        }

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

        protected void gridDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            #region REMOVE
            //
            if (e.CommandName == "remover")
            {
                OLEDBConnect Objconn = new OLEDBConnect();
                string modelo = string.Empty;
                string horario = string.Empty;
                string lado = string.Empty;
                //
                try
                {
                    try
                    {
                        //string modelo = e.CommandArgument.ToString();
                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        //                   
                        Objconn.Conectar();
                        Objconn.Parametros.Clear();
                        //
                        modelo = gridDados.Rows[index].Cells[0].Text;
                        horario = gridDados.Rows[index].Cells[6].Text;
                        lado = gridDados.Rows[index].Cells[7].Text;
                        //
                        if (lado.Equals("BOT"))
                        {
                            lado = "0";
                        }
                        else if (lado.Equals("TOP"))
                        {
                            lado = "1";
                        }
                        else
                        {
                            lado = EventSeqno(modelo, lado);
                        }
                        //
                        string Sql = "DELETE FROM R_AP_TEMP WHERE DATA2 = '" + modelo + "' AND DATA4 ='" + horario + "' AND DATA3 =" + lado;
                        Objconn.SetarSQL(Sql);
                        //
                        Objconn.Executar();

                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }
                    //
                    if (Objconn.Isvalid)
                    {
                        lbAviso.Text = "Registro deletado com sucesso!";
                        lbAviso.ForeColor = System.Drawing.Color.Blue;
                        lbAviso.Visible = true;
                        CarregaGrid(modelo, lado);//refresh no grid

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
            //
            #region EDITA
            //
            if (e.CommandName == "editar")
            {

                OLEDBConnect Objconn = new OLEDBConnect();
                //
                int ExiteCampoVazio = 0;
                string modelo = string.Empty;//= e.CommandArgument.ToString();//modelo selecionado
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string lado = string.Empty;
                //
                try
                {
                    try
                    {
                        string valorL1 = string.Empty;
                        string valorL2 = string.Empty;
                        string valorL3 = string.Empty;
                        string valorL4 = string.Empty;
                        string valorL5 = string.Empty;
                        string horario = string.Empty;
                        // 
                        if (!(((RequiredFieldValidator)gridDados.Rows[index].Cells[1].FindControl("RequiredFieldValidator5")).IsValid) || !(((RequiredFieldValidator)gridDados.Rows[index].Cells[3].FindControl("RequiredFieldValidator7")).IsValid))
                        {
                            ExiteCampoVazio++;
                        }
                        //
                        if (ExiteCampoVazio == 0)
                        {
                            modelo = gridDados.Rows[index].Cells[0].Text;
                            valorL1 = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtValorL1")).Text) ? "0" : ((TextBox)gridDados.Rows[index].Cells[1].FindControl("txtValorL1")).Text.Trim();
                            valorL2 = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtValorL2")).Text) ? "0" : ((TextBox)gridDados.Rows[index].Cells[2].FindControl("txtValorL2")).Text.Trim();
                            valorL3 = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Text) ? "0" : ((TextBox)gridDados.Rows[index].Cells[3].FindControl("txtValorL3")).Text.Trim();
                            valorL4 = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[4].FindControl("txtValorL4")).Text) ? "0" : ((TextBox)gridDados.Rows[index].Cells[4].FindControl("txtValorL4")).Text.Trim();
                            valorL5 = string.IsNullOrEmpty(((TextBox)gridDados.Rows[index].Cells[5].FindControl("txtValorL5")).Text) ? "0" : ((TextBox)gridDados.Rows[index].Cells[5].FindControl("txtValorL5")).Text.Trim();
                            horario = gridDados.Rows[index].Cells[6].Text;
                            lado = gridDados.Rows[index].Cells[7].Text;
                            //
                            if (lado.Equals("BOT"))
                            {
                                lado = "0";
                            }
                            else if (lado.Equals("TOP"))
                            {
                                lado = "1";
                            }
                            else
                            {
                                lado = EventSeqno(modelo, lado);
                            }

                        }
                        // 
                        if (ExiteCampoVazio == 0)
                        {

                            Objconn.Conectar();
                            Objconn.Parametros.Clear();
                            //
                            string Sql = @"UPDATE R_AP_TEMP SET 
                                                               DATA2  ='" + modelo + "'," +
                                                              "DATA5  ='" + valorL1 + "'," +
                                                              "DATA6  ='" + valorL2 + "'," +
                                                              "DATA8  ='" + valorL3 + "'," +
                                                              "DATA9  ='" + valorL4 + "'," +
                                                              "DATA10 ='" + valorL5 + "'," +
                                                              "WORK_TIME = SYSDATE " +
                                                 "WHERE DATA2 = '" + modelo + "' AND DATA4 ='" + horario + "' AND DATA3 =" + lado;
                            Objconn.SetarSQL(Sql);
                            Objconn.Executar();
                        }

                    }
                    finally
                    {
                        Objconn.Desconectar();
                    }
                    //
                    if (ExiteCampoVazio == 0)
                    {
                        if (Objconn.Isvalid)
                        {
                            lbAviso.Text = "Alteração realizada com sucesso!";
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            lbAviso.Visible = true;
                            CarregaGrid(modelo, lado);//refresh no grid

                        }
                        else
                        {
                            lbAviso.Text = "ERRO: " + Objconn.Message;
                            lbAviso.ForeColor = System.Drawing.Color.Red;
                            lbAviso.Visible = true;
                        }
                    }
                    else
                    {
                        lbAviso.Text = "ERRO: Não é permitido valores em branco.";
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

        private bool HorarioCadastrado(string modelo, string horario, string lado)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string Sql = "SELECT DATA2 AS ROW_COUNT FROM r_ap_temp WHERE DATA2 ='" + modelo + "' AND DATA4 ='" + horario + "' AND DATA3 =" + lado;
                Objconn.SetarSQL(Sql);
                //
                Objconn.Executar();

            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            if (Objconn.Tabela.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //private bool ExisteCampoVazio()
        //{
        //    if ((string.IsNullOrEmpty(txtValor.Text)) || (string.IsNullOrEmpty(cboModelo.SelectedValue)) || (string.IsNullOrEmpty(chkListaLinha.SelectedValue)))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string modelo = string.Empty;
                string horario = string.Empty;
                string lado = string.Empty;
                string producao = cboProducao.SelectedValue;

                if (producao.Equals("SMT"))
                {
                    modelo = cboModelo.SelectedValue;
                    horario = cboHorario.SelectedValue;
                    lado = rdbLado.SelectedValue;
                }
                else
                {
                    modelo = cboModelo.SelectedValue;
                    horario = cboHorario.SelectedValue;
                    lado = EventSeqno(modelo, cboLado.SelectedValue);

                }
                //
                if (!HorarioCadastrado(modelo, horario, lado))
                {
                    OLEDBConnect Objconn = new OLEDBConnect();
                    //
                    try
                    {
                        try
                        {
                            Objconn.Conectar();
                            Objconn.Parametros.Clear();
                            //
                            string linha = string.Empty; //chkListaLinha.SelectedValue;
                            string valor = txtValor.Text;
                            string valor1 = "0";
                            string valor2 = "0";
                            string valor3 = "0";
                            string valor4 = "0";
                            string valor5 = "0";
                            //
                            if (producao.Equals("SMT"))
                            {
                                for (int i = 0; i <= chkListaLinha.Items.Count - 1; i++)
                                {
                                    if (chkListaLinha.Items[i].Selected)
                                    {
                                        linha += chkListaLinha.Items[i].Value.ToString();
                                    }
                                }
                            }
                            else
                            {
                                linha = cboLinha.SelectedValue;
                            }
                            //
                            string sqlValues = string.Empty;
                            if (linha.Contains("L1") || linha.Contains("SI0"))
                            {
                                //DATA5
                                //sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', 1, 'PLANEJADO', '" + valor + "', 0, 0, SYSDATE, 0, 0, 0) ";
                                valor1 = valor;
                            }
                            if (linha.Contains("L2"))
                            {
                                //DATA6
                                //sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', 1, 'PLANEJADO', 0,'" + valor + "', 0, SYSDATE, 0, 0, 0) ";
                                valor2 = valor;
                            }
                            if (linha.Contains("L3"))
                            {
                                //DATA8
                                //sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', 1, 'PLANEJADO', 0, 0, 0, SYSDATE, '" + valor + "', 0, 0) ";
                                valor3 = valor;
                            }
                            if (linha.Contains("L4"))
                            {
                                //DATA9
                                //sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', 1, 'PLANEJADO', 0, 0, 0, SYSDATE, 0, '" + valor + "', 0) ";
                                valor4 = valor;
                            }
                            if (linha.Contains("L5"))
                            {
                                //DATA10
                                //sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', 1, 'PLANEJADO', 0, 0, 0, SYSDATE, 0, 0, '" + valor + "') ";
                                valor5 = valor;
                            }

                            //
                            sqlValues = " VALUES('ARRIS_PLAN', '" + modelo + "', '" + lado + "', '" + horario + "','" + valor1 + "','" + valor2 + "', 0, SYSDATE, '" + valor3 + "', '" + valor4 + "', '" + valor5 + "') ";
                            string Sql = @"INSERT INTO r_ap_temp (DATA1, DATA2, DATA3, DATA4, DATA5, DATA6, DATA7, WORK_TIME, DATA8, DATA9, DATA10 ) "
                                                     + sqlValues;

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
                            lbAviso.Text = "Modelo cadastrado com sucesso!";
                            lbAviso.ForeColor = System.Drawing.Color.Blue;
                            lbAviso.Visible = true;
                            CarregaGrid(modelo, lado);//refresh no grid

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
                else
                {
                    lbAviso.Text = "ERRO: Horário já está cadastrado.";
                    lbAviso.ForeColor = System.Drawing.Color.Red;
                    lbAviso.Visible = true;
                }
            }
            else
            {
                lbAviso.Text = "ERRO: Não é permitido valores em branco.";
                lbAviso.ForeColor = System.Drawing.Color.Red;
                lbAviso.Visible = true;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("default");
        }

        protected void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modelo = cboModelo.SelectedValue;
            string lado = rdbLado.SelectedValue;
            string producao = cboProducao.SelectedValue;
            //
            CarregaGrid(modelo, lado);
            ComboLado(producao, modelo);
        }

        protected void rdbLado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string modelo = cboModelo.SelectedValue.ToString();
            string lado = rdbLado.SelectedValue; //rdbLado.SelectedItem.Value; 
            CarregaGrid(modelo, lado);
        }

        protected void cboProducao_SelectedIndexChanged(object sender, EventArgs e)
        {
            string producao = cboProducao.SelectedValue;
            string modelo = cboModelo.SelectedValue;
            //
            ComboLinha(cboProducao.SelectedValue, string.Empty);
            ComboLado(producao, string.Empty);
            //
            if (producao.Equals("SMT"))
            {
                divBE.Visible = false;
                divLinhaSMT.Visible = true;
                divLadoSMT.Visible = true;
                //
                ComboModelo(producao);
                //ComboLado(producao, modelo);
            }
            else
            {
                divBE.Visible = true;
                divLinhaSMT.Visible = false;
                divLadoSMT.Visible = false;
                //
                ComboModelo(producao);
                // ComboLado(producao, modelo);
            }
        }

        protected void cboLado_SelectedIndexChanged(object sender, EventArgs e)
        {

            string modelo = cboModelo.SelectedValue;
            //string lado = cboLado.SelectedValue;
            string lado = EventSeqno(modelo, cboLado.SelectedValue);
            string producao = cboProducao.SelectedValue;
            //
            ComboLinha(producao, cboModelo.SelectedItem.ToString());
            CarregaGrid(modelo, lado);
        }

    }
}