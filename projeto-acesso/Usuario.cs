using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_acesso;

/*
| OBS:
| a) Cada usuário só pode ter uma permissão para cada ambiente
*/

namespace projeto_acesso
{
    internal class Usuario
    {
        private int id;
        private string nome;
        private List<Ambiente> ambientes;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }

        public Usuario(string nome) 
        {
            Nome = nome;
        }

        public Usuario(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Ambientes = new List<Ambiente>();
        }

        public bool ConcederPermissao(Ambiente ambiente)
        {
            return false;
        }

        public bool RevogarPermissao(Ambiente ambiente)
        {
            return false;
        }
    }
}

