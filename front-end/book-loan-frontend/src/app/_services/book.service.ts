import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IBook } from '../_models/IBook';
import { map } from 'rxjs';
import { PaginatedResult } from '../_models/IPagination';
import { IFilterBook } from '../_models/IFilterBook';

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


  searchBook(term: string, page: number, itemsPerPage: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('term', term);
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<any>(this.baseUrl + 'book/search', {
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

  filterBook(filterBook: IFilterBook) {
    let params = new HttpParams();
    if (filterBook.name) params = params.append('name', filterBook.name);
    if (filterBook.author) params = params.append('author', filterBook.author);
    if (filterBook.publisher) params = params.append('publisher', filterBook.publisher);
    if (filterBook.yearOfPublication) params = params.append('yearOfPublication', filterBook.yearOfPublication);
    if (filterBook.edition) params = params.append('edition', filterBook.edition);
    if (filterBook.pageNumber) params = params.append('pageNumber', filterBook.pageNumber);
    if (filterBook.pageSize) params = params.append('pageSize', filterBook.pageSize);

    console.log(params);

    return this.http.get<any>(this.baseUrl + 'book/filter', {
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

  removeBook(book: IBook) {
    return this.http.delete<any>(this.baseUrl + 'book/' + book.id).pipe(
      map((response) => {
        return response;
      })
    );
  }
}
