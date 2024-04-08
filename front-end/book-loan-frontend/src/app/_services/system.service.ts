import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { map } from 'rxjs';
import { IDashboard } from '../_models/IDashboard';

@Injectable({
  providedIn: 'root',
})
export class SystemService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  dashboard() {
    return this.http.get<any>(this.baseUrl + 'system/dashboard').pipe(
      map((response: IDashboard) => {
        return response;
      })
    );
  }
}
