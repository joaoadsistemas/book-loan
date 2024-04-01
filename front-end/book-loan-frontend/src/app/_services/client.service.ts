import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { PaginatedResult } from '../_models/IPagination';
import { IClient } from '../_models/IClient';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ClientService {
  baseUrl: string = environment.apiUrl;
  paginatedResult: PaginatedResult<Array<IClient>> = new PaginatedResult();

  constructor(private http: HttpClient) {}

  findClients(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Array<IClient>>(this.baseUrl + 'client', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
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

  insertClient(client: IClient) {
    return this.http.post<any>(this.baseUrl + 'client', client).pipe(
      map((response) => {
        return response;
      })
    );
  }

  updateClient(client: IClient) {
    return this.http.put<any>(this.baseUrl + 'client', client).pipe(
      map((response) => {
        return response;
      })
    );
  }
}
