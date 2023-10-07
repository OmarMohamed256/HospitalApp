import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, of, take } from 'rxjs';
import { AccountService } from '../services/account.service';
import { User } from 'src/app/models/UserModels/user';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User | any;
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user );

    if(currentUser == null){
      const user = localStorage.getItem('user');
      if (user) {
        currentUser = JSON.parse(user);
      }
    }
    
    if(currentUser !== null &&  currentUser !== undefined){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }
    return next.handle(request);
  }
}
