import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';
import { ClientService } from '../../_services/client.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-client-forms',
  templateUrl: './client-forms.component.html',
  styleUrl: './client-forms.component.css',
})
export class ClientFormsComponent implements OnInit {
  client?: IClient;
  clientForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private clientService: ClientService,
    private toastr: ToastrService
  ) {
    const currentNavigation = this.router.getCurrentNavigation();
    if (currentNavigation?.extras.state) {
      const state = currentNavigation.extras.state;
      if (this.isIClient(state)) {
        this.client = state as IClient;
      }
    }
  }

  ngOnInit(): void {
    this.initializeForm();
    if (this.client) {
      this.clientForm.setValue(this.client);
    }
  }

  initializeForm() {
    this.clientForm = this.fb.group({
      id: [0, [Validators.required, Validators.min(0)]],
      cpf: [
        '',
        [
          Validators.required,
          Validators.minLength(11),
          Validators.maxLength(11),
        ],
      ],
      name: ['', [Validators.required, Validators.maxLength(200)]],
      address: ['', [Validators.required, Validators.maxLength(50)]],
      city: ['', [Validators.required, Validators.maxLength(50)]],
      neighborhood: ['', [Validators.required, Validators.maxLength(50)]],
      number: ['', [Validators.required, Validators.maxLength(50)]],
      phoneNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      fixPhoneNumber: ['', [Validators.required,Validators.minLength(10) , Validators.maxLength(10)]],
    });
  }

  insertClient() {
    this.clientService.insertClient(this.clientForm.value).subscribe({
      next: (response: any) => {
        this.toastr.success('Client has been registered successfully')
        this.clientForm.reset()
      },
    });
  }

  updateClient() {
    this.clientService.updateClient(this.clientForm.value).subscribe({
      next: (response: any) => {
        this.toastr.success('Client has been updated successfully')
        this.router.navigate(['/client'])
      },
    });
  }

  removeClient(client: IClient) {
    Swal.fire({
      title: 'Exclude client',
      text: 'Do you really want to delete this client?',
      icon: 'warning',
      showCancelButton: true,
      cancelButtonText: 'Cancel',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        if (this.client) {
          this.clientService.removeClient(this.client).subscribe({
            next: (response: any) => {
              this.toastr.success('Client has been deleted successfully')
              this.router.navigate(['/client'])
            },
          });
        }
      }

    });
  }


  isIClient(obj: any): obj is IClient {
    return (
      obj &&
      typeof obj.id === 'number' &&
      typeof obj.cpf === 'string' &&
      typeof obj.name === 'string' &&
      typeof obj.address === 'string' &&
      typeof obj.city === 'string' &&
      typeof obj.neighborhood === 'string' &&
      typeof obj.number === 'string' &&
      typeof obj.phoneNumber === 'string' &&
      typeof obj.fixPhoneNumber === 'string'
    );
  }
}
