# ControleLancamentosAPI

## :book: Introdução

O **ControleLancamentosAPI** é uma aplicação de API RESTful desenvolvida para gerenciar e receber lançamentos financeiros. Faz parte do sistema `Controle e Consolidação de Lançamentos` que está melhor descrito [neste repositório](https://github.com/ovinifalcao/ControleEConsolidacaoDeLancamentos). O principal objetivo é fornecer endpoints para registrar movimentações de lançamento de operações financeiras nos seguintes moldes: 

|Campo| Descrição |
|--|--|
| Crédito | Evento de entrada de valores ao Caixa |
| Débito | Evento de saída de valores do Caixa |
| Cancelamento de Crédito | Evento de estorno de valores saindo do Caixa |
| Cancelamento de Débito | Evento de estorno de valores voltando ao Caixa |

## :rocket: Tecnologias

O projeto foi construído utilizando um *stack* moderno da Microsoft e ferramentas de containerização:

* **Framework:** .NET Core API
    - MediatR
    - EF Core
    - Swagger
    - XUnit
    - AutoBogus
* **Data Base:** PostgreSQL
* **Mensageria:** Apache Kafka
* **Contêineres:** Docker

### Pré-requisitos

- .Net SDK 8.0 ou superior
- Docker 

## ⚙️ Como Executar o Projeto

É recomendado que a execução do projeto seja feita utilizando Docker local.

Clone o projeto para execução local:

```bash
git clone https://github.com/ovinifalcao/ControleLancamentosAPI.git
```

Vá para o diretório clonado:

```bash
cd ControleLancamentosAPI
```

Execute os Docker Compose:

```bash
docker-compose up -d
```

Restaure os pacotes .Net
```bash
dotnet restore
```

Vá para o diretório da API:
```bash
cd ControleLancamentosAPI/ControleLancamentosAPI
```

Execute a aplicação:
```bash
dotnet run
```