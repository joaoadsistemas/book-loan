import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IBook } from '../../_models/IBook';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit {
  bookInfo: string = '';

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

  constructor(private bsModalRef: BsModalRef) {}

  closeModal() {
    this.bsModalRef.hide();
  }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}
