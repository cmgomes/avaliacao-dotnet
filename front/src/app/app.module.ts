import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AppPage } from './pages/App/app';
import { HomePage } from './pages/home/home';
import { PessoasPage } from './pages/pessoas/pessoas';
import { NotFoundPage } from './pages/404/not-found-page';

const appRoutes: Routes = [
    {path: 'home', component: HomePage},
    {path: 'pessoas', component: PessoasPage},
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
    NotFoundPage
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(
        appRoutes,
        { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [],
  bootstrap: [AppPage]
})
export class AppModule { }
