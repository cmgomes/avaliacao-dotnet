import { Component, OnInit } from '@angular/core';
import { PessoasService} from "../../../services/pessoas.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './formulario.html',
  styleUrls: ['./formulario.css']
})

export class FormularioPage implements OnInit {
    acao;
    idPessoa = null;
    dadosPessoa: any;

    constructor(private service: PessoasService, private route: ActivatedRoute) { }

    ngOnInit() {

        this.dadosPessoa = {
            id: null,
            nome: null,
            endereco: null,
            documento: null,
            sexo: null,
            dataNascimento: null
        };

      this.route.params.subscribe(params => {
        if (params.id) {
            this.idPessoa = params.id;
        }

        this.acao = this.idPessoa ? "Edição" : "Cadastro";
        if (this.idPessoa) {
          this.carregarPessoa();
        }
      });
    }

    carregarPessoa() {
        this.service.getPessoa(this.idPessoa).subscribe((res) => {
            if (res) {
                this.dadosPessoa = res;
            }
        });
    }

    salvar() {
        this.service.salvar(this.idPessoa, this.dadosPessoa).subscribe((res) => {
          console.log(res);
        });
    }
}
