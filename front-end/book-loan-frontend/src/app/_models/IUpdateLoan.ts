export interface IUpdateLoan {
  id: number;
  clientId: number;
  booksIds: number[];
  deliveryDate: string;
  delivered: boolean;
}
