import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';
import { ClientService } from '../../_services/client.service';
import cli from '@angular/cli';
import { IPagination, PaginatedResult } from '../../_models/IPagination';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css',
})
export class ClientDetailsComponent implements OnInit {
  clientInfo: string = '';
  page: number = 1;
  itemsPerPage: number = 10;
  pagination?: IPagination;
  onClose: EventEmitter<any> = new EventEmitter();
  clients: Array<IClient> = [
  ];

  ngOnInit(): void {
    this.searchClients();
  }

  constructor(private bsModalRef: BsModalRef, private clientService: ClientService) {}

  closeModal() {
    this.bsModalRef.hide();
  }

  searchClients(){
    return this.clientService.searchClient(this.clientInfo, this.page, this.itemsPerPage).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.clients = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  addClient(client: IClient) {
    this.onClose.emit(client);
    this.closeModal();
  }


  pageChanged(event: any): void {
    this.page = event.page;
    this.searchClients();
  }


}
