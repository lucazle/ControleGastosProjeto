# ControleGastos — Sistema de Controle de Gastos Residenciais

Aplicação completa de controle de gastos residenciais desenvolvida com **ASP.NET Core Web API** e **front-end em React + TypeScript**, seguindo separação de responsabilidades em camadas (Controller → Service → DAO → Banco de Dados).

---

## Tecnologias Utilizadas

- Back-end: C# / .NET 8 / ASP.NET Core Web API
- ORM: Entity Framework Core 8 (SQLite)
- Banco de Dados: SQLite
- Front-end: React + TypeScript (Vite)
- Versionamento: Git

---

## Arquitetura do Projeto

```
ControleGastos/
├── Backend/
│   ├── Controllers/           ← recebe e responde requisições HTTP
│   │   ├── PessoaController.cs
│   │   ├── TransacaoController.cs
│   │   └── RelatorioController.cs
│   ├── DAO/                   ← acesso ao banco de dados
│   │   ├── Interfaces/
│   │   │   ├── IPessoaDao.cs
│   │   │   └── ITransacaoDao.cs
│   │   ├── PessoaDao.cs
│   │   └── TransacaoDao.cs
│   ├── Data/                  ← contexto do Entity Framework
│   │   └── AppDbContext.cs
│   ├── DTO/                   ← objetos de transferência de dados
│   │   ├── Pessoas/
│   │   ├── Transacoes/
│   │   └── Relatorios/
│   ├── Models/                ← entidades do sistema
│   │   ├── Pessoa.cs
│   │   ├── Transacao.cs
│   │   └── TipoTransacao.cs
│   ├── Services/               ← regras de negócio
│   │   ├── Interfaces/
│   │   │   ├── IPessoaService.cs
│   │   │   ├── ITransacaoService.cs
│   │   │   └── IRelatorioService.cs
│   │   ├── PessoaService.cs
│   │   ├── TransacaoService.cs
│   │   └── RelatorioService.cs
│   ├── Migrations/             ← histórico versionado do schema do banco
│   └── Program.cs
└── frontend/
    ├── src/
    │   ├── components/          ← telas (Pessoas, Transações, Totais)
    │   ├── services/            ← chamadas HTTP para a API
    │   └── types/               ← tipos TypeScript (espelham os DTOs)
    └── package.json
```

---

## Padrão de Camadas

```
Controller → Service → DAO → Banco de Dados
```

| Camada | Responsabilidade |
|---|---|
| Controller | Recebe requisições HTTP, aciona o Service, retorna respostas |
| Service | Aplica as regras de negócio e converte entidade ↔ DTO |
| DAO | Executa operações no banco de dados via EF Core |
| Model | Representa as entidades do sistema (mapeadas para tabelas) |
| DTO | Controla o formato do que entra e sai da API |

---

## Organização do Front-end
 
| Pasta | Responsabilidade |
|---|---|
| `components/` | Telas da aplicação (Pessoas, Transações, Totais) — estrutura visual e interação do usuário |
| `services/` | Funções que fazem as chamadas HTTP para a API do back-end |
| `types/` | Tipos TypeScript que espelham o formato dos DTOs do back-end |

---

## Funcionalidades

**Pessoas**
- [x] Cadastrar pessoa (nome, idade)
- [x] Listar todas as pessoas
- [x] Remover pessoa (remove também, em cascata, todas as suas transações)

**Transações**
- [x] Cadastrar transação (descrição, valor, tipo, pessoa)
- [x] Listar todas as transações
- [x] Validação: a pessoa informada precisa existir
- [x] Validação: pessoas menores de 18 anos só podem cadastrar despesas

**Relatório**
- [x] Totais de receitas, despesas e saldo por pessoa
- [x] Total geral somando todas as pessoas

---

## Modelo de Dados

### Pessoa
| Campo | Tipo | Descrição |
|---|---|---|
| Id | int | Chave primária (auto incremento) |
| Nome | string | Nome da pessoa (obrigatório) |
| Idade | int | Idade da pessoa |

### Transacao
| Campo | Tipo | Descrição |
|---|---|---|
| Id | int | Chave primária (auto incremento) |
| Descricao | string | Descrição da transação (obrigatória) |
| Valor | decimal | Valor da transação |
| Tipo | enum | `Despesa` ou `Receita` |
| PessoaId | int | Chave estrangeira — precisa referenciar uma pessoa existente |

---

## Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (para o front-end)
- [Git](https://git-scm.com/)

### Passo a passo

**1. Clone o repositório**
```bash
git clone <url-do-repositorio>
cd ControleGastos
```

**2. Rode o back-end**

Abra o arquivo `ControleGastos.slnx` no Visual Studio e rode o projeto (F5), ou via terminal:
```bash
cd Backend
dotnet restore
dotnet ef database update
dotnet run
```

O banco de dados SQLite (`controle_gastos.db`) é criado automaticamente na primeira execução, e as migrations garantem que o schema esteja atualizado.

A API estará disponível em algo como `https://localhost:7279` (confira a porta exibida no seu terminal/Visual Studio).

**3. Rode o front-end**

Em outro terminal:
```bash
cd frontend
npm install
npm run dev
```

O front-end estará disponível em `http://localhost:5173`.

> **Atenção:** se a porta do back-end for diferente de `7279`, ajuste a constante `API_URL` nos arquivos dentro de `frontend/src/services/`.

---

## Endpoints da API

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/pessoas` | Lista todas as pessoas |
| POST | `/api/pessoas` | Cadastra uma nova pessoa |
| DELETE | `/api/pessoas/{id}` | Remove uma pessoa (e suas transações, em cascata) |
| GET | `/api/transacoes` | Lista todas as transações |
| POST | `/api/transacoes` | Cadastra uma nova transação |
| GET | `/api/relatorio` | Retorna os totais por pessoa e o total geral |

### Exemplo de requisição — Cadastrar pessoa
```json
POST /api/pessoas
{
    "nome": "Ana Souza",
    "idade": 34
}
```

### Exemplo de requisição — Cadastrar transação
```json
POST /api/transacoes
{
    "descricao": "Salário mensal",
    "valor": 5200.00,
    "tipo": "Receita",
    "pessoaId": 1
}
```

### Exemplo de resposta — Relatório de totais
```json
{
    "pessoas": [
        {
            "pessoaId": 1,
            "nome": "Ana Souza",
            "totalReceitas": 5200.00,
            "totalDespesas": 2020.00,
            "saldo": 3180.00
        }
    ],
    "totalReceitasGeral": 5200.00,
    "totalDespesasGeral": 2020.00,
    "saldoGeral": 3180.00
}
```

---

## Validações e Regras de Negócio

| Campo | Regra |
|---|---|
| Nome | Não pode ser vazio |
| Descrição | Não pode ser vazio |
| Valor da transação | Deve ser maior que zero |
| Selecionar uma pessoa | Transação precisa referenciar uma pessoa existente |

Pessoas menores de 18 anos só podem cadastrar despesas

Todas as validações lançam uma exceção capturada pelo respectivo Controller, que responde com `400 Bad Request` e uma mensagem explicando o motivoo e exibe essa mensagem na tela.

---

## Autor

Desenvolvido por **Lucas Souza**
