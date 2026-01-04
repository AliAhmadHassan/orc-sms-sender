using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcSMS.DTO
{
    public class Mensagem:Base
    {
        //ID	            int
        //DDD	            int
        //Telefone	        bigint
        //Texto	            nvarchar(160)
        //MensagemStatus_ID	int
        //Remessa_Id	    int

        public Mensagem()
        {
            this.ID = -1;
        }

        [AtributoBind(ChavePrimaria = true
            , ProcedureAlterar = "SPUMensagem"
            , ProcedureInserir = "SPIMensagem"
            , ProcedureRemover = "SPDMensagem"
            , ProcedureListarTodos = "SPSMensagem"
            , ProcedureSelecionar = "SPSMensagemPelaPK")]
        public int ID { get; set; }
        public int DDD { get; set; }
        public Int64 Telefone { get; set; }
        public string Texto { get; set; }
        public int MensagemStatus_ID { get; set; }
        public int Remessa_Id { get; set; }
	
    }
}
