# Controle de Estacionamento

Este projeto é um sistema de gerenciamento de estacionamento que permite marcar entradas e saídas de veículos, além de calcular a duração do estacionamento e o preço a ser pago. O projeto foi desenvolvido utilizando o ASP.NET MVC e o Entity Framework com SQLite.

## Estrutura do Projeto:

app: Projeto principal que contém a lógica da aplicação.

test: Projeto de testes que contém os testes unitários.

## Configuração e Inicialização da Aplicação

No projeto `app`

Restaure as dependências do projeto com o comando:

```
dotnet restore
```

Compile o projeto executando:

```
dotnet build
```

Para criar uma nova migração execute o comando:

```
dotnet ef migrations add <nome-da-migracao>
```

Para aplicar as migrações ao banco de dados utilize:

```
dotnet ef database update
```

Inicie o servidor com o comando:

```
dotnet run
```

## Configuração e Inicialização dos testes

No projeto `test`

Restaure as dependências do projeto com o comando:

```
dotnet restore
```

Compile o projeto executando:

```
dotnet build
```

Para executar os testes utilize:

```
dotnet test
```
