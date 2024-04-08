import { ClientService } from './../../_services/client.service';
import { Component } from '@angular/core';
import { IBook } from '../../_models/IBook';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal'; // Import ModalOptions
import { ClientDetailsComponent } from '../../_modals/client-details/client-details.component';
import { IClient } from '../../_models/IClient';
import { BookDetailsComponent } from '../../_modals/book-details/book-details.component';

@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css'],
})
export class LoansComponent {
  books: Array<IBook> = [];

  client?: IClient;

  bsModalRef?: BsModalRef;
  clientInfo: string = '';
  bookInfo: string = '';

  constructor(
    private modalService: BsModalService,
    private clientService: ClientService
  ) {}

  openClientDetails() {
    const modalOptions: ModalOptions<ClientDetailsComponent> = {
      initialState: {
        clientInfo: this.clientInfo,
      },
    };

    this.bsModalRef = this.modalService.show(
      ClientDetailsComponent,
      modalOptions
    );

    this.bsModalRef?.content.onClose.subscribe((result: IClient) => {
      this.client = result
    });
  }

  removeClient() {
    this.client = undefined;
  }

  openBookDetails() {
    const modalOptions: ModalOptions<BookDetailsComponent> = {
      initialState: {
        bookInfo: this.bookInfo,
      },
    };

    this.bsModalRef = this.modalService.show(
      BookDetailsComponent,
      modalOptions
    );

    this.bsModalRef.content.onClose.subscribe((result: IBook) => {
      this.books.push(result);
    });
  }
}
