import { Component, OnInit } from '@angular/core';
import { IBook } from '../../_models/IBook';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookService } from '../../_services/book.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-forms',
  templateUrl: './book-forms.component.html',
  styleUrl: './book-forms.component.css',
})
export class BookFormsComponent implements OnInit {
  book?: IBook;
  bookForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private bookService: BookService,
    private toastr: ToastrService
  ) {
    const currentNavigation = this.router.getCurrentNavigation();
    if (currentNavigation?.extras.state) {
      const state = currentNavigation.extras.state;
      if (this.isIBook(state)) {
        this.book = state;
      }
    }
  }

  ngOnInit(): void {
    this.initializeForms();
    if (this.book) {
      this.bookForm.setValue(this.book);
      let yearOfPublication = this.book.yearOfPublication
        .toString()
        .split('T');
      this.bookForm.controls['yearOfPublication'].setValue(yearOfPublication[0]);
    }
  }

  initializeForms() {
    this.bookForm = this.fb.group({
      id: [0, [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      author: ['', [Validators.required, Validators.maxLength(200)]],
      publisher: ['', [Validators.required, Validators.maxLength(50)]],
      yearOfPublication: ['', [Validators.required]],
      edition: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  insertBook() {
    if (this.bookForm.valid) {
      this.bookService.insertBook(this.bookForm.value).subscribe({
        next: (response: any) => {
          this.toastr.success('Book has been registered successfully');
          this.bookForm.reset();
        },
        error: (response: any) => {
          this.toastr.error(response.error);
          console.log(response);
        },
      });
    }
  }

  updateBook() {
    if (this.bookForm.valid) {
      this.bookService.updateBook(this.bookForm.value).subscribe({
        next: (response: any) => {
          this.toastr.success('Book has been Updated successfully');
          this.router.navigate(['/book'])
        },
        error: (response: any) => {
          this.toastr.error(response.error);
          console.log(response);
        },
      });
    }
  }

  isIBook(obj: any): obj is IBook {
    return (
      obj &&
      typeof obj.id == 'number' &&
      typeof obj.name == 'string' &&
      typeof obj.author == 'string' &&
      typeof obj.publisher == 'string' &&
      typeof obj.yearOfPublication == 'string' &&
      typeof obj.edition == 'string'
    );
  }
}
