import { Component, OnInit } from '@angular/core';
import { IUser } from '../../_models/IUser';
import { Router } from '@angular/router';
import { IPagination } from '../../_models/IPagination';
import { UserService } from '../../_services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  users: Array<IUser> = [];
  pagination: IPagination | undefined;
  pageNumber: number = 1;
  pageSize: number = 10;

  userForms: FormGroup = new FormGroup({});

  isCollapsed = true;

  constructor(private router: Router, private userService: UserService, private fb: FormBuilder) {}
  ngOnInit(): void {
    this.findUsers();
    this.initializeForm();
  }


  initializeForm() {
    this.userForms = this.fb.group({
      name: ['',],
      email: [
        '',
      ],
      isAdmin: [false],
      isNotAdmin: [false],
      active: [false],
      inactive: [false],
    });


  }


  findUsers() {
    this.userService.findUsers(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.users = response.result;
          this.pagination = response.pagination;
        }
      },
    });
  }

  filter() {
    var filter = this.userForms.value;
    filter.pageNumber = this.pageNumber;
    filter.pageSize = this.pageSize;

    this.userService.filterUser(filter).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.users = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  updateUser(user: IUser) {
    console.log(user);
    this.router.navigate(['user/put'], { state: user });
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.findUsers();
    }
  }
}
