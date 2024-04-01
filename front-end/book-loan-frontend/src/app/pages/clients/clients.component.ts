import { ClientService } from './../../_services/client.service';
import { Component, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';
import { Router } from '@angular/router';
import { IPagination } from '../../_models/IPagination';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css',
})
export class ClientsComponent implements OnInit{
  constructor(private clientService: ClientService, private router: Router) {}


  pageNumber: number = 1;
  pageSize: number = 10;
  pagination?: IPagination;

  clients: Array<IClient> = [  ];

  ngOnInit(): void {
    this.findClients();
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

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.findClients();
    }
  }


  updateClient(client: IClient) {
    this.router.navigate(['client/put'], { state: client });
  }
}
