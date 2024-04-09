export class TextFormatter {
  static formatCPF(cpf: string): string {
    cpf = cpf.replace(/\D/g, ''); // Remove caracteres não numéricos
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
  }

  static formatTelefoneFixo(telefone: string): string {
    telefone = telefone.replace(/\D/g, ''); // Remove caracteres não numéricos
    return telefone.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
  }

  static formatTelefoneCelular(telefone: string): string {
    telefone = telefone.replace(/\D/g, ''); // Remove caracteres não numéricos
    return telefone.replace(/(\d{2})(\d{1})(\d{4})(\d{4})/, '($1) $2 $3-$4');
  }

  static formatDate(data: string): string {
    const dateObj = new Date(data);
    const day = String(dateObj.getDate()).padStart(2, '0');
    const month = String(dateObj.getMonth() + 1).padStart(2, '0');
    const year = dateObj.getFullYear();

    return `${day}/${month}/${year}`;
  }
}
