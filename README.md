# **NEPPO - Teste Frontend Web Based** #

#Projeto a ser desenvolvido:

Desenvolver projeto web based utilizando as tecnologias html5, javascript (ecmascript 2015), css3.
Funcionalidades que deverão ser desenvolvidas:

	CRUD de pessoas
		Criar tela de pesquisa, cadastro, edição e exclusão de pessoas.
			Objeto "Pessoa" terá os seguintes atributos: nome, data nascimento, documento de identificação, sexo, endereco.
			Incluir validação de obrigatório para todos os campos: documento de identificação, nome, data nascimento e sexo.
			
	Relatorio de pessoas
		Criar tela que contenha resultados graficos das pessoas.
			Grafico da faixa de idades das pessoas, faixas: ["0 a 9", "10 a 19", "20 a 29", "30 a 39", "Maior que 40"]
			Grafico contendo quantidade de pessoas que tenham sexo masculino e feminino
	
	Utilizar servico RESTful:
		endpoint: https://test-frontend-neppo.herokuapp.com/pessoas/
[Documentacao servico REST para referencia](https://test-frontend-neppo.herokuapp.com/apipie/): (https://test-frontend-neppo.herokuapp.com/apipie/)


#Pré requisitos, tecnologias:

	Javascript:
		- Utilizar ES6+
		- Criar projeto modularizado, aplicar conceitos ECMAScript 6 modules, AMD ou CommonJS(http://2ality.com/2014/09/es6-modules-final.html)

	HTML e CSS:
		- Utilizar HTML5
		- Utilizar CSS3(https://developer.mozilla.org/pt-BR/docs/Web/CSS/CSS3) & SASS(se preciso)
		- Desenvolver design responsivo

	UX:
		- Desenvolva pensando e aplicando conceitos de UX (http://designculture.com.br/conceitos-fundamentais-de-um-bom-ux)
	


#Hint (dicas):
	Abaixo contem algumas dicas para tornar seu projeto melhor:
		- Tente desenvolver testes unitarios
		- Tente utilizar padroes de projetos(design patterns)
		- Tente desenvolver usando o conceito single Page Application (SPA)
		- Tente usar algum framework JS, como por exemplo: Angular, Vue.js, React, AngularJS.
	  	- Tente utilizar algum framework de view, como por exemplo: bootstrap, google-material-design, metro-io
		- Tente usar Flux/Redux
		- Tente utilizar alguma ferramenta facilitadora para "unificar, minificar e obfuscar arquivos estaticos", como: webpack, gulp, grunt, etc.
		- Tente utilizar algum gerenciador de pacotes javascript(gerenciador de dependencias), como: yarn, NPM, Bower. 
		- Tente usar Typescript (se tiver conhecimento pode optar pelo desenvolvimento deste ao inves de javascript)
		- Tente ter import de apenas um arquivo JS no arquivo HTML.				
		- Tente ter import de apenas um arquivo CSS no arquivo HTML.


Crie um fork deste repo, faça o desenvolvimento necessario e nos envie a url.
Caso tenha problemas em criar um fork, siga o link [How to Fork in bitbucket](https://confluence.atlassian.com/bitbucket/forking-a-repository-221449527.html#ForkingaRepository-HowtoForkaRepository)

Sugestão de editores de texto (IDE): [VSCODE](https://code.visualstudio.com/), [WebStorm](https://www.jetbrains.com/webstorm/), [Atom](https://atom.io/), [SublimeText](https://www.sublimetext.com/)


# Avaliação Cristiano Gomes - Frontend - Neppo

- Este projeto foi gerado com [Angular CLI](https://github.com/angular/angular-cli) versão 1.4.3.
- Desenvolvido utilizando IDE PHPStorm

## Requisitos
- Ferramentas necessárias: angular-cli, yarn

## Para executar em desenvolvimento
- Rodar o comando `yarn install`
- Rodar o comando `ng serve --open` e navegar até `http://localhost:4200/`


# Avaliação Cristiano Gomes - Backend - Neppo

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
