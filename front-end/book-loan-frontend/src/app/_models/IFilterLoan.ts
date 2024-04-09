export interface IFilterLoan {
  cpf: string;
  name: string;
  loanDateInitial: string;
  loanDateFinal: string;
  deliverDateInitial: string;
  deliverDateFinal: string;
  delivered: boolean;
  notDelivered: boolean;
  pageNumber: number;
  pageSize: number;
}
