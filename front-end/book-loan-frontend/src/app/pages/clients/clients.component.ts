import { ClientService } from './../../_services/client.service';
import { Component, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';
import { Router } from '@angular/router';
import { IPagination } from '../../_models/IPagination';
import { TextFormatter } from '../../_helpers/TextFormatter';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css',
})
export class ClientsComponent implements OnInit{
  constructor(private clientService: ClientService, private router: Router, private fb: FormBuilder) {}


  pageNumber: number = 1;
  pageSize: number = 10;
  pagination?: IPagination;

  clients: Array<IClient> = [  ];
  clientForm: FormGroup = new FormGroup({});

  isCollapsed = true;

  ngOnInit(): void {
    this.findClients();
    this.initializeForm();
  }

  initializeForm() {
    this.clientForm = this.fb.group({
      cpf: [
        '',

      ],
      name: ['',],
      city: ['',],
      neighborhood: ['', ],
      phoneNumber: ['', ],
      fixPhoneNumber: ['',],
    });
  }

  findClients() {
    this.clientService.findClients(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.clients = response.result;
          this.pagination = response.pagination;
        }
      }
    })

  }

  filter() {
    var filter = this.clientForm.value;
    filter.pageNumber = this.pageNumber;
    filter.pageSize = this.pageSize;

    this.clientService.filterClient(filter).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.clients = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.findClients();
    }
  }



  updateClient(client: IClient) {
    this.router.navigate(['client/put'], { state: client });
  }

  cpfFormater(cpf: string | undefined) {
    if (!cpf) return undefined;
    return TextFormatter.formatCPF(cpf);
  }

  phoneFormater(phone: string | undefined) {
    if (!phone) return undefined;
    return TextFormatter.formatTelefoneCelular(phone);
  }

}
