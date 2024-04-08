import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    NgxSpinnerModule
  ],
  exports: [
    ToastrModule,
    NgxSpinnerModule,
    ModalModule,
    BrowserAnimationsModule,
    PaginationModule,
    NgxSpinnerModule
  ],
})
export class SharedModule {}
