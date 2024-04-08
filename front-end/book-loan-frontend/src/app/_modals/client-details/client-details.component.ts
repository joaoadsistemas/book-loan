import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css',
})
export class ClientDetailsComponent implements OnInit {
  clientInfo: string = '';
  onClose: EventEmitter<any> = new EventEmitter();
  clients: Array<IClient> = [
    {
      id: 1,
      cpf: '51409776543',
      name: 'Luan',
      address: 'Rua Limeira',
      city: 'Santos',
      neighborhood: 'Centro',
      number: '843',
      phoneNumber: 'string',
      fixPhoneNumber: 'string',
    },
    {
      id: 2,
      cpf: '51489876652',
      name: 'Marcia',
      address: 'Rua Achilhes Audfi',
      city: 'Cerquilho',
      neighborhood: 'Centro',
      number: '1054',
      phoneNumber: '11111111111',
      fixPhoneNumber: '22222222',
    },
  ];

  ngOnInit(): void {
    console.log(this.clientInfo);
  }

  constructor(private bsModalRef: BsModalRef) {}

  closeModal() {
    this.bsModalRef.hide();
  }

  addClient(client: IClient) {
    this.onClose.emit(client);
    this.closeModal();
  }
}
