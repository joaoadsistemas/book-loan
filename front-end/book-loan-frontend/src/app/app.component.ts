import { Component, OnInit } from '@angular/core';
import { IUserToken } from './_models/IUserToken';
import { UserService } from './_services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  constructor(private userService: UserService) {}

  setUserLogged() {
    const userString = localStorage.getItem('user');
    if (!userString) {
      return;
    }

    const userToken: IUserToken = JSON.parse(userString);
    this.userService.setUserLogged(userToken);
  }

  ngOnInit(): void {
    this.setUserLogged();
  }
  title = 'book-loan-frontend';
}
