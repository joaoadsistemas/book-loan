import { map } from 'rxjs';
import { IClient } from './../../_models/IClient';
import { ClientService } from './../../_services/client.service';
import { Component, OnInit } from '@angular/core';
import { IBook } from '../../_models/IBook';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal'; // Import ModalOptions
import { ClientDetailsComponent } from '../../_modals/client-details/client-details.component';
import { BookDetailsComponent } from '../../_modals/book-details/book-details.component';
import { ToastrService } from 'ngx-toastr';
import { ILoan } from '../../_models/ILoan';
import { LoanService } from '../../_services/loan.service';
import { ILoanGet } from '../../_models/ILoanGet';
import { Router } from '@angular/router';
import { LoanBookService } from '../../_services/loan-book.service';
import { IUpdateLoan } from '../../_models/IUpdateLoan';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IPagination } from '../../_models/IPagination';

@Component({
  selector: 'app-loans',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css'],
})
export class LoansComponent implements OnInit{

  books: Array<IBook> = [];

  loan?: ILoanGet;

  pageNumber: number = 1;
  pageSize: number = 10;
  pagination?: IPagination;

  loans: Array<ILoanGet> = [  ];

  client?: IClient;

  bsModalRef?: BsModalRef;
  clientInfo: string = '';
  bookInfo: string = '';
  deliveryDate: string = '';
  delivered: boolean = false;



  constructor(
    private modalService: BsModalService,
    private clientService: ClientService,
    private toastrService: ToastrService,
    private loanService: LoanService,
    private router: Router,
    private loanBookService: LoanBookService,

  ) {
    const currentNavigation = this.router.getCurrentNavigation();
    if (currentNavigation?.extras.state) {
      const state = currentNavigation.extras.state;
      console.log(state);
      console.log(this.isILoanGet(state));
      if (this.isILoanGet(state)) {
        this.loan = state;
      }
    }
  }
  ngOnInit(): void {
    if (this.loan) {

      this.client = this.loan.client;

      let deliveryDateFormated = this.loan.deliveryDate
        .toString()
        .split('T')[0];

      this.deliveryDate = deliveryDateFormated;
      this.delivered = this.loan.delivered;

      this.loanBookService.getBooksLoan(this.loan.id).subscribe({
        next: (response: any) => {
          this.books = response.map((x: any) => x.book);
        }
      })

    }

  }





  openClientDetails() {

    if(!this.clientInfo) {
      return;
    }

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

    this.clientInfo = '';
  }



  removeClient() {
    this.client = undefined;
  }

  openBookDetails() {
    if(!this.bookInfo) {
      return;
    }
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
      var existBook = this.books.find((book) => book.id === result.id);
      if (existBook) {
        this.toastrService.error('This book is already added');
        return;
      }
      this.books.push(result);
    });

    this.bookInfo = '';

  }

  removeBook(book: IBook) {
    this.books = this.books.filter((b) => b.id !== book.id);
  }

  insertLoan() {
    var loan: ILoan = {
      clientId : this.client!.id,
      idsBooks: this.books.map((book) => book.id),
      deliveryDate: this.deliveryDate,
    }

    this.loanService.insertLoan(loan).subscribe(
      {
        next: (response) => {
          this.toastrService.success(response.message);
          this.books = [];
          this.client = undefined;
          this.deliveryDate = '';
        }
      }
    )

  }

  updateLoan() {
    var loanPut: IUpdateLoan = {
      id: this.loan!.id,
      clientId : this.client!.id,
      booksIds: this.books.map((book) => book.id),
      deliveryDate: this.deliveryDate,
      delivered: this.delivered,

    }
    this.loanService.updateLoan(loanPut).subscribe({
      next: (response) => {
        console.log(response)
        this.toastrService.success('Loan updated successfully');
        this.router.navigate(['/loans']);
      }
    })
  }




  isILoanGet(obj: any): obj is ILoanGet {
    return (
      obj &&
      typeof obj.id == 'number' &&
      obj.client &&
      typeof obj.client.id == 'number' &&
      typeof obj.loanDate == 'string' &&
      typeof obj.deliveryDate == 'string' &&
      typeof obj.delivered == 'boolean' &&
      typeof obj.client === 'object' &&
      obj.client !== null &&
      'id' in obj.client &&
      'cpf' in obj.client &&
      'name' in obj.client &&
      'address' in obj.client &&
      'city' in obj.client &&
      'neighborhood' in obj.client &&
      'number' in obj.client &&
      'phoneNumber' in obj.client &&
      'fixPhoneNumber' in obj.client
    );
  }



}
