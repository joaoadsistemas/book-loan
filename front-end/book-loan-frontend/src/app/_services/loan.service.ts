import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ILoan } from '../_models/ILoan';
import { map } from 'rxjs';
import { ILoanGet } from '../_models/ILoanGet';
import { PaginatedResult } from '../_models/IPagination';
import { IUpdateLoan } from '../_models/IUpdateLoan';
import { IFilterLoan } from '../_models/IFilterLoan';

@Injectable({
  providedIn: 'root'
})
export class LoanService {

  baseUrl: string = environment.apiUrl;
  paginatedResult: PaginatedResult<Array<ILoanGet>> = new PaginatedResult();


  constructor(private http: HttpClient) { }

  getLoans(page: number, itemsPerPage: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }


    return this.http.get<Array<ILoanGet>>(this.baseUrl + 'loan', {observe: 'response', params}).pipe(
      map((response) => {
        if (response.body) {
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          this.paginatedResult.pagination = JSON.parse(pagination);
        }

        return this.paginatedResult;
      }
    ));

  }

  filterLoan(filterClient: IFilterLoan) {
    let params = new HttpParams();
    if (filterClient.cpf) params = params.append('cpf', filterClient.cpf);
    if (filterClient.name) params = params.append('name', filterClient.name);
    if (filterClient.deliverDateInitial) params = params.append('deliverDateInitial', filterClient.deliverDateInitial);
    if (filterClient.deliverDateFinal) params = params.append('deliverDateFinal', filterClient.deliverDateFinal);
    if (filterClient.loanDateInitial) params = params.append('loanDateInitial', filterClient.loanDateInitial);
    if (filterClient.loanDateFinal) params = params.append('loanDateFinal', filterClient.loanDateFinal);
    if (filterClient.delivered) params = params.append('delivered', filterClient.delivered);
    if (filterClient.notDelivered) params = params.append('notDelivered', filterClient.notDelivered);
    if (filterClient.pageNumber) params = params.append('pageNumber', filterClient.pageNumber);
    if (filterClient.pageSize) params = params.append('pageSize', filterClient.pageSize);

    console.log(params);

    return this.http.get<any>(this.baseUrl + 'loan/filter', {
      observe: 'response',
      params,
    }).pipe(
      map((response) => {
        console.log(response);
        if (response.body) {
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          this.paginatedResult.pagination = JSON.parse(pagination);
        }
        console.log(this.paginatedResult);
        return this.paginatedResult;
      })
    );
  }



  insertLoan(loan: ILoan) {
    return this.http.post(this.baseUrl + 'loan', loan).pipe(
      map((response: any) => {
        return response;
      }
    ));
  }

  updateLoan(loan: IUpdateLoan) {
    return this.http.put(this.baseUrl + 'loan', loan).pipe(
      map((response: any) => {
        return response;
      }
    ));
  }



}
