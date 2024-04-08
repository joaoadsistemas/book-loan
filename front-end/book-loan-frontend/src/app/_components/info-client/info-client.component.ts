import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IClient } from '../../_models/IClient';

@Component({
  selector: 'app-info-client',
  templateUrl: './info-client.component.html',
  styleUrl: './info-client.component.css',
})
export class InfoClientComponent {
  @Input() client?: IClient;
  @Input() showThrash: boolean = false;
  @Input() showAdd: boolean = false;

  @Output() clickButton = new EventEmitter<void>();


  addOrRemove() {
    this.clickButton.emit();
  }

}
