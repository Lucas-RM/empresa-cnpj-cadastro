export const API_BASE_URL = 'https://localhost:7113/api';

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: `${API_BASE_URL}/auth/login`,
    CADASTRO: `${API_BASE_URL}/auth/registrar`,
  },
  EMPRESA: {
    LISTAR: `${API_BASE_URL}/empresa/listar`,
    CADASTRAR: `${API_BASE_URL}/empresa/cadastrar`,
  },
}; 