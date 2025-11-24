/*﻿/*
| Instituto Federal de São Paulo - Campus Cubatão
| Nome: Guilherme Mendes de Sousa - CB3030857
| Nome: Stiven Richardy Silva Rodrigues - CB3030202
| Turma: ADS 471
| 
| Opções no seletor:
| 0. Sair
| 1. Cadastrar ambiente
| 2. Consultar ambiente
| 3. Excluir ambiente
| 4. Cadastrar usuario
| 5. Consultar usuario
| 6. Excluir usuario
| 7. Conceder permissão de acesso ao usuario (informar ambiente e usuário - vincular ambiente ao usuário)
| 8. Revogar permissão de acesso ao usuario (informar ambiente e usuário - desvincular ambiente do usuário)
| 9. Registrar acesso (informar o ambiente e o usuário - registrar o log respectivo)
| 10. Consultar logs de acesso (informar o ambiente e listar os logs - filtrar por logs autorizados/negados/todos)
| Legenda:
| a) Realizar a persistência dos dados quando a aplicação for encerrada (upload)
| b) Fazer a carga dos dados ao executar a aplicação (download)
| Sugestão: 
| -> CRIAR UM MODELO RELACIONAL PARA IMPLEMENTAR A PERSISTÊNCIA DA APLICAÇÃO
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace projeto_acesso
{
    internal class Program
    {
        public static Cadastro cadastro = new Cadastro();
        public static int idAmbiente = 1;
        public static int idUsuario = 1;

        static void Main(string[] args)
        {
            try
            {
                cadastro.Download();
                Utils.MensagemSucesso("Dados carregados com sucesso.");

                if (cadastro.Usuarios.Count > 0) 
                    idUsuario = cadastro.Usuarios.Max(u => u.Id) + 1;

                if (cadastro.Ambientes.Count > 0) 
                    idAmbiente = cadastro.Ambientes.Max(a => a.Id) + 1;
            }
            catch (Exception ex)
            {
                Utils.MensagemErro("Erro ao carregar dados: " + ex.Message);
            }

            int seletor = -1;
            while (seletor != 0)
            {
                Console.Clear();
                Utils.Titulo("PAINEL PRINCIPAL");
                Console.WriteLine(" 0 - Sair\n" +
                    " 1 - Cadastrar Ambiente\n" +
                    " 2 - Consultar Ambiente\n" +
                    " 3 - Excluir Ambiente\n" +
                    " 4 - Cadastrar Usuário\n" +
                    " 5 - Consultar Usuário\n" +
                    " 6 - Excluir Usuário\n" +
                    " 7 - Conceder permissão de acesso ao Usuário\n" +
                    " 8 - Revogar permissão de acesso ao Usuário\n" +
                    " 9 - Registrar Acesso\n" +
                    " 10 - Consultar logs de Acesso");
                Console.WriteLine(new string('-', 70));
                Console.Write(" Escolha uma opção: ");
                seletor = Utils.lerInt(Console.ReadLine(), 0, " Entrada inválida!\n Digite outro número: ");

                switch (seletor)
                {
                    case 0:
                        cadastro.Upload();
                        Console.WriteLine("Dados salvos. Programa finalizado!");
                        break;
                    case 1:
                        CadastrarAmbiente();
                        break;
                    case 2:
                        ConsultarAmbiente();
                        break;
                    case 3:
                        ExcluirAmbiente();
                        break;
                    case 4:
                        CadastrarUsuario();
                        break;
                    case 5:
                        ConsultarUsuario();
                        break;
                    case 6:
                        ExcluirUsuario();
                        break;
                    case 7:
                        PermitirUsuario();
                        break;
                    case 8:
                        BloquearUsuario();
                        break;
                    case 9:
                        RegistrarAcesso();
                        break;
                    case 10:
                        ConsultarLogs();
                        break;
                    default:
                        Utils.MensagemErro("Digite um número de 0 - 10!");
                        break;
                }
            }
        }

        static void CadastrarAmbiente()
        {
            Utils.Titulo("CADASTRAR AMBIENTE");
            Console.Write(" Digite o Nome do Ambiente: ");
            string ambiente = Console.ReadLine();
            Ambiente novoAmbiente = new Ambiente(idAmbiente, ambiente);
            if(cadastro.PesquisarAmbiente(novoAmbiente) == null)
            {
                cadastro.AdicionarAmbiente(novoAmbiente);
                Console.WriteLine($" Id: {novoAmbiente.Id}\n" +
                    $" Nome: {novoAmbiente.Nome}");
                Utils.MensagemSucesso("Ambiente cadastrado!");
                idAmbiente++;
            }
            else
                Utils.MensagemErro("O ambiente já existe.");
        }

        static void ConsultarAmbiente()
        {
            Utils.Titulo("CONSULTAR AMBIENTE");
            Console.Write(" Digite o Nome do Ambiente: ");
            string ambiente = Console.ReadLine();
            Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(ambiente));
            if (ambientePesquisado != null)
            {
                Console.WriteLine($" Id: {ambientePesquisado.Id}\n" +
                    $" Nome: {ambientePesquisado.Nome}");
                Utils.MensagemSucesso("Ambiente encontrado!");
            }
            else
                Utils.MensagemErro("O ambiente não existe.");
        }

        static void ExcluirAmbiente()
        {
            Utils.Titulo("EXCLUIR AMBIENTE");
            Console.Write(" Digite o Nome do Ambiente: ");
            string ambiente = Console.ReadLine();
            Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(ambiente));
            if (cadastro.RemoverAmbiente(ambientePesquisado))
            {
                Console.WriteLine($" Id: {ambientePesquisado.Id}\n" +
                    $" Nome: {ambientePesquisado.Nome}");
                Utils.MensagemSucesso("Ambiente excluído!");
            }
            else
                Utils.MensagemErro("O ambiente não existe.");
        }

        static void CadastrarUsuario()
        {
            Utils.Titulo("CADASTRAR USUÁRIO");
            Console.Write(" Digite o Nome do Usuário: ");
            string usuario = Console.ReadLine();
            Usuario novoUsuario = new Usuario(idUsuario, usuario);
            if (cadastro.PesquisarUsuario(novoUsuario) == null)
            {
                cadastro.AdicionarUsuario(novoUsuario);
                Console.WriteLine($" Id: {novoUsuario.Id}\n" +
                    $" Nome: {novoUsuario.Nome}");
                Utils.MensagemSucesso("Usuário cadastrado!");
                idUsuario++;
            }
            else
                Utils.MensagemErro("O usuário já existe.");
        }

        static void ConsultarUsuario()
        {
            Utils.Titulo("CONSULTAR USUÁRIO");
            Console.Write(" Digite o Nome do Usuário: ");
            string usuario = Console.ReadLine();
            Usuario usuarioPesquisado = cadastro.PesquisarUsuario(new Usuario(usuario));
            if (usuarioPesquisado != null)
            {
                Console.WriteLine($" Id: {usuarioPesquisado.Id}\n" +
                    $" Nome: {usuarioPesquisado.Nome}");
                Utils.MensagemSucesso("Usuário encontrado!");
            }
            else
                Utils.MensagemErro("O usuário não existe.");
        }

        static void ExcluirUsuario()
        {
            Utils.Titulo("EXCLUIR USUÁRIO");
            Console.Write(" Digite o Nome do Usuário: ");
            string usuario = Console.ReadLine();
            Usuario usuarioPesquisado = cadastro.PesquisarUsuario(new Usuario(usuario));
            if (cadastro.RemoverUsuario(usuarioPesquisado))
            {
                Console.WriteLine($" Id: {usuarioPesquisado.Id}\n" +
                    $" Nome: {usuarioPesquisado.Nome}");
                Utils.MensagemSucesso("Usuário excluído!");
            }
            else
                Utils.MensagemErro("O usuário não existe ou não pode ser removido.");
        }

        static void PermitirUsuario()
        {
            Utils.Titulo("PERMITIR USUÁRIO");
            Console.Write(" Digite o Nome do Usuário: ");
            string nomeUsuario = Console.ReadLine();
            Usuario usuarioPesquisado = cadastro.PesquisarUsuario(new Usuario(nomeUsuario));
            if (usuarioPesquisado != null)
            {
                Console.Write(" Digite o Ambiente: ");
                string nomeAmbiente = Console.ReadLine();
                Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(nomeAmbiente));
                if (usuarioPesquisado.ConcederPermissao(ambientePesquisado))
                    Utils.MensagemSucesso($"Acesso concedido ao espaço {ambientePesquisado.Nome}");
                else
                    Utils.MensagemErro("Não foi possível conceder a permissão");
            }
            else
                Utils.MensagemErro("O usuário não existe.");
        }

        static void BloquearUsuario()
        {
            Utils.Titulo("BLOQUEAR USUÁRIO");
            Console.Write(" Digite o Nome do Usuário: ");
            string usuario = Console.ReadLine();
            Usuario usuarioPesquisado = cadastro.PesquisarUsuario(new Usuario(usuario));
            if (usuarioPesquisado != null)
            {
                Console.Write(" Digite o nome do Ambiente: ");
                string nomeAmbiente = Console.ReadLine();
                Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(nomeAmbiente));
                if (usuarioPesquisado.RevogarPermissao(ambientePesquisado))
                    Utils.MensagemSucesso($"Acesso removido ao ambiente {ambientePesquisado.Nome}");
                else
                    Utils.MensagemErro($"Não foi possível remover o acesso");
            }
            else
                Utils.MensagemErro("O usuário não existe.");
        }

        static void RegistrarAcesso()
        {
            Utils.Titulo("REGISTRAR ACESSO");
            Console.Write(" Digite o nome do Usuário: ");
            string nomeUsuario = Console.ReadLine();
            Usuario usuarioPesquisado = cadastro.PesquisarUsuario(new Usuario(nomeUsuario));
            if (usuarioPesquisado != null)
            {
                Console.Write(" Digite o nome do Ambiente: ");
                string nomeAmbiente = Console.ReadLine();
                Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(nomeAmbiente));
                if (ambientePesquisado != null)
                {
                    bool autorizado = usuarioPesquisado.Ambientes.Any(a => a.Id == ambientePesquisado.Id);
                    Log novoLog = new Log(DateTime.Now, usuarioPesquisado, autorizado);
                    ambientePesquisado.RegistrarLog(novoLog);
                    Utils.MensagemSucesso($"Registro de acesso {(novoLog.TipoAcesso ? "efetuado" : "negado")} ao ambiente {ambientePesquisado.Nome}");
                }
                else
                    Utils.MensagemErro("Ambiente não cadastrado");
            }
            else
                Utils.MensagemErro("Usuário não existe");
        }

        static void ConsultarLogs()
        {
            Utils.Titulo("CONSULTAR LOGS");
            Console.Write(" Digite o nome do Ambiente: ");
            string nomeAmbiente = Console.ReadLine();
            Ambiente ambientePesquisado = cadastro.PesquisarAmbiente(new Ambiente(nomeAmbiente));
            if (ambientePesquisado != null)
            {
                Console.WriteLine(new string('-', 70));
                Console.WriteLine(" Filtros disponíveis:\n" +
                    " 1 - Acessos bem-sucedidos\n" +
                    " 2 - Acessos negados\n" +
                    " 3 - Todos");
                Console.WriteLine(new string('-', 70));
                Console.Write(" Informe o filtro: ");
                int seletor = Utils.lerMinMax(Console.ReadLine(), 1, 3, "Filtro inválido. Tente Novamente: "); 
                switch (seletor){
                    case 1:
                        if (ambientePesquisado.Logs?.Any(lg => lg.TipoAcesso) == true)
                        {
                            Console.WriteLine(new string('-', 70));
                            foreach (Log log in ambientePesquisado.Logs.Where(lg => lg.TipoAcesso))
                            {
                                Console.WriteLine($" Data de acesso: {log.DtAcesso}\n Tipo: Acesso permitido\n Usuário: {log.Usuario.Nome}");
                                Console.WriteLine(new string('-', 70));
                            }
                            Utils.MensagemSucesso("Logs de acesso bem-sucedido encontrados");
                        }
                        else
                            Utils.MensagemErro("Nenhum log de acesso bem-sucedido");
                        break;
                    case 2:
                        Console.WriteLine(new string('-', 70));
                        if (ambientePesquisado.Logs?.Any(lg => !lg.TipoAcesso) == true)
                        {
                            foreach (Log log in ambientePesquisado.Logs.Where(lg => !lg.TipoAcesso))
                            {
                                Console.WriteLine($" Data de acesso: {log.DtAcesso}\n Tipo: Acesso negado\n Usuário: {log.Usuario.Nome}");
                                Console.WriteLine(new string('-', 70));
                            }
                            Utils.MensagemSucesso("Logs de acesso negado encontrados");
                        }
                        else
                            Utils.MensagemErro("Nenhum log de acesso negado");
                        break;
                    case 3:
                        if (ambientePesquisado.Logs?.Count > 0)
                        {
                            Console.WriteLine(new string('-', 70));
                            foreach (Log log in ambientePesquisado.Logs)
                            {
                                Console.WriteLine($" Data de acesso: {log.DtAcesso}\n Tipo: Acesso {(log.TipoAcesso ? "permitido" : "negado")}\n Usuário: {log.Usuario.Nome}");
                                Console.WriteLine(new string('-', 70));
                            }
                            Utils.MensagemSucesso("Logs de acesso encontrados");
                        }
                        else
                            Utils.MensagemErro("Nenhum log encontrado");
                        break;
                }

            }
            else
                Utils.MensagemErro("Ambiente não cadastrado");
        }
    }
}
