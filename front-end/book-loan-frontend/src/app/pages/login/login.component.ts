import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { ILogin } from '../../_models/ILogin';
import { ToastrService } from 'ngx-toastr';
import { IUserToken } from '../../_models/IUserToken';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForms: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForms();
  }

  initializeForms() {
    this.loginForms = this.fb.group({
      Email: [
        '',
        [Validators.required, Validators.maxLength(200), Validators.email],
      ],
      Password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(100),
        ],
      ],
    });
  }

  login(loginBody: ILogin) {
    this.userService.login(loginBody).subscribe({
      next: (response: IUserToken) => {
        this.toastr.success('Login successfully');
        this.router.navigate(['/']);
      },
    });
  }
}
