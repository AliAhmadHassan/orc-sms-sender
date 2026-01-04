using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrcSMS.DTO
{
    public abstract class Base
    {
        public sealed class AtributoBind : System.Attribute
        {
            private bool _chavePrimaria;

            public bool ChavePrimaria
            {
                get { return _chavePrimaria; }
                set { _chavePrimaria = value; }
            }

            private string _procedureInserir;

            public string ProcedureInserir
            {
                get { return _procedureInserir; }
                set { _procedureInserir = value; }
            }

            private string _procedureAlterar;

            public string ProcedureAlterar
            {
                get { return _procedureAlterar; }
                set { _procedureAlterar = value; }
            }

            private string _procedureRemover;

            public string ProcedureRemover
            {
                get { return _procedureRemover; }
                set { _procedureRemover = value; }
            }

            private string _procedureListarTodos;

            public string ProcedureListarTodos
            {
                get { return _procedureListarTodos; }
                set { _procedureListarTodos = value; }
            }

            private string _procedureSelecionar;

            public string ProcedureSelecionar
            {
                get { return _procedureSelecionar; }
                set { _procedureSelecionar = value; }
            }

        }
    }
}
