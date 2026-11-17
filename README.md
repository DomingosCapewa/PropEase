# PropEase API

API REST para gerenciar imóveis e proprietários, usando herança e polimorfismo no domínio e persistência com Entity Framework Core e SQLite.

## Destaques técnicos

- Classe abstrata `Imovel`;
- Herança com `Casa` e `Apartamento`;
- Encapsulamento do estado de aluguel;
- Polimorfismo no cálculo e no status do aluguel;
- Persistência com EF Core e SQLite usando herança TPH;
- Swagger para exploração da API.

## Requisitos

- .NET SDK 9.0;
- Windows, Linux ou macOS.

## Executando

Na raiz do projeto:

```bash
dotnet restore
dotnet build
dotnet run
```

As migrações são aplicadas automaticamente durante a inicialização. A API fica disponível em `http://localhost:5150` e o Swagger em `http://localhost:5150/swagger`.

O banco SQLite é criado no diretório do projeto. Para usar outro caminho, altere `ConnectionStrings:DefaultConnection` em um arquivo de configuração local.

## Endpoints

### Proprietários

- `GET /api/proprietarios`
- `GET /api/proprietarios/{id}`
- `POST /api/proprietarios`
- `PUT /api/proprietarios/{id}`
- `DELETE /api/proprietarios/{id}`

Exemplo de criação:

```json
{
  "nome": "Maria Silva",
  "telefone": "(11) 99999-0000",
  "cpf": "123.456.789-00"
}
```

### Imóveis

- `GET /api/imoveis`
- `GET /api/imoveis/{id}`
- `POST /api/imoveis`
- `POST /api/imoveis/alugar/{id}`
- `PUT /api/imoveis/disponibilizar/{id}`
- `DELETE /api/imoveis/{id}`
- `GET /api/imoveis/{id}/calcular-aluguel?periodo=30`

Exemplo de criação:

```json
{
  "tipo": "Casa",
  "endereco": "Rua A, 123",
  "numero": 10,
  "proprietarioId": 1,
  "alugado": false
}
```

## Estrutura principal

- `Models/`: entidades do domínio;
- `Data/`: contexto do Entity Framework Core;
- `Controllers/`: endpoints HTTP;
- `Services/`: regras de aplicação;
- `Migrations/`: migrações do SQLite.
