import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './home.html',
    styleUrls: ['./home.css']
})

export class HomePage {
    constructor() {}
    title = 'Home Page';

    iniciar() {
        alert("to ligado nessa gambis ai");
        //this.router.navigate(['pessoas']);
    }
}
