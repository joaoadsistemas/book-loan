import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IBook } from '../_models/IBook';
import { map } from 'rxjs';
import { PaginatedResult } from '../_models/IPagination';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  baseUrl: string = environment.apiUrl;
  paginatedResult: PaginatedResult<Array<IBook>> = new PaginatedResult();

  constructor(private http: HttpClient) {}

  findBooks(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Array<IBook>>(this.baseUrl + 'book', { observe: 'response', params })
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


  insertBook(book: IBook) {
    return this.http.post<any>(this.baseUrl + 'book', book).pipe(
      map((response) => {
        return response;
      })
    )
  }

  updateBook(book: IBook) {
    return this.http.put<any>(this.baseUrl + 'book', book).pipe(
      map((response) => {
        return response;
      })
    )
  }
}
