import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, OnInit } from '@angular/core';
import { IClient } from '../../_models/IClient';

@Component({
  selector: 'app-client-details',
  templateUrl: './client-details.component.html',
  styleUrl: './client-details.component.css',
})
export class ClientDetailsComponent implements OnInit {
  clientInfo: string = '';
  clients: Array<IClient> = [
    {
      name: 'Joao',
      address: 'Rua Achiles Audi',
      city: 'Cerquilho',
      cpf: '51409677826',
      fixPhoneNumber: '33856787',
      id: 1,
      neighborhood: 'Centro',
      number: '1043',
      phoneNumber: '15991564371',
    },
    {
      name: 'Joao',
      address: 'Rua Achiles Audi',
      city: 'Cerquilho',
      cpf: '51409677826',
      fixPhoneNumber: '33856787',
      id: 1,
      neighborhood: 'Centro',
      number: '1043',
      phoneNumber: '15991564371',
    },
  ];

  ngOnInit(): void {
    console.log(this.clientInfo);
  }

  constructor(private bsModalRef: BsModalRef) {}

  closeModal() {
    this.bsModalRef.hide();
  }
}
