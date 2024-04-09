import { Component, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IBook } from '../../_models/IBook';
import { BookService } from '../../_services/book.service';
import { IPagination } from '../../_models/IPagination';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit {
  bookInfo: string = '';
  page: number = 1;
  itemsPerPage: number = 10;
  pagination?: IPagination;
  onClose: EventEmitter<any> = new EventEmitter();

  books: Array<IBook> = [
  ];

  constructor(private bsModalRef: BsModalRef, private bookService: BookService) {}

  ngOnInit(): void {
    this.searchBook();
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  searchBook(){
    return this.bookService.searchBook(this.bookInfo, this.page, this.itemsPerPage).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.books = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  addBook(book: IBook) {
    this.onClose.emit(book);
    this.closeModal();
  }

  pageChanged(event: any): void {
    this.page = event.page;
    this.searchBook();
  }
}
