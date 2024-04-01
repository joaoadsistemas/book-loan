import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { IUser } from '../../_models/IUser';
import { Router } from '@angular/router';
import { UserService } from '../../_services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-forms',
  templateUrl: './user-forms.component.html',
  styleUrl: './user-forms.component.css',
})
export class UserFormsComponent implements OnInit {
  user?: IUser;
  userForms: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private userServive: UserService,
    private toastr: ToastrService
  ) {
    const currentNavigation = this.router.getCurrentNavigation();
    if (currentNavigation?.extras.state) {
      const state = currentNavigation.extras.state;
      if (this.isIUser(state)) {
        this.user = state;
      }
    }
  }

  ngOnInit(): void {
    this.initializeForm();
    if (this.user) {
      this.user.password = '';
      this.userForms.setValue(this.user);
    }
  }

  initializeForm() {
    const passwordValidators = [
      Validators.minLength(8),
      Validators.maxLength(100),
    ];

    if (!this.user) {
      passwordValidators.unshift(Validators.required);
    }

    this.userForms = this.fb.group({
      id: [0, [Validators.required, Validators.min(0)]],
      name: [
        '',
        [Validators.required, Validators.maxLength(250)],
      ],
      email: ['', [Validators.required, Validators.maxLength(200), Validators.email]],
      password: ['', passwordValidators],
      isAdmin: [false],
    });
  }


  register() {
    if (this.userForms.valid) {
      this.userServive.register(this.userForms.value).subscribe({
        next: (response: any) => {
          this.toastr.success('User created successfully');
          this.userForms.reset();
        },
        error: (response: any) => {
          this.toastr.error(response.error);
        },
      });
    }
  }

  updateUser() {
    if (this.userForms.valid) {
      this.userServive.update(this.userForms.value).subscribe({
        next: (response: any) => {
          this.toastr.success(response.message);
        },
        error: (response: any) => {
          this.toastr.error(response.error);
        },
      });
    }
  }

  isIUser(obj: any): obj is IUser {
    return (
      obj &&
      typeof obj.id == 'number' &&
      typeof obj.name == 'string' &&
      typeof obj.email == 'string' &&
      (typeof obj.password == 'string' || obj.password == null || obj.password == undefined) &&
      typeof obj.isAdmin == 'boolean'
    );
  }



}
