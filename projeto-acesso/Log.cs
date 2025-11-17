using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_acesso;

namespace projeto_acesso
{
    internal class Log
    {
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipoAceso;

        public DateTime DtAcesso { get => dtAcesso; set => dtAcesso = value; }
        public bool TipoAceso { get => tipoAceso; set => tipoAceso = value; }
        internal Usuario Usuario { get => usuario; set => usuario = value; }

        public Log() { }

        public Log(DateTime dtAcesso, Usuario usuario, bool tipoAceso)
        {
            DtAcesso = dtAcesso;
            Usuario = usuario;
            TipoAceso = tipoAceso;
        }
    }
}
