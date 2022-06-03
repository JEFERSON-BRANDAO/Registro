﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Collections;
//using System.Data.SqlClient;
using System.Data.OleDb;
//using Oracle.DataAccess.Client;

namespace Classes
{
    public class OLEDBConnect
    {
        #region Attributes

        private bool _isvalid;
        private string _message;
        private string _stringConnection;
        private OleDbConnection _connection;
        private DataTable _tabela;
        private IList _parametros = new ArrayList();
        private OleDbTransaction _transaction;
        private OleDbCommand _command;

        #endregion
        //
        #region Properties

        public string StringConnection
        {
            get { return _stringConnection; }
            set { _stringConnection = value; }
        }

        public OleDbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public DataTable Tabela
        {
            get { return _tabela; }
            set { _tabela = value; }
        }

        public IList Parametros
        {
            get { return _parametros; }
            set { _parametros = value; }
        }

        public OleDbTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        public bool Isvalid
        {
            get { return _isvalid; }
            set { _isvalid = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        #endregion
        //
        #region Methods

        public bool Conectar()
        {
            try
            {
                Criptografia objCript = new Criptografia();
                string _connectionString = "Provider=OraOLEDB.Oracle;Data Source=" + objCript.Descriptografar(ConfigurationManager.AppSettings["dataSourceDB"].ToString()) + ";User Id=" + objCript.Descriptografar(ConfigurationManager.AppSettings["usuarioDB"].ToString()) + ";Password=" + objCript.Descriptografar(ConfigurationManager.AppSettings["senhaDB"].ToString()) + "; OLEDB.NET = True;";
                //
                _connection = new OleDbConnection(_connectionString);
                //
                _connection.Open();
                return true;
            }
            catch (Exception erro)
            {
                Message = erro.Message;
                return false;
            }
        }


        //public string TesteConexao()
        //{
        //    try
        //    {
        //        Criptografia objCript = new Criptografia();
        //        string _connectionString = "Provider=OraOLEDB.Oracle;Data Source=" + objCript.Descriptografar(ConfigurationManager.AppSettings["dataSourceDB"].ToString()) + ";User Id=" + objCript.Descriptografar(ConfigurationManager.AppSettings["usuarioDB"].ToString()) + ";Password=" + objCript.Descriptografar(ConfigurationManager.AppSettings["senhaDB"].ToString()) + "; OLEDB.NET = True;";
        //        //string _connectionString = "Provider = OraOLEDB.Oracle; Data Source = LEVEL6_TEST; User Id = bdhoras; Password = bdh0r4s; OLEDB.NET = True;" 
        //        //string _connectionString = "Provider=msdaora;Data Source=" + objCript.Descriptografar(ConfigurationManager.AppSettings["dataSourceDB"].ToString()) + ";User Id=" + objCript.Descriptografar(ConfigurationManager.AppSettings["usuarioDB"].ToString()) + ";Password=" + objCript.Descriptografar(ConfigurationManager.AppSettings["senhaDB"].ToString()) + ";";

        //        //
        //        _connection = new OleDbConnection(_connectionString);
        //        //
        //        _connection.Open();
        //        return "true";
        //    }
        //    catch (Exception erro)
        //    {
        //        Message = erro.Message;
        //        return Message;
        //    }
        //}

        public void Desconectar()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception) { }
        }

        public void AdicionarParametro(string nome, object valor, SqlDbType tipo)
        {
            OleDbParameter parametro = new OleDbParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Input;
            parametro.Value = valor;

            _parametros.Add(parametro);
        }

        public void AdicionarParametroSaida(string nome, SqlDbType tipo)
        {
            OleDbParameter parametro = new OleDbParameter(nome, tipo);
            parametro.Direction = ParameterDirection.Output;

            _parametros.Add(parametro);
        }

        public void SetarSQL(string SQL)
        {
            _command = new OleDbCommand();
            _command.CommandType = CommandType.Text;
            _command.CommandText = SQL;
            _command.Connection = _connection;
        }

        public void SetarSP(string nomeSP)
        {
            _command = new OleDbCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = nomeSP;
            _command.Connection = _connection;
        }

        public bool Executar()
        {

            try
            {
                //_command.Parameters.Clear();

                foreach (OleDbParameter parametro in _parametros)
                {
                    _command.Parameters.Add(parametro);

                    //_command.Parameters.AddWithValue(parametro.ToString(), "");
                }

                //_parametros = new ArrayList();

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(_command);
                Tabela = new DataTable();
                dataAdapter.Fill(Tabela);

                _isvalid = true;
                _message = "";

                return true;
            }
            catch (Exception erro)
            {
                _isvalid = false;
                _message = erro.Message;

                return false;
            }
        }

        #endregion
    }
}
