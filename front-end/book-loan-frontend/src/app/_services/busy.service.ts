import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  requestCount = 0;
  constructor(private spinner: NgxSpinnerService) {}

  busy() {
    this.requestCount++;
    this.spinner.show(undefined, { type: 'ball-spin-fade' });
  }

  notBusy() {
    this.requestCount--;
    if (this.requestCount <= 0) {
      this.requestCount = 0;
      this.spinner.hide();
    }
  }
}
