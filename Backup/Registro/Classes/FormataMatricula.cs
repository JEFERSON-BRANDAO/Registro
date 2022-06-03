using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class FormataMatricula
    {
        public string formataStrMatricula(string matricula)
        { 
            string strMatricula = string.Empty;
            //
            if (string.IsNullOrEmpty(matricula))
            {
                strMatricula = string.Empty;
            }
            else
            {
                if (matricula.Length > 0)
                {
                    int cont = 0;
                    //
                    if (matricula.Length >= 10)//tamanho da matricula completa
                    {
                        for (int i = 0; i < matricula.Length; i++)
                        {
                            cont++;
                            //
                            if (cont >= 4 && cont <= 9)//xxx 000000 x  (remove os 3 primeiros números e o último)
                            {
                                strMatricula += matricula[i];
                            }
                        }
                    }
                    else //caso tamanho da matricula seja menor que 10, mantem o valor informado
                    {
                        strMatricula = matricula;
                    }
                }
            }
            //
            return strMatricula.Trim();
        }
    }
}