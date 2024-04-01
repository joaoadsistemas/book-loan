import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUser } from '../_models/IUser';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ILogin } from '../_models/ILogin';
import { IUserToken } from '../_models/IUserToken';
import { PaginatedResult } from '../_models/IPagination';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl: string = environment.apiUrl;

  private userLogged = new BehaviorSubject<IUserToken | null>(null);
  userLoggedToken$ = this.userLogged.asObservable();

  paginatedResult: PaginatedResult<Array<IUser>> = new PaginatedResult();

  constructor(private http: HttpClient) {}

  setUserLogged(userToken: IUserToken) {
    this.userLogged.next(userToken);
  }

  logout() {
    this.userLogged.next(null);
    localStorage.clear();
  }

  isAdmin() {
    const userStorage = localStorage.getItem('user');

    if (userStorage) {
      const userToken: IUserToken = JSON.parse(userStorage);
      return userToken.isAdmin;
    }

    return false;
  }

  findUsers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Array<IUser>>(this.baseUrl + 'user', { observe: 'response', params })
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

  findUserById(id?: number) {
    return this.http.get<any>(this.baseUrl + 'user/' + (id ? id : '0')).pipe(
      map((response: IUser) => {
        return response;
      })
    );
  }

  login(loginBody: ILogin) {
    return this.http.post<any>(this.baseUrl + 'user/login', loginBody).pipe(
      map((response: IUserToken) => {
        if (response) {
          localStorage.setItem('user', JSON.stringify(response));
          this.setUserLogged(response);
        }
        return response;
      })
    );
  }

  register(user: IUser) {
    return this.http.post<any>(this.baseUrl + 'user/register', user).pipe(
      map((response) => {
        return response;
      })
    );
  }

  update(user: IUser) {
    if (!user.password || user.password.length == 0) {
      user.password = null;
    }
    return this.http.put<any>(this.baseUrl + 'user', user).pipe(
      map((response) => {
        return response;
      })
    );
  }
}
