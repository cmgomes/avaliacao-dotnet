import { Injectable } from '@angular/core';
import {RequestOptions, Response} from '@angular/http';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PessoasService  {
  private _apiUrl = 'http://localhost:5000/api/pessoa/';
  itensPorPagina = 15;

  constructor (private http: HttpClient) {}

  public getPessoas(pagina: number, itensPorPagina: number) {
      if (!pagina) {
        pagina = 1;
      }

      if (!itensPorPagina) {
          itensPorPagina = this.itensPorPagina;
      }

      let tmpUrl = this._apiUrl + pagina + '/' + itensPorPagina;
      return this.http.get(tmpUrl);
  }

  public buscarPessoas(campo: string, valor: any, pagina: number, itensPorPagina: number) {
      if (!pagina) {
          pagina = 1;
      }

      if (!itensPorPagina) {
          itensPorPagina = this.itensPorPagina;
      }

      let tmpUrl = this._apiUrl + 'buscar/' + campo + '/' + valor + '/' + pagina + '/' + itensPorPagina;
      return this.http.get(tmpUrl);
  }

  public getTotalPaginas(itensPorPagina: number) {
      if (!itensPorPagina) {
          itensPorPagina = this.itensPorPagina;
      }

      let tmpUrl = this._apiUrl + 'paginas' + '/' + itensPorPagina;
      return this.http.get(tmpUrl);
  }

  public getTotalPaginasFiltradas(campo:string, valor:any, itensPorPagina: number) {
      if (!itensPorPagina) {
          itensPorPagina = this.itensPorPagina;
      }

      let tmpUrl = this._apiUrl + 'buscar/paginas/' + campo + '/' + valor + '/' + itensPorPagina;
      return this.http.get(tmpUrl);
  }


  public getPessoa(idPessoa: number) {
      let tmpUrl = this._apiUrl +  idPessoa;
      return this.http.get(tmpUrl);
  }

  public salvar(idPessoa: number, dadosPessoa: object) {
      let tmpUrl = this._apiUrl;

      if (idPessoa != null) {
          tmpUrl = tmpUrl + idPessoa;
          return this.http.put(tmpUrl, JSON.stringify(dadosPessoa));
      }

      return this.http.post(tmpUrl, JSON.stringify(dadosPessoa));
  }

  public remover(idPessoa: number) {
      if (!idPessoa) {
          return;
      }
      return this.http.delete(this._apiUrl + idPessoa).subscribe(data => {
          return true;
      }, error => {
          console.clear();
          console.log("Erro na remoção de pessoa: ", error);
          return false;
      });
  }
}
