import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { UserService } from '../_services/user.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private userService: UserService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    this.userService.userLoggedToken$.pipe(take(1)).subscribe({
      next: (userLogged) => {
        if (userLogged) {
          req = req.clone({
            setHeaders: {
              Authorization: `Bearer ${userLogged.token}`,
            },
          });
        }
      },
    });
    return next.handle(req);
  }
}
