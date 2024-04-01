import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { Route, Router } from '@angular/router';
import { IUser } from '../../_models/IUser';
import { IUserToken } from '../../_models/IUserToken';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit {
  constructor(private userService: UserService, private router: Router) {}

  user?: IUserToken;

  ngOnInit(): void {
    this.findUser();
  }

  findUser() {
    this.userService.userLoggedToken$.subscribe({
      next: (user) => {
        if (user) {
          this.user = user;
        }
      },
    });
  }

  updateUser() {
    this.userService.findUserById().subscribe({
      next: (user: IUser) => {
        console.log(user)
        this.router.navigate(['user/put'], {state: user})
      }
    })
  }


  logout() {
    this.userService.logout();
  }
}
