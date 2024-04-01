import { HomeComponent } from './pages/home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientsComponent } from './pages/clients/clients.component';
import { ClientFormsComponent } from './pages/client-forms/client-forms.component';
import { BooksComponent } from './pages/books/books.component';
import { BookFormsComponent } from './pages/book-forms/book-forms.component';
import { LoansComponent } from './pages/loans/loans.component';
import { LoginComponent } from './pages/login/login.component';
import { UsersComponent } from './pages/users/users.component';
import { UserFormsComponent } from './pages/user-forms/user-forms.component';
import { authGuard } from './_guards/auth.guard';
import { adminGuard } from './_guards/admin.guard';
import { AuthorizationMessageComponent } from './pages/authorization-message/authorization-message.component';
import { verifyLoginGuard } from './_guards/verify-login.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [authGuard] },
  { path: 'client', component: ClientsComponent, canActivate: [authGuard] },
  {
    path: 'client/create',
    component: ClientFormsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'client/put',
    component: ClientFormsComponent,
    canActivate: [authGuard],
  },
  { path: 'book', component: BooksComponent, canActivate: [authGuard] },
  {
    path: 'book/create',
    component: BookFormsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'book/put',
    component: BookFormsComponent,
    canActivate: [authGuard],
  },
  { path: 'loan', component: LoansComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent, canActivate: [verifyLoginGuard] },
  {
    path: 'user',
    component: UsersComponent,
    canActivate: [authGuard, adminGuard],
  },
  {
    path: 'user/create',
    component: UserFormsComponent,
    canActivate: [authGuard, adminGuard],
  },
  {
    path: 'user/put',
    component: UserFormsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'noauthorization',
    component: AuthorizationMessageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
