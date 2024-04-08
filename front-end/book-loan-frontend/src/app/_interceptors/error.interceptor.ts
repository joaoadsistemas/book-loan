import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService, private router: Router) {}
  intercept(
    req: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err) {
          switch (err.status) {
            case 400:
              if (err.error.errors) {
                const modalStateErrors = [];
                for (const key in err.error.errors) {
                  if (err.error.errors[key]) {
                    modalStateErrors.push(err.error.errors[key]);
                    this.toastr.error(err.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              } else if (typeof err.error === 'object') {
                this.toastr.error(err.statusText, err.status);
              } else {
                this.toastr.error(
                  err.error === null ? 'Bad Request' : err.error,
                  err.status
                );
              }
              break;
            case 401:
              this.toastr.error(
                err.error === null ? 'Unauthorized' : err.error,
                err.status
              );
              break;
            case 404:
              this.router.navigateByUrl('/');
              break;
            case 500:
              this.toastr.error('An internal error ocurred');
              break;
            default:
              this.toastr.error('An unexpected error occurred');
              break;
          }
        }

        return throwError(err);
      })
    );
  }
}
