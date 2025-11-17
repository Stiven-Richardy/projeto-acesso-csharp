using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projeto_acesso;

/*
| OBS: 
| a) Métodos de remoção devem indicar o sucesso da operação  
| b) Usuários só poderão ser removidos se estiverem sem nenhum tipo de permissão de acesso
*/

namespace projeto_acesso
{
    internal class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;

        internal List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }
        internal List<Ambiente> Ambientes { get => ambientes; set => ambientes = value; }

        public Cadastro() 
        {
            Usuarios = new List<Usuario>();
            Ambientes = new List<Ambiente>();
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            Usuarios.Add(usuario);
        }

        public bool RemoverUsuario(Usuario usuario)
        {
            bool usuarioRemovido = false;
            if (PesquisarUsuario(usuario) != null)
            {
                Usuarios.Remove(usuario);
                usuarioRemovido = true;
            }
            return usuarioRemovido;
        }

        public Usuario PesquisarUsuario(Usuario usuario)
        {
            Usuario usuarioPesquisado = Usuarios.Find(u => u.Nome.Equals(usuario.Nome, StringComparison.OrdinalIgnoreCase));
            return usuarioPesquisado;
        }

        public void AdicionarAmbiente(Ambiente ambiente)
        {
            Ambientes.Add(ambiente);
        }

        public bool RemoverAmbiente(Ambiente ambiente)
        {
            bool ambienteRemovido = false;
            if(PesquisarAmbiente(ambiente) != null)
            {
                Ambientes.Remove(ambiente);
                ambienteRemovido = true;
            }
            return ambienteRemovido;
        }

        public Ambiente PesquisarAmbiente(Ambiente ambiente)
        {
            Ambiente ambientePesquisado = Ambientes.Find(a => a.Nome.Equals(ambiente.Nome, StringComparison.OrdinalIgnoreCase));
            return ambientePesquisado;
        }

        public void Upload()
        {

        }

        public void Download()
        {

        }
    }
}


