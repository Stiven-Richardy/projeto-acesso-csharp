using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using projeto_acesso;

/*
| OBS:
| a) Cada ambiente registrará, no máximo, 100 logs
*/

namespace projeto_acesso
{
    internal class Ambiente
    {
        private int id;
        private string nome;
        private Queue<Log> logs;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public Queue<Log> Logs { get => logs; set => logs = value; }

        public Ambiente() { }

        public Ambiente(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Logs = new Queue<Log>();
        }

        public void RegistrarLog(Log log)
        {

        }
    }
}

