using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;
using System.Collections;
using Classes;
using System.Configuration;


namespace Classes
{
    public class Permissao
    {
        public IList<Menu> ListaMenu(string Usuario)
        {
            OLEDBConnect Objconn = new OLEDBConnect();
            List<Menu> Lista = new List<Menu>();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string Menus = ConfigurationManager.AppSettings["menu"].ToString();
                string Sql = @"SELECT FUNCTIONNAME FROM EPERMISSION WHERE PERMISSIONNAME = '" + Usuario + "' AND FUNCTIONNAME IN ('" + Menus.Replace(",", "','") + "')";
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    for (int i = 0; i < Objconn.Tabela.Rows.Count; i++)
                    {
                        Menu objMenu = new Menu();
                        string menu = Objconn.Tabela.Rows[i]["FUNCTIONNAME"].ToString();
                        //
                        objMenu.menu = menu.ToUpper();
                        objMenu.menuItem = menu.ToLower();
                        objMenu.Titulo = "[" + menu.ToLower() + "]";
                        //
                        switch (menu)
                        {
                            case "OBSERVACAO":
                                objMenu.Pagina = "listaObservacao.aspx";
                                break;

                            case "PLANEJAMENTO":
                                objMenu.Pagina = "listaPlanejado.aspx";
                                break;

                            case "ANIVERSARIANTE":
                                objMenu.Pagina = "aniversariante.aspx";
                                break;

                            case "GRAFICO":
                                objMenu.Pagina = "grafico.aspx";
                                break;
                        }
                        //
                        Lista.Add(objMenu);
                    }

                }
            }
            finally
            {
                Objconn.Desconectar();
            }
            //
            return Lista;
        }

        public bool PermiteAcessoMenu(string IdUsuario, string pagina)
        {

            OLEDBConnect Objconn = new OLEDBConnect();
            //
            try
            {
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string Sql = @" select    m.idmenu as Id,
                                      m.menu as Menu,
                                      m.menuItem as MenuItem,
                                      m.titulo as Titulo,
                                      m.pagina as Pagina,
                                      m.idgrupo,
                                      m.status,
                                      g.nome as Grupo,
                                      u.idusuario as idUsuario
                                      from compras.menu m
                                      inner join compras.usuario u on m.idgrupo = u.idgrupo
                                      inner join compras.grupo g on m.idgrupo = g.idgrupo
                                      where m.status = 1 and u.idusuario = " + IdUsuario + " and m.pagina = '" + pagina + "'";
                //
                Objconn.SetarSQL(Sql);
                Objconn.Executar();
            }
            finally
            {
                Objconn.Desconectar();
            }
            //  
            return Objconn.Tabela.Rows.Count > 0 ? true : false;

        }

    }
}