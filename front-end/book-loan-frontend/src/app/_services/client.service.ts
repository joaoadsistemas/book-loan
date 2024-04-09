import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { PaginatedResult } from '../_models/IPagination';
import { IClient } from '../_models/IClient';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
import { IFilterClient } from '../_models/IFilterClient';

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
          return this.paginatedResult;
        })
      );
  }

  searchClient(term: string, page: number, itemsPerPage: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('term', term);
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<any>(this.baseUrl + 'client/search', {
      observe: 'response',
      params,
    }).pipe(
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

  filterClient(filterClient: IFilterClient) {
    let params = new HttpParams();
    if (filterClient.cpf) params = params.append('cpf', filterClient.cpf);
    if (filterClient.name) params = params.append('name', filterClient.name);
    if (filterClient.city) params = params.append('city', filterClient.city);
    if (filterClient.neighborhood) params = params.append('neighborhood', filterClient.neighborhood);
    if (filterClient.fixPhoneNumber) params = params.append('fixPhoneNumber', filterClient.fixPhoneNumber);
    if (filterClient.phoneNumber) params = params.append('phoneNumber', filterClient.phoneNumber);
    if (filterClient.pageNumber) params = params.append('pageNumber', filterClient.pageNumber);
    if (filterClient.pageSize) params = params.append('pageSize', filterClient.pageSize);

    console.log(params);

    return this.http.get<any>(this.baseUrl + 'client/filter', {
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


  findClientById(id: number) {
    return this.http.get<any>(this.baseUrl + 'client/' + id).pipe(
      map((response) => {
        return response;
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

  removeClient(client: IClient) {
    return this.http.delete<any>(this.baseUrl + 'client' + client.id).pipe(
      map((response) => {
        return response;
      })
    );
  }


}
