export interface IUser {
  id: number;
  name: string;
  email: string;
  password?: string | null;
  isAdmin: boolean;
  active: boolean;
}
