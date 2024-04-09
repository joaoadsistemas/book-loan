import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './_modules/shared.module';
import { HomeComponent } from './pages/home/home.component';
import { NavbarComponent } from './_components/navbar/navbar.component';
import { FooterComponent } from './_components/footer/footer.component';
import { BaseUiComponent } from './_components/base-ui/base-ui.component';
import { ClientsComponent } from './pages/clients/clients.component';
import { ClientFormsComponent } from './pages/client-forms/client-forms.component';
import { BooksComponent } from './pages/books/books.component';
import { BookFormsComponent } from './pages/book-forms/book-forms.component';
import { LoansComponent } from './pages/loans/loans.component';
import { InfoBookComponent } from './_components/info-book/info-book.component';
import { ClientDetailsComponent } from './_modals/client-details/client-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InfoClientComponent } from './_components/info-client/info-client.component';
import { BookDetailsComponent } from './_modals/book-details/book-details.component';
import { LoginComponent } from './pages/login/login.component';
import { UsersComponent } from './pages/users/users.component';
import { UserFormsComponent } from './pages/user-forms/user-forms.component';
import {
  HTTP_INTERCEPTORS,
  HttpClient,
  HttpClientModule,
} from '@angular/common/http';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { AuthorizationMessageComponent } from './pages/authorization-message/authorization-message.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { ListLoansComponent } from './pages/list-loans/list-loans.component';
import { provideNgxMask } from 'ngx-mask';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    BaseUiComponent,
    ClientsComponent,
    ClientFormsComponent,
    BooksComponent,
    BookFormsComponent,
    LoansComponent,
    InfoBookComponent,
    ClientDetailsComponent,
    InfoClientComponent,
    BookDetailsComponent,
    LoginComponent,
    UsersComponent,
    UserFormsComponent,
    AuthorizationMessageComponent,
    ListLoansComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    provideNgxMask(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
