# Avaliação Cristiano Gomes - Frontend

- Este projeto foi gerado com [Angular CLI](https://github.com/angular/angular-cli) versão 1.4.3.
- Desenvolvido utilizando IDE PHPStorm

## Requisitos
- É necessário realizar a instalação dos pacotes rxjs, angular-cli e opcionalmente yarn
``npm install --save-dev rxjs``
``npm install -g @angular/cli``
``npm install -g yarn``

## Para executar em desenvolvimento
- Rodar o comando `yarn install`
- Rodar o comando `ng serve --open` ou apenas ``yarn install`` e navegar até `http://localhost:4200/`


# Avaliação Cristiano Gomes - Backend

Este projeto foi gerado utilizando [.Net Core 2.0](https://www.microsoft.com/net/download/core), através da IDE Visual Code

## Requisitos
- Ferramentas necessárias: dotnet core 2 na linha de comando
- Banco de dados postgre

## Banco de dados
- O contexto utilizado foi configurado para utilizar banco postgre
- Para configurar string de conexão, favor editar appsettings.json

## Para executar em desenvolvimento
- Após configurar a string de conexão rodar o comando ``dotnet ef database update``
- E para subir o servidor localmente: `dotnet run`

## Endpoints
Todos os endpoints retornam quantidade máxima de 1000 itens por página, mesmo que o valor solicitado seja superior

- Buscar todos os registros: `GET:/api/pessoa`
- Efetuar busca paginada por todos os registros: `GET:/api/pessoa/<pagina>/<itens_por_pagina>`
- Efetuar busca `GET:/api/pessoa/buscar/<campo>/<valor_do_campo>`
  exemplo: ```/api/pessoa/buscar/nome/Cristiano```. Obs: Para filtrar por data de nascimento, 
  o formato esperado pela api é:`yyyy-MM-dd``
- Efetuar busca paginada:  `GET:/api/pessoa/buscar/<campo>/<valor_do_campo>/<pagina>/<itens_por_pagina>`
  exemplo: ```/api/pessoa/buscar/sexo/f/1/50```
- Buscar quantidade de páginas: ``GET:/api/pessoa/paginas/<itens_por_pagina>`` exemplo: ```/api/pessoa/paginas/5```
- Efetuar cadastro de nova pessoa: ``POST:/api/pessoa`` com os parâmetros de exemplo com header ``Content-type: application/json``:
```javascript
{
	"nome": "Fernando Pessoa",
	"endereco": "Av João Pessoa",
	"sexo": "m",
	"dataNascimento": "1888-06-13",
	"documento": "O marinheiro"
}
```
-Efetuar edição de pessoa existente: ``PUT:/api/pessoa/<id_pessoa>``com os parâmetros de exemplo com header ``Content-type: application/json``:
```javascript
{
	"nome": "Fernando Pessoa",
	"endereco": "Av João Pessoa",
	"sexo": "m",
	"dataNascimento": "1888-06-13",
	"documento": "O marinheiro"
}
```
Observação: se na edição for enviado o Id no corpo dos parametros o mesmo será ignorado, sendo válido apenas o da URL.

- Apagar o registro de uma pessoa existente: ``DELETE:/api/pessoa/<id_pessoa>`` exemplo: ``DELETE:/api/pessoa/1``
