import { Component, Input } from '@angular/core';
import { IBook } from '../../_models/IBook';

@Component({
  selector: 'app-info-book',
  templateUrl: './info-book.component.html',
  styleUrl: './info-book.component.css',
})
export class InfoBookComponent {
  @Input() book?: IBook;
  @Input() showThrash: boolean = false;
  @Input() showAdd: boolean = false;
}
