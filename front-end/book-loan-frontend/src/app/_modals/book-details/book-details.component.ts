import { Component, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IBook } from '../../_models/IBook';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent {
  bookInfo: string = '';
  onClose: EventEmitter<any> = new EventEmitter();

  books: Array<IBook> = [
    {
      id: 5,
      name: 'It',
      author: 'Stephen King',
      publisher: 'Suma',
      yearOfPublication: '2014-11-02T00:00:00',
      edition: '1',
    },
    {
      id: 6,
      name: '1984',
      author: 'Geroge Orwell',
      publisher: 'DarkSide',
      yearOfPublication: '2024-03-31T00:00:00',
      edition: '1',
    },
    {
      id: 7,
      name: 'Iluminado',
      author: 'Stephen King',
      publisher: 'Suma',
      yearOfPublication: '2024-03-31T00:00:00',
      edition: '1',
    },
  ];

  constructor(private bsModalRef: BsModalRef) {}

  closeModal() {
    this.bsModalRef.hide();
  }

  addBook(book: IBook) {
    this.onClose.emit(book);
    this.closeModal();
  }
}
