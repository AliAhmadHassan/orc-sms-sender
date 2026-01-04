using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcSMS.DTO
{
    public class MensagemStatus:Base
    {
        //ID	            int
        //Descricao	        nvarchar(MAX)

        public MensagemStatus()
        {
            this.ID = -1;
        }

        [AtributoBind(ChavePrimaria = true
            , ProcedureAlterar = "SPUMensagemStatus"
            , ProcedureInserir = "SPIMensagemStatus"
            , ProcedureRemover = "SPDMensagemStatus"
            , ProcedureListarTodos = "SPSMensagemStatus"
            , ProcedureSelecionar = "SPSMensagemStatusPelaPK")]
        public int ID { get; set; }
        public string Descricao { get; set; }
	
    }
}
