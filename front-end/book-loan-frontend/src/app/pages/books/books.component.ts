import { Component, OnInit } from '@angular/core';
import { IBook } from '../../_models/IBook';
import { BookService } from '../../_services/book.service';
import { IPagination } from '../../_models/IPagination';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrl: './books.component.css',
})
export class BooksComponent implements OnInit {
  constructor(private bookService: BookService, private router: Router, private fb: FormBuilder) {}

  bookForm: FormGroup = new FormGroup({});

  books: Array<IBook> = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  pagination?: IPagination;

  isCollapsed = true;

  ngOnInit(): void {
    this.findBooks();
    this.initializeForms();
  }

  initializeForms() {
    this.bookForm = this.fb.group({
      name: ['', ],
      author: ['', ],
      publisher: ['', ],
      yearOfPublication: ['', ],
      edition: ['', ],
    });
  }

  findBooks() {
    this.bookService.findBooks(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.books = response.result;
          this.pagination = response.pagination;
        }
      },
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.findBooks();
    }
  }

  updateBook(book: IBook) {
    this.router.navigate(['book/put'], { state: book })
  }

  filter() {
    var filter = this.bookForm.value;
    filter.pageNumber = this.pageNumber;
    filter.pageSize = this.pageSize;

    this.bookService.filterBook(filter).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.books = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }
}
