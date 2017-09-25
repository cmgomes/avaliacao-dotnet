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
            return _repositorio.GetAll(1, 1000).ToList();
        }

        [HttpGet("{pagina}/{itensPorPagina}")]
        public IEnumerable<Pessoa> GetPaginado(int pagina, int itensPorPagina)
        {
            if (pagina < 1) {
                pagina = 1;
            }

            return _repositorio.GetAll(pagina, itensPorPagina).ToList();
        }

        // GET api/pessoa/<id>
        [HttpGet("{id}")]
        public Pessoa Get(int id)
        {
            return _repositorio.Get(id);
        }


        // GET api/pessoa/buscar/<campo>/<valor>
        [HttpGet("buscar/{campo}/{valor}")]
        public List<Pessoa> Buscar(string campo, string valor)
        {
            return _repositorio.Buscar(campo, valor).ToList();
        }

        // GET api/pessoa/buscar/<campo>/<valor>/<pagina>/<itens por página>
        [HttpGet("buscar/{campo}/{valor}/{pagina}/{itensPorPagina}")]
        public List<Pessoa> BuscarPaginado(string campo, string valor, int pagina, int itensPorPagina)
        {
            if (pagina < 1) {
                pagina = 1;
            }

            if (itensPorPagina > 1000 || itensPorPagina < 0){
                itensPorPagina = 1000;
            }

            return _repositorio.Buscar(campo, valor, pagina, itensPorPagina).ToList();
        }

        [HttpGet("buscar/paginas/{campo}/{valor}/{itensPorPagina}")]
        public int PaginasBusca(string campo, string valor, int itensPorPagina) {
            if (itensPorPagina > 1000 || itensPorPagina < 0) {
                itensPorPagina = 1000;
            }

            try {
                double paginas = _repositorio.Buscar(campo, valor).Count() / Convert.ToDouble(itensPorPagina);
                return Convert.ToInt32(Math.Ceiling(paginas));
            } catch (Exception e) {
                Console.WriteLine("Erro no método em ApiController::86: " + e.Message);
            }

            return 1;
        }

        // GET api/pessoa/paginas
        [HttpGet("paginas/{itensPorPagina}")]
        public int Paginas(int itensPorPagina) {
            if (itensPorPagina > 1000 || itensPorPagina < 0) {
                itensPorPagina = 1000;
            }

            try {
                return _repositorio.GetTotalPaginas(itensPorPagina);
            } catch (Exception e) {
                Console.WriteLine("Erro no método em ApiController::86: " + e.Message);
            }

            return 1;
        }

        // POST api/pessoa
        [HttpPost]
        public object Post([FromBody]Dictionary<string, string> dados)
        {
            var validator = new PessoaValidator();
            var pessoa = new Pessoa() {
                nome = dados.ContainsKey("nome") ? dados["nome"] : null,
                sexo = dados.ContainsKey("sexo") ? dados["sexo"] : null,
                endereco = dados.ContainsKey("endereco") ? dados["endereco"] : null,
                documento = dados.ContainsKey("documento") ? dados["documento"] : null
            };

            try {
                

                DateTime date;
                if(DateTime.TryParse(dados["dataNascimento"], out date)) {
                    pessoa.dataNascimento = date;
                }

                var result = validator.Validate(pessoa);

                if (!result.IsValid) {
                    return StatusCode(422, result.Errors.Select(r => r.ErrorMessage));
                }

                _repositorio.Add(pessoa);
                
            } catch( Exception e) {
                Console.WriteLine("Erro em ApiController::107-126: " + e.Message);
                return StatusCode(500, "Falha no processamento da informação");
            }

            return pessoa;
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
                return StatusCode(500, "Nenhum registro encontrado com o código informado.");
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

            try {
                _repositorio.Update(pessoa);

                Dictionary<string, string> retorno = new Dictionary<string, string>()
                {
                    {"success", "true"}
                };

                return Json(retorno);
            } catch(Exception e) {
                Console.WriteLine("Erro na atualização de pessoa: " + e.Message);
                return StatusCode(500, "Falha no processamento da informação");
            }
        }

        // DELETE api/pessoa/<id>
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var pessoa = _repositorio.Get(id);
            if (!(pessoa is Pessoa)) {
                return StatusCode(500, "Nenhum registro encontrado com o código informado.");
            }

            try {
                _repositorio.Delete(pessoa);

                Dictionary<string, string> retorno = new Dictionary<string, string>()
                {
                    {"success", "true"}
                };

                return Json(retorno);

            } catch (Exception e) {
                Console.WriteLine("Erro na atualização de pessoa: " + e.Message);
                return StatusCode(500, "Falha no processamento da informação");
            }
            
        }
    }
}
