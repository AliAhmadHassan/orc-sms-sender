using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrcSMS.DTO
{
    /// <summary>
    /// Definições das colunas da planilha
    /// </summary>
    public class XLSX_Coluna
    {
        public sealed class AtributoXLS_Coluna : System.Attribute
        {

            private int inOrdem;

            public AtributoXLS_Coluna()
            {
                inOrdem = 0;
            }

            public int Ordem
            {
                get { return inOrdem; }
                set { inOrdem = value; }
            }
        }
    }

    /// <summary>
    /// Definição do inicio da planilha a ser transportado
    /// </summary>
    public class XLSX_Planilha
    {
        public sealed class AtributoXLS : System.Attribute
        {
            public AtributoXLS()
            {
                Linha = 0;
                Coluna = 0;
            }

            public int Linha { get; set; }
            public int Coluna { get; set; }
        }
    }
}
