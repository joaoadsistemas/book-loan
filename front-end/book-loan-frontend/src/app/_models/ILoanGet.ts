import { IClient } from "./IClient";

export interface ILoanGet {

  id: number;
  clientId: number;
  loanDate: string;
  deliveryDate: string;
  delivered: boolean;
  client: IClient;

}
