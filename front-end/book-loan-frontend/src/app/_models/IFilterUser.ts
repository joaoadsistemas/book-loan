export interface IFilterUser {
  name: string;
  email: string;
  isAdmin: boolean;
  isNotAdmin: boolean;
  active: boolean;
  inactive: boolean;
  pageNumber: number;
  pageSize: number;
}
