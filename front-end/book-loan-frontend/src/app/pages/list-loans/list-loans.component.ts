import { Component, OnInit } from '@angular/core';
import { ILoanGet } from '../../_models/ILoanGet';
import { LoanService } from '../../_services/loan.service';
import { IPagination } from '../../_models/IPagination';
import { Router } from '@angular/router';
import { TextFormatter } from '../../_helpers/TextFormatter';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-list-loans',
  templateUrl: './list-loans.component.html',
  styleUrl: './list-loans.component.css'
})
export class ListLoansComponent implements OnInit{

  loans: Array<ILoanGet> = [];
  pagination?: IPagination;
  page: number = 1;
  itemsPerPage: number = 10;
  isCollapsed = true;

  loanForms: FormGroup = new FormGroup({});

  constructor(private loanService: LoanService, private router: Router, private fb: FormBuilder){}

  ngOnInit(): void {
    this.getLoans();
    this.initializeForm();
  }


  getLoans() {
    this.loanService.getLoans(this.page, this.itemsPerPage).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.loans = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  initializeForm() {
    this.loanForms = this.fb.group({
      cpf: ['', [Validators.minLength(11), Validators.maxLength(11)]],
      name: [''],
      loanDateInitial: [''],
      loanDateFinal: [''],
      deliverDateInitial: [''],
      deliverDateFinal: [''],
      delivered: [null],
      notDelivered: [null],
    })
  }

  filter() {
    var filter = this.loanForms.value;
    filter.pageNumber = this.page;
    filter.pageSize = this.itemsPerPage;

    this.loanService.filterLoan(filter).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.loans = response.result;
          this.pagination = response.pagination;
        }
      }
    })
  }

  updateLoan(loan: ILoanGet) {
    this.router.navigate(['loan'], {state: loan});
  }


  pageChanged(event: any) {
    if (this.page !== event.page) {
      this.page = event.page;
      this.getLoans();
    }
  }

  dateFormatter(date: string) {

      return TextFormatter.formatDate(date);

  }

  cpfFormater(cpf: string) {
    if (!cpf) return undefined;
    return TextFormatter.formatCPF(cpf);
  }
}
