import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';
import { CollapseModule } from 'ngx-bootstrap/collapse';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    NgxSpinnerModule,
    NgxMaskDirective,
    NgxMaskPipe,
    CollapseModule.forRoot(),
  ],
  exports: [
    ToastrModule,
    NgxSpinnerModule,
    ModalModule,
    BrowserAnimationsModule,
    PaginationModule,
    NgxSpinnerModule,
    NgxMaskDirective,
    NgxMaskPipe,
    CollapseModule,
  ],
})
export class SharedModule {}
