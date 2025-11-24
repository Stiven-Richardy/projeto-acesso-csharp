# 🏢 Projeto: Controle de Acesso (C# Console)

Este projeto acadêmico foi desenvolvido como parte da disciplina de **Estrutura de Dados**, com o objetivo de aplicar conceitos de manipulação de listas e filas (`List<T>` e `Queue<T>`) em uma aplicação **C# Console**. O sistema simula o controle de acesso físico a ambientes, gerenciando permissões de usuários e registrando logs de entrada em um banco de dados **SQLite**.

## 🎯 Objetivos

- **Gerenciamento de Entidades**: Implementar o cadastro e exclusão de `Usuários` e `Ambientes`.
- **Controle de Permissões**: Desenvolver lógica para conceder e revogar o acesso de usuários a ambientes específicos (Relacionamento N:N).
- **Sistema de Logs (Fila)**: Registrar tentativas de acesso (autorizadas ou negadas) utilizando uma `Queue<Log>`, limitando o histórico a 100 registros por ambiente (FIFO).
- **Persistência de Dados**: Implementar a persistência relacional utilizando o banco de dados embutido **SQLite**, garantindo que dados e logs sejam salvos ao encerrar (`Upload`) e carregados ao iniciar (`Download`).

## 🛠️ Ferramentas Utilizadas

- C# (.NET Framework / Core)
- Visual Studio
- SQLite (Banco de Dados)
- Biblioteca `System.Data.SQLite`
- Git e GitHub

## 🗄️ Estrutura do Banco de Dados

O projeto utiliza o banco de dados **SQLite** (arquivo `database.db`) com as seguintes tabelas:

#### Tabela: `usuarios`
| Column Name | Data Type | Allow Nulls | Descrição |
| :--- | :--- | :--- | :--- |
| **id** | `INTEGER` | Não | Chave Primária (PK) |
| **nome** | `TEXT` | Sim | Nome do usuário |

#### Tabela: `ambientes`
| Column Name | Data Type | Allow Nulls | Descrição |
| :--- | :--- | :--- | :--- |
| **id** | `INTEGER` | Não | Chave Primária (PK) |
| **nome** | `TEXT` | Sim | Nome do ambiente/sala |

#### Tabela: `permissoes`
| Column Name | Data Type | Allow Nulls | Descrição |
| :--- | :--- | :--- | :--- |
| **id_usuario** | `INTEGER` | Não | Chave Estrangeira (FK) para `usuarios` |
| **id_ambiente** | `INTEGER` | Não | Chave Estrangeira (FK) para `ambientes` |

#### Tabela: `logs`
| Column Name | Data Type | Allow Nulls | Descrição |
| :--- | :--- | :--- | :--- |
| **dt_acesso** | `DATETIME` | Não | Data e hora do registro |
| **id_usuario** | `INTEGER` | Não | Chave Estrangeira (FK) para `usuarios` |
| **id_ambiente** | `INTEGER` | Não | Chave Estrangeira (FK) para `ambientes` |
| **tipo_acesso** | `BOOLEAN` | Não | Status (True=Permitido, False=Negado) |

## 🗂️ Estrutura do Projeto
```
📁 projeto-acesso-csharp/
├── 📁 projeto-acesso
|   ├── 📄 Program.cs
|   ├── 📄 Cadastro.cs
|   ├── 📄 Usuario.cs
|   ├── 📄 Ambiente.cs
|   ├── 📄 Log.cs
│   └── 📄 Utils.cs
├── 📄 projeto-acesso.sln
├── 📄 .gitignore
└── 📄 README.md
```

## 🚀 Como Executar

1. Abra a IDE **Visual Studio 2022**.
2. Vá em **Clonar um Repositório** e digite o link `https://github.com/Stiven-Richardy/projeto-acesso-csharp`.
3. Selecione a pasta desejada e clone o projeto.
4. Certifique-se de instalar o pacote NuGet do SQLite. No Console do Gerenciador de Pacotes, execute: `Install-Package System.Data.SQLite`.
5. Execute a aplicação a partir do Visual Studio.
Obs.: O arquivo `database.db` será criado automaticamente na pasta de execução (bin/Debug) na primeira inicialização.

## 👨‍🏫 Autores

- **Stiven Richardy Silva Rodrigues**  
  Estudante de Análise e Desenvolvimento de Sistemas | IFSP — Campus Cubatão  
  [@Stiven-Richardy](https://github.com/Stiven-Richardy)

- **Guilherme Mendes de Sousa**  
  Estudante de Análise e Desenvolvimento de Sistemas | IFSP — Campus Cubatão  
  [@Guilh3rme-M3ndes](https://github.com/Guilh3rme-M3ndes)

## 📚 Referências

- C# Reference: [Microsoft C#](https://learn.microsoft.com/pt-br/visualstudio/get-started/csharp/?view=vs-2022)
