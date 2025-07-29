export interface Usuario {
  id?: number;
  nome: string;
  email: string;
  senha?: string;
}

export interface UsuarioLogin {
  email: string;
  senha: string;
}

export interface UsuarioCadastro {
  nome: string;
  email: string;
  senha: string;
}

export interface RespostaAutenticacao {
  token: string;
  nome: string;
  email: string;
}
