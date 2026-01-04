using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcSMS.DAL
{
    public class MensagemStatus : Base<DTO.MensagemStatus>
    {
        //public virtual List<DTO.MensagemStatus> SelectPeloIdPai(int Id)
        //{
        //    List<DTO.MensagemStatus> LMensagemStatus = new List<DTO.MensagemStatus>();

        //    using (SqlConnection Conn = new SqlConnection(Conexao.strConn))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SPSMensagemStatusPelaIdPai", Conn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("IdPai", Id);

        //                Conn.Open();
        //                using (SqlDataReader Dr = cmd.ExecuteReader())
        //                {
        //                    while (Dr.Read())
        //                        LMensagemStatus.Add(Auxiliar.RetornaDadosEntidade<DTO.MensagemStatus>(Dr));
        //                }
        //            }
        //            catch (Exception Erro)
        //            {
        //                throw new Exception("Erro ao consultar");
        //            }
        //            finally
        //            {

        //            }
        //        }
        //    }

        //    return LMensagemStatus;
        //}
    }
}
