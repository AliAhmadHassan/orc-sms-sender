using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcSMS.DTO
{
    public class Remessa:Base
    {
        //Id	        int
        //Nome	        nvarchar(MAX)
        //Descricao	    nvarchar(MAX)

        public Remessa()
        {
            this.ID = -1;
        }

        [AtributoBind(ChavePrimaria = true
            , ProcedureAlterar = "SPURemessa"
            , ProcedureInserir = "SPIRemessa"
            , ProcedureRemover = "SPDRemessa"
            , ProcedureListarTodos = "SPSRemessa"
            , ProcedureSelecionar = "SPSRemessaPelaPK")]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
