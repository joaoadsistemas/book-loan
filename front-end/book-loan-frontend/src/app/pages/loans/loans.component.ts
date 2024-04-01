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
  books: Array<IBook> = [
    {
      id: 1,
      author: 'Stephen King',
      edition: '1',
      name: 'It',
      publisher: 'Suma',
      yearOfPublication: new Date(2014, 5, 1),
    },
    {
      id: 1,
      author: 'Stephen King',
      edition: '1',
      name: 'It',
      publisher: 'Suma',
      yearOfPublication: new Date(2014, 5, 1),
    },
    {
      id: 1,
      author: 'Stephen King',
      edition: '1',
      name: 'It',
      publisher: 'Suma',
      yearOfPublication: new Date(2014, 5, 1),
    },
  ];

  clients: Array<IClient> = [
    {
      name: 'Joao',
      address: 'Rua Achiles Audi',
      city: 'Cerquilho',
      cpf: '51409677826',
      fixPhoneNumber: '33856787',
      id: 1,
      neighborhood: 'Centro',
      number: '1043',
      phoneNumber: '15991564371',
    },
  ];

  bsModalRef?: BsModalRef;
  clientInfo: string = '';
  bookInfo: string = '';

  constructor(private modalService: BsModalService) {}

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
  }
}
