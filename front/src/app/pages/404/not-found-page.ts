import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './not-found-page.html',
    styleUrls: ['./not-found-page.css']
})

export class NotFoundPage {
    constructor() {}
    title = 'Home Page';

    iniciar() {
        alert("to ligado nessa gambis ai");
        //this.router.navigate(['pessoas']);
    }
}
