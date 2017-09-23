using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Neppo.Models;
using Neppo.Repositories.Interfaces;
using Neppo.Validators;

namespace Neppo.Controllers
{
    [Route("api/pessoa")]
    public class ApiController : Controller
    {
        private readonly IPessoaRepository _repositorio;

        public ApiController (IPessoaRepository repo) 
        {
            _repositorio = repo;
        }

        // GET api/pessoa
        [HttpGet]
        public IEnumerable<Pessoa> Get()
        {
            return _repositorio.GetAll().ToList();
        }

        // GET api/pessoa/<id>
        [HttpGet("{id}")]
        public Pessoa Get(int id)
        {
            return _repositorio.Get(id);
        }

        // GET api/pessoa/<campo>/<valor>
        [HttpGet("{campo}/{valor}")]
        public List<Pessoa> Get(string campo, string valor)
        {
            return _repositorio.Buscar(campo, valor).ToList();
        }

        // GET api/pessoa/<campo>/<valor>/<pagina>/<itens por página>
        [HttpGet("{campo}/{valor}/{pagina}/{itensPorPagina}")]
        public List<Pessoa> Get(string campo, string valor, int pagina, int itensPorPagina)
        {
            if (itensPorPagina > 1000){
                itensPorPagina = 1000;
            }

            return _repositorio.Buscar(campo, valor, pagina, itensPorPagina).ToList();
        }

        // POST api/pessoa
        [HttpPost]
        public object Post([FromBody]Dictionary<string, string> dados)
        {
            var validator = new PessoaValidator();

            try {
                var pessoa = new Pessoa() {
                    nome = dados.ContainsKey("nome") ? dados["nome"] : null,
                    sexo = dados.ContainsKey("sexo") ? dados["sexo"] : null,
                    endereco = dados.ContainsKey("endereco") ? dados["endereco"] : null,
                    documento = dados.ContainsKey("documento") ? dados["documento"] : null
                };

                DateTime date;
                if(DateTime.TryParse(dados["dataNascimento"], out date)) {
                    pessoa.dataNascimento = date;
                }

                var result = validator.Validate(pessoa);

                if (!result.IsValid) {
                    return StatusCode(422, result.Errors.Select(r => r.ErrorMessage));
                }

                _repositorio.Add(pessoa);
                return pessoa;
            } catch( Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/pessoa/<id>
        [HttpPut("{id}")]
        public object Put(int id, [FromBody]Dictionary<string, string> dados)
        {
            if (!dados.ContainsKey("id")) {
                dados.Add("id", id.ToString());
            } else {
                dados["id"] = id.ToString();
            }

            var pessoa = _repositorio.Get(id);
            if (!(pessoa is Pessoa)) {
                return StatusCode(422, "Nenhum registro encontrado com o código informado.");
            }

            foreach (var chave in dados.Keys) {
                if (chave == "id"){
                    continue;
                }

                if (chave == "dataNascimento") {
                    pessoa.dataNascimento = null;

                    DateTime date;
                    if (DateTime.TryParse(dados["dataNascimento"], out date)) {
                        pessoa.dataNascimento = date;
                    }
                } else {
                    var propertyInfo = pessoa.GetType().GetProperty(chave);
                    propertyInfo.SetValue(pessoa, Convert.ChangeType(dados[chave], propertyInfo.PropertyType), null);
                }
            }

            var validator = new PessoaValidator();
            var result = validator.Validate(pessoa);

            if (!result.IsValid) {
                return StatusCode(422, result.Errors.Select(r => r.ErrorMessage));
            }

            _repositorio.Update(pessoa);
            return pessoa;
        }

        // DELETE api/pessoa/<id>
        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            var pessoa = _repositorio.Get(id);
            if (!(pessoa is Pessoa)) {
                return StatusCode(422, "Nenhum registro encontrado com o código informado.");
            }

            _repositorio.Delete(pessoa);

            return StatusCode(200, "");
        }
    }
}
