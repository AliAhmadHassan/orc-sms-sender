using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.IO;

namespace OrcSMS.DAL
{
    public class XLSX
    {
        public void ExportarParaExcel<T>(List<T> _Lentidade, string PathArquivo)
        {
            


            Type tipo = typeof(T);

            string[,] arrHeader = new string[_Lentidade.Count, tipo.GetProperties().Length];
            string[] TipoColuna = new string[tipo.GetProperties().Length];

            //FieldInfo[] instanceFields = tipo.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var props = typeof(T).GetProperties(); ;

            foreach (var p in props)
            {

                PropertyInfo info = tipo.GetProperty(p.Name);

                object[] Obj = tipo.GetProperty(p.Name).GetCustomAttributes(true);

                if (info.PropertyType.Name.Equals("String") || info.PropertyType.Name.Equals("Char") || info.PropertyType.Name.Equals("Guid") || info.PropertyType.Name.Equals("Object"))
                    TipoColuna[((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = "string";
                else if ((info.PropertyType.Name.Equals("Int32")) || (info.PropertyType.Name.Equals("Int64")))
                    TipoColuna[((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = "Int";
                else if ((info.PropertyType.Name.Equals("decimal")) || (info.PropertyType.Name.Equals("Decimal")) || (info.PropertyType.Name.Equals("double")) || (info.PropertyType.Name.Equals("Double")))
                    TipoColuna[((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = "decimal";
                else if (info.PropertyType.Name.Equals("DateTime"))
                    TipoColuna[((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = "DateTime";
                else if (info.PropertyType.Name.Equals("bool"))
                    TipoColuna[((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = "bool";


                for (int i = 0; i < _Lentidade.Count; i++)
                {
                    T _entidade = _Lentidade[i];
                    arrHeader[i, ((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)Obj.GetValue(0)).Ordem] = Convert.ToString(p.GetValue(_entidade, null));
                }
            }

            #region Gerar Xml
            {
                //C:\Windows\SysWOW64>mmc comexp.msc /32
                Excel.Application xl = null;
                Excel.Workbook wb = null;
                Excel.Worksheet excelWorksheet = null;
                try
                {
                    xl = new Excel.Application();
                    xl.Visible = false;

                    //To open an excel file,
                    wb = xl.Workbooks.Open(PathArquivo, 0, false, 5, System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true, false, System.Reflection.Missing.Value, false);//Open the excel 

                    //To read cell(s) in the worksheet,
                    excelWorksheet = (Excel.Worksheet)wb.Sheets[1]; //Select the first sheet
                    //Excel.Range excelCell = (Excel.Range)excelWorksheet.get_Range("B4:FZ4", Type.Missing); //Select a range of cells
                    //Excel.Range excelCell1 = (Excel.Range)excelWorksheet.get_Range("B4:B4", Type.Missing); //Select a single cell
                    //excelCell1.Cells.Value2 = DateTime.Today.ToString().Replace(" 00:00:00", ""); //Assign a value to the cell

                    for (int Coluna = 0; Coluna < TipoColuna.Length; Coluna++)
                    {
                        string[,] strarray = new string[_Lentidade.Count, 1];
                        Int32[,] Intarray = new Int32[_Lentidade.Count, 1];
                        double[,] decimalarray = new double[_Lentidade.Count, 1];
                        object[,] DateTimearray = new object[_Lentidade.Count, 1];
                        bool[,] boolarray = new bool[_Lentidade.Count, 1];


                        Excel.Range excelCell = excelWorksheet.get_Range(ConvertNumAlfa(Coluna) + "2:" + ConvertNumAlfa(Coluna) + Convert.ToString(2 + _Lentidade.Count - 1), Type.Missing); //Select a single cell
                        for (int Linha = 0; Linha < _Lentidade.Count; Linha++)
                        {
                            switch (TipoColuna[Coluna])
                            {
                                case "string": strarray[Linha, 0] = arrHeader[Linha, Coluna].ToString();
                                    break;
                                case "Int": Intarray[Linha, 0] = Int32.Parse(arrHeader[Linha, Coluna].ToString());
                                    break;
                                case "decimal": decimalarray[Linha, 0] = double.Parse(arrHeader[Linha, Coluna].ToString());
                                    break;
                                case "DateTime":
                                    if (arrHeader[Linha, Coluna].ToString() != "01/01/0001 00:00:00")
                                        DateTimearray[Linha, 0] = DateTime.Parse(arrHeader[Linha, Coluna].ToString());
                                    break;
                                case "bool": boolarray[Linha, 0] = bool.Parse(arrHeader[Linha, Coluna].ToString());
                                    break;
                            }
                        }

                        switch (TipoColuna[Coluna])
                        {
                            case "string": excelCell.Value2 = strarray;
                                break;
                            case "Int": excelCell.Value2 = Intarray;
                                break;
                            case "decimal": excelCell.Value2 = decimalarray;
                                break;
                            case "DateTime": excelCell.Value2 = DateTimearray;
                                //for (int i = 0; i < _Lentidade.Count; i++)
                                //    if (excelWorksheet.Cells[i,Coluna] == "00/01/1900  00:00:00")
                                //        excelWorksheet.Cells[i, Coluna] = "";

                                break;
                            case "bool": excelCell.Value2 = boolarray;
                                break;
                        }
                    }

                    wb.Save(); //Save the workbook
                    wb.Close(false, null, null);
                    //Finally Quit the Excel Application
                    //xl.Save();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    xl.Quit();
                    liberarObjetos(excelWorksheet);
                    liberarObjetos(wb);
                    liberarObjetos(xl);
                }

            }
            #endregion
        }

        public List<T> RetornaEntidade<T>(string PathArquivo)
        {
            List<T> lista = new List<T>();

            Type tipo = typeof(T);
            

            #region Definições da planilha
            int InicioLinha = -1, InicioColuna = -1;
            int MaxIndice = 0;

            SortedList<string, int> ObjIndices = new SortedList<string, int>();

            foreach (PropertyInfo pi in Activator.CreateInstance(typeof(T)).GetType().GetProperties())
            {
                foreach (object objAux in tipo.GetProperty(pi.Name).GetCustomAttributes(true))
                {
                    if (objAux.GetType().Name == "AtributoXLS_Coluna")
                    {
                        if (MaxIndice < ((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)objAux).Ordem)
                            MaxIndice = ((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)objAux).Ordem;
                        ObjIndices.Add(pi.Name, ((OrcSMS.DTO.XLSX_Coluna.AtributoXLS_Coluna)objAux).Ordem);
                    }
                    else if (objAux.GetType().Name == "AtributoXLS")
                    {
                        InicioLinha = ((OrcSMS.DTO.XLSX_Planilha.AtributoXLS)objAux).Linha + 1;
                        InicioColuna = ((OrcSMS.DTO.XLSX_Planilha.AtributoXLS)objAux).Coluna;
                    }
                }

            }
            if (InicioLinha == -1)
                throw new Exception("Atributo XLSX_Planilha não definido na Entidade");
            #endregion



            Excel.Application xl = null;
            Excel.Workbook wb = null;
            Excel.Worksheet excelWorksheet = null;
            try
            {
                xl = new Excel.Application();
                xl.Visible = false;
                //To open an excel file,
                wb = xl.Workbooks.Open(PathArquivo, 0, false, 5, System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true, false, System.Reflection.Missing.Value, false);//Open the excel 

                //To read cell(s) in the worksheet,
                excelWorksheet = (Excel.Worksheet)wb.Sheets[1]; //Select the first sheet
                Excel.Range excelCell2 = (Excel.Range)excelWorksheet.get_Range(string.Format("{0}{1}:{0}{1}", ConvertNumAlfa(InicioColuna), InicioLinha), Type.Missing);

                int intLinhas = 0;
                for (int i = 1; ((Excel.Range)excelCell2.Cells[i, 1]).Value2 != null; i++)
                {
                    intLinhas++;
                }

                Excel.Range excelCell = excelWorksheet.get_Range(string.Format("{0}{2}:{1}{3}", ConvertNumAlfa(InicioColuna), ConvertNumAlfa(InicioColuna + MaxIndice), InicioLinha, InicioLinha + intLinhas), Type.Missing); //Select a single cell

                object[,] Planilha = excelCell.Value2;

                for (int Linha = 1; Linha <= intLinhas; Linha++)
                {
                    T _entidade = (T)Activator.CreateInstance(typeof(T));
                    int Coluna = 0;
                    foreach (PropertyInfo pi in _entidade.GetType().GetProperties())
                    {
                        Coluna = ObjIndices[pi.Name]+1;

                        object valor = Planilha[Linha, Coluna];

                        switch (pi.PropertyType.Name)
                        {
                            case "String": pi.SetValue(_entidade, Convert.ToString(valor), null);
                                break;
                            case "Int32": pi.SetValue(_entidade, Convert.ToInt32(valor), null);
                                break;
                            case "Decimal": pi.SetValue(_entidade, Convert.ToDecimal(valor), null);
                                break;
                            case "DateTime":
                                if (valor.ToString().Contains("/"))
                                    pi.SetValue(_entidade, Convert.ToDateTime(valor), null);
                                else
                                    pi.SetValue(_entidade, Convert.ToDateTime("01/01/1900").AddDays(Convert.ToInt32(valor) - 2), null);
                                break;
                            case "Bool": pi.SetValue(_entidade, Convert.ToBoolean(valor), null);
                                break;
                            default:
                                throw new Exception("Tipo [" + pi.PropertyType.Name + "] Não Definido ");
                                break;
                        }

                    }
                    lista.Add(_entidade);
                    //Excel.Range excelCell = excelWorksheet.get_Range(ConvertNumAlfa(Coluna) + "2:" + ConvertNumAlfa(Coluna) + Convert.ToString(2 + MaxIndice - 1), Type.Missing); //Select a single cell
                }

                wb.Save(); //Save the workbook
                wb.Close(false, null, null);
                //Finally Quit the Excel Application
                //xl.Save();
            }
            catch (Exception ex)
            {
                //throw ex; 
            }
            finally
            {
                xl.Quit();
                liberarObjetos(excelWorksheet);
                liberarObjetos(wb);
                liberarObjetos(xl);
            }
            return lista;
        }

        private static string ConvertNumAlfa(int Num)
        {
            string[] Matrix = new string[79];
            Matrix[1] = "A";
            Matrix[2] = "B";
            Matrix[3] = "C";
            Matrix[4] = "D";
            Matrix[5] = "E";
            Matrix[6] = "F";
            Matrix[7] = "G";
            Matrix[8] = "H";
            Matrix[9] = "I";
            Matrix[10] = "J";
            Matrix[11] = "K";
            Matrix[12] = "L";
            Matrix[13] = "M";
            Matrix[14] = "N";
            Matrix[15] = "O";
            Matrix[16] = "P";
            Matrix[17] = "Q";
            Matrix[18] = "R";
            Matrix[19] = "S";
            Matrix[20] = "T";
            Matrix[21] = "U";
            Matrix[22] = "V";
            Matrix[23] = "W";
            Matrix[24] = "X";
            Matrix[25] = "Y";
            Matrix[26] = "Z";
            Matrix[27] = "AA";
            Matrix[28] = "AB";
            Matrix[29] = "AC";
            Matrix[30] = "AD";
            Matrix[31] = "AE";
            Matrix[32] = "AF";
            Matrix[33] = "AG";
            Matrix[34] = "AH";
            Matrix[35] = "AI";
            Matrix[36] = "AJ";
            Matrix[37] = "AK";
            Matrix[38] = "AL";
            Matrix[39] = "AM";
            Matrix[40] = "AN";
            Matrix[41] = "AO";
            Matrix[42] = "AP";
            Matrix[43] = "AQ";
            Matrix[44] = "AR";
            Matrix[45] = "AS";
            Matrix[46] = "AT";
            Matrix[47] = "AU";
            Matrix[48] = "AV";
            Matrix[49] = "AW";
            Matrix[50] = "AX";
            Matrix[51] = "AY";
            Matrix[52] = "AZ";
            Matrix[53] = "BA";
            Matrix[54] = "BB";
            Matrix[55] = "BC";
            Matrix[56] = "BD";
            Matrix[57] = "BE";
            Matrix[58] = "BF";
            Matrix[59] = "BG";
            Matrix[60] = "BH";
            Matrix[61] = "BI";
            Matrix[62] = "BJ";
            Matrix[63] = "BK";
            Matrix[64] = "BL";
            Matrix[65] = "BM";
            Matrix[66] = "BN";
            Matrix[67] = "BO";
            Matrix[68] = "BP";
            Matrix[69] = "BQ";
            Matrix[70] = "BR";
            Matrix[71] = "BS";
            Matrix[72] = "BT";
            Matrix[73] = "BU";
            Matrix[74] = "BV";
            Matrix[75] = "BW";
            Matrix[76] = "BX";
            Matrix[77] = "BY";
            Matrix[78] = "BZ";
            return Matrix[Num + 1];
        }

        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }

}
