import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {ConfirmationPopoverModule} from 'angular-confirmation-popover';

import { Interceptor } from './interceptor';

import { AppPage } from './pages/App/app';
import { HomePage } from './pages/home/home';
import { PessoasPage } from './pages/pessoas/pessoas';
import { NotFoundPage } from './pages/404/not-found-page';
import { FormularioPage } from './pages/pessoas/formulario/formulario';

import {PessoasService} from "./services/pessoas.service";

const appRoutes: Routes = [
    {path: 'home', component: HomePage},
    {path: 'pessoas', component: PessoasPage},
    {path: 'pessoas/formulario', component: FormularioPage},
    {path: 'pessoas/formulario/:id', component: FormularioPage},
    {path: '**', component: NotFoundPage},
    { path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    }
];

@NgModule({
  declarations: [
    AppPage,
    HomePage,
    PessoasPage,
    NotFoundPage,
    FormularioPage
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(
        appRoutes,
        { enableTracing: false }
    ),
    HttpClientModule,
    NgbModule.forRoot(),
    FormsModule,
    ConfirmationPopoverModule.forRoot({
        confirmButtonType: 'danger'
    })
  ],
  providers: [
      {
          provide: HTTP_INTERCEPTORS,
          useClass: Interceptor,
          multi: true
      },
      PessoasService
  ],
  bootstrap: [AppPage]
})
export class AppModule { }