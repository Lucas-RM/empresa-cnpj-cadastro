export interface EnderecoEmpresa {
  id?: number;
  cep: string;
  logradouro: string;
  numero: string;
  complemento?: string;
  bairro: string;
  cidade: string;
  estado: string;
}

export interface Empresa {
  id?: number;
  razaoSocial: string;
  nomeFantasia: string;
  cnpj: string;
  email: string;
  telefone: string;
  endereco: EnderecoEmpresa;
}

export interface EmpresaCadastro {
  razaoSocial: string;
  nomeFantasia: string;
  cnpj: string;
  email: string;
  telefone: string;
  endereco: EnderecoEmpresa;
}

export interface EmpresaListagem {
  cnpj: string;
  nome: string;
  nomeFantasia: string;
  situacao: string;
  abertura: string;
  atividadePrincipal: string;
  logradouro: string;
  numero: string;
  municipio: string;
  uf: string;
  cep: string;
}

export interface RespostaListagemEmpresas {
  total: number;
  paginaAtual: number;
  paginaTamanho: number;
  dados: EmpresaListagem[];
} 