using System;
using System.Data.SQLite;
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
        private string connectionString = "Data Source=database.db;Version=3;";

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
            Usuario usuarioPesquisado = PesquisarUsuario(usuario);
            if (usuarioPesquisado != null && usuarioPesquisado.Ambientes.Count() == 0)
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
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        new SQLiteCommand("DELETE FROM logs", conn).ExecuteNonQuery();
                        new SQLiteCommand("DELETE FROM permissoes", conn).ExecuteNonQuery();
                        new SQLiteCommand("DELETE FROM ambientes", conn).ExecuteNonQuery();
                        new SQLiteCommand("DELETE FROM usuarios", conn).ExecuteNonQuery();

                        foreach (Usuario u in Usuarios)
                        {
                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO usuarios (id, nome) VALUES (@id, @nome)", conn);
                            cmd.Parameters.AddWithValue("@id", u.Id);
                            cmd.Parameters.AddWithValue("@nome", u.Nome);
                            cmd.ExecuteNonQuery();
                        }

                        foreach (Ambiente a in Ambientes)
                        {
                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO ambientes (id, nome) VALUES (@id, @nome)", conn);
                            cmd.Parameters.AddWithValue("@id", a.Id);
                            cmd.Parameters.AddWithValue("@nome", a.Nome);
                            cmd.ExecuteNonQuery();

                            foreach (Log l in a.Logs)
                            {
                                cmd = new SQLiteCommand("INSERT INTO logs (dt_acesso, id_usuario, id_ambiente, tipo_acesso) VALUES (@dtAcesso, @idUsuario, @idAmbiente, @tipoAcesso)", conn);
                                cmd.Parameters.AddWithValue("@dtAcesso", l.DtAcesso);
                                cmd.Parameters.AddWithValue("@idUsuario", l.Usuario.Id);
                                cmd.Parameters.AddWithValue("@idAmbiente", a.Id);
                                cmd.Parameters.AddWithValue("@tipoAcesso", l.TipoAcesso);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (Usuario u in Usuarios)
                        {
                            foreach (Ambiente a in u.Ambientes)
                            {
                                SQLiteCommand cmd = new SQLiteCommand("INSERT INTO permissoes (id_usuario, id_ambiente) VALUES (@idUsuario, @idAmbiente)", conn);
                                cmd.Parameters.AddWithValue("@idUsuario", u.Id);
                                cmd.Parameters.AddWithValue("@idAmbiente", a.Id);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Download()
        {
            string sqlCreateUsuario = "CREATE TABLE IF NOT EXISTS usuarios (" +
                "   id INTEGER PRIMARY KEY," +
                "   nome TEXT" +
                ");";

            string sqlCreateAmbiente = "CREATE TABLE IF NOT EXISTS ambientes (" +
                "   id INTEGER PRIMARY KEY," +
                "   nome TEXT" +
                ");";

            string sqlCreateLog = "CREATE TABLE IF NOT EXISTS logs (" +
                "   dt_acesso DATETIME," +
                "   id_usuario INTEGER," +
                "   id_ambiente INTEGER," +
                "   tipo_acesso BOOLEAN," +
                "   FOREIGN KEY (id_usuario) REFERENCES usuarios (id)," +
                "   FOREIGN KEY (id_ambiente) REFERENCES ambientes (id)" +
                ");";

            string sqlCreatePermissoes = "CREATE TABLE IF NOT EXISTS permissoes (" +
                "   id_usuario INTEGER," +
                "   id_ambiente INTEGER," +
                "   FOREIGN KEY (id_usuario) REFERENCES usuarios (id)," +
                "   FOREIGN KEY (id_ambiente) REFERENCES ambientes (id)" +
                ");";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                new SQLiteCommand(sqlCreateUsuario, conn).ExecuteNonQuery();
                new SQLiteCommand(sqlCreateAmbiente, conn).ExecuteNonQuery();
                new SQLiteCommand(sqlCreateLog, conn).ExecuteNonQuery();
                new SQLiteCommand(sqlCreatePermissoes, conn).ExecuteNonQuery();

                string sqlUsuarios = "SELECT * FROM usuarios";
                SQLiteCommand cmd = new SQLiteCommand(sqlUsuarios, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nome = reader["nome"].ToString();
                    Usuarios.Add(usuario);
                }
                reader.Close();

                string sqlAmbientes = "SELECT * FROM ambientes";
                cmd = new SQLiteCommand(sqlAmbientes, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ambiente ambiente = new Ambiente();
                    ambiente.Id = Convert.ToInt32(reader["id"]);
                    ambiente.Nome = reader["nome"].ToString();
                    Ambientes.Add(ambiente);
                }
                reader.Close();

                string sqlPermissoes = "SELECT * FROM permissoes";
                cmd = new SQLiteCommand(sqlPermissoes, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idUsuario = Convert.ToInt32(reader["id_usuario"]);
                    int idAmbiente = Convert.ToInt32(reader["id_ambiente"]);

                    Usuario usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);
                    Ambiente ambiente = Ambientes.FirstOrDefault(a => a.Id == idAmbiente);

                    if (usuario != null && ambiente != null)
                        usuario.Ambientes.Add(ambiente);
                }
                reader.Close();

                string sqlLogs = "SELECT * FROM logs ORDER BY dt_acesso ASC";
                cmd = new SQLiteCommand(sqlLogs, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int idUsuario = Convert.ToInt32(reader["id_usuario"]);
                    int idAmbiente = Convert.ToInt32(reader["id_ambiente"]);

                    Usuario usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);
                    Ambiente ambiente = Ambientes.FirstOrDefault(a => a.Id == idAmbiente);

                    if (usuario != null && ambiente != null)
                    {
                        Log log = new Log();
                        log.DtAcesso = Convert.ToDateTime(reader["dt_acesso"]);
                        log.TipoAcesso = Convert.ToBoolean(reader["tipo_acesso"]);
                        log.Usuario = usuario;
                        ambiente.Logs.Enqueue(log);
                    }
                }
                reader.Close();
            }
        }
    }
}


