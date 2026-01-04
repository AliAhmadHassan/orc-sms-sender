using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcSMS.DAL
{
    public class Remessa : Base<DTO.Remessa>
    {
        //public virtual List<DTO.Remessa> SelectPeloIdPai(int Id)
        //{
        //    List<DTO.Remessa> LRemessa = new List<DTO.Remessa>();

        //    using (SqlConnection Conn = new SqlConnection(Conexao.strConn))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SPSRemessaPelaIdPai", Conn))
        //        {
        //            try
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("IdPai", Id);

        //                Conn.Open();
        //                using (SqlDataReader Dr = cmd.ExecuteReader())
        //                {
        //                    while (Dr.Read())
        //                        LRemessa.Add(Auxiliar.RetornaDadosEntidade<DTO.Remessa>(Dr));
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

        //    return LRemessa;
        //}

    }
}
