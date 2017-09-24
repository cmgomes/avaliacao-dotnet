import { Component, OnInit } from '@angular/core';
import { PessoasService} from "../../services/pessoas.service";

@Component({
    selector: 'app-root',
    templateUrl: './pessoas.html',
    styleUrls: ['./pessoas.css']
})

export class PessoasPage implements OnInit{
    pessoas;
    errorMessage;
    itensPorPagina = 5;
    pagina = 1;
    paginas;
    confirmacao = {
        titulo: "Atenção",
        mensagem: "Deseja mesmo remover este registro?"
    };
    filtros: any;

    constructor (private service: PessoasService) {}

    ngOnInit() {
        this.limparFiltros(true);
    }

    limparFiltros(carregar: boolean) {
        this.filtros = {
            campo: null,
            valor: null
        };

        if (carregar) {
            this.carregarListaPessoas();
        }
    }

    filtrar(pagina: number) {
        this.pagina = !pagina ? 1 : pagina;

        if (!this.filtros.campo && !this.filtros.valor) {
            this.carregarListaPessoas();
            return;
        }

        this.service.getTotalPaginasFiltradas(this.filtros.campo, this.filtros.valor, this.itensPorPagina).subscribe((res) => {
            this.paginas = Array(res).fill(0).map((x,i) => i+1);
        });

        this.service.buscarPessoas(this.filtros.campo, this.filtros.valor, this.pagina, this.itensPorPagina).subscribe(
            (res) => {
                this.pessoas = res;
            },

            (error) => {
                this.errorMessage = <any>error
            }
        );
    }

    paginar(pagina: number) {

        if (pagina < 1) {
            pagina = 1;
        }

        if (pagina > this.paginas.length) {
            pagina = this.paginas.length;
        }

        if (pagina == this.pagina) {
            return;
        }

        this.pagina = pagina;
        this.carregarListaPessoas();
    }

    carregarListaPessoas() {
        if (this.filtros.campo != null && this.filtros.valor != null) {
            this.filtrar(this.pagina);
        }

        this.service.getTotalPaginas(this.itensPorPagina).subscribe((res) => {
            this.paginas = Array(res).fill(0).map((x,i) => i+1);
        });

        this.service.getPessoas(this.pagina, this.itensPorPagina).subscribe(
            (res) => {
                this.pessoas = res;
            },

            (error) => {
                this.errorMessage = <any>error
            }
        );
    }

    remover(idPessoa: number) {
        if (!idPessoa) {
            return;
        }

        if (this.service.remover(idPessoa)) {
            this.carregarListaPessoas();
            alert("Registro removido com sucesso.");
        } else {
            alert("Erro na remoção do registro.");
        }
    }

    setItensPorPagina(e) {
        this.itensPorPagina = e;
        this.carregarListaPessoas();
    }
}
