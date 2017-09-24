import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/observable';

@Injectable()
export class Interceptor implements HttpInterceptor {
    intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authReq = req.clone({
            headers: req.headers.set('poweredBy', 'Chris Gomes'),
        });

        authReq = authReq.clone({
            headers: req.headers.set('Content-type', 'application/json'),
        });

        return next.handle(authReq);
    }
}
