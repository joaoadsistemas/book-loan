import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoanBookService {

  baseUrl:string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBooksLoan(idLoan: number) {
    return this.http.get(this.baseUrl + 'loanbook/loan/' + idLoan).pipe(
      map ((response: any) => {
        return response;
      })
    );
  }
}
