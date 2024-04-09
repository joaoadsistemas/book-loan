import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IClient } from '../../_models/IClient';
import { TextFormatter } from '../../_helpers/TextFormatter';

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

  cpfFormater(cpf: string | undefined) {
    if (!cpf) return undefined;
    return TextFormatter.formatCPF(cpf);
  }

  phoneFormater(phone: string | undefined) {
    if (!phone) return undefined;
    return TextFormatter.formatTelefoneCelular(phone);
  }

    fixPhoneFormater(fixPhone: string | undefined) {
    if (!fixPhone) return undefined;
    return TextFormatter.formatTelefoneFixo(fixPhone);
  }

}
