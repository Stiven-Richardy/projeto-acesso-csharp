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
using System.Text;
using System.Threading.Tasks;

namespace projeto_acesso
{
    internal class Program
    {
        public static Cadastro cadastro = new Cadastro();
        public static int idAmbiente = 0;

        static void Main(string[] args)
        {
            int seletor = -1;
            while (seletor != 0)
            {
                Console.Clear();
                Utils.Titulo("PAINEL PRINCIPAL");
                Console.WriteLine(" 0 - Sair\n" +
                    " 1 - Cadastrar ambiente\n" +
                    " 2 - Consultar ambiente\n" +
                    " 3 - Excluir ambiente\n" +
                    " 4 - Cadastrar usuario\n" +
                    " 5 - Consultar usuario\n" +
                    " 6 - Excluir usuario\n" +
                    " 7 - Conceder permissão de acesso ao usuario\n" +
                    " 8 - Revogar permissão de acesso ao usuario\n" +
                    " 9 - Registrar acesso\n" +
                    " 10 - Consultar logs de acesso");
                Console.WriteLine(new string('-', 70));
                Console.Write(" Escolha uma opção: ");
                seletor = Utils.lerInt(Console.ReadLine(), 0, " Entrada inválida!\n  Digite outro número: ");

                switch (seletor)
                {
                    case 0:
                        Console.WriteLine(" Programa finalizado!");
                        break;
                    case 1:
                        CadastrarAmbiente();
                        break;
                    case 2:
                        ConsultarAmbiente();
                        break;
                    case 3:
                        // ExcluirAmbiente();
                        break;
                    case 4:
                        // Cadastrarusuario();
                        break;
                    case 5:
                        // ConsultarUsuario();
                        break;
                    case 6:
                        // ExcluirUsuario();
                        break;
                    case 7:
                        // ConcederPermissao();
                        break;
                    case 8:
                        // RevogarPermissao();
                        break;
                    case 9:
                        // RegistrarAcesso();
                        break;
                    case 10:
                        // ConsultarLogs();
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
            {
                Utils.MensagemErro("O ambiente já existe.");
            }
        }

        static void ConsultarAmbiente()
        {
            Utils.Titulo("CONSULTAR AMBIENTE");
            Console.Write(" Digite o Nome do Ambiente: ");
            string ambiente = Console.ReadLine();
            Ambiente ambientePesquisado = new Ambiente(ambiente);
            ambientePesquisado = cadastro.PesquisarAmbiente(ambientePesquisado);
            if (ambientePesquisado != null)
            {
                Console.WriteLine($" Id: {ambientePesquisado.Id}\n" +
                    $" Nome: {ambientePesquisado.Nome}");
                Utils.MensagemSucesso("Ambiente encontrado!");
            }
            else
            {
                Utils.MensagemErro("O ambiente não existe.");
            }
        }
    }
}
