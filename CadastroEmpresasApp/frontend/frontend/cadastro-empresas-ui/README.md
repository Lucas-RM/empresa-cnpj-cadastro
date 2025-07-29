# Sistema de Cadastro de Empresas - Frontend

Este é o frontend do Sistema de Cadastro de Empresas, desenvolvido em Angular 20.1.3 com Angular Material e validações em tempo real.

## 🚀 Tecnologias Utilizadas

- **Angular 20.1.3** - Framework principal
- **Angular Material** - Componentes de UI
- **Biome** - Formatação e linting de código
- **SCSS** - Pré-processador CSS
- **TypeScript** - Linguagem de programação
- **RxJS** - Programação reativa

## 📋 Pré-requisitos

- Node.js 22.16.0 ou superior
- npm (incluído com Node.js)

## 🛠️ Instalação e Configuração

### 1. Clone o repositório
```bash
git clone <url-do-repositorio>
cd CadastroEmpresasApp/frontend/cadastro-empresas-ui
```

### 2. Instale as dependências
```bash
npm install
```

### 3. Configure o backend
Certifique-se de que o backend está rodando na porta 5000. Se necessário, ajuste a URL da API no arquivo:
```
src/app/constants/api.constants.ts
```

### 4. Execute o projeto
```bash
npm start
```

O projeto estará disponível em `http://localhost:4200`

## 📁 Estrutura do Projeto

```
src/
├── app/
│   ├── components/
│   │   ├── login/           # Componente de login/cadastro
│   │   ├── home/            # Página principal
│   │   └── cadastro-empresa/ # Formulário de cadastro de empresa
│   ├── services/
│   │   ├── auth.ts          # Serviço de autenticação
│   │   ├── empresa.ts       # Serviço de empresas
│   │   └── validacao.service.ts # Validações em tempo real
│   ├── models/
│   │   ├── usuario.model.ts # Interfaces de usuário
│   │   └── empresa.model.ts # Interfaces de empresa
│   ├── constants/
│   │   └── api.constants.ts # Constantes da API
│   ├── app.config.ts        # Configuração da aplicação
│   ├── app.routes.ts        # Configuração de rotas
│   └── app.ts              # Componente principal
├── styles.scss             # Estilos globais
└── main.ts                # Ponto de entrada
```

## 🔧 Funcionalidades

### Autenticação
- Login de usuário
- Cadastro de novo usuário
- Validação de email em tempo real
- Validação de força de senha
- Armazenamento de token JWT

### Cadastro de Empresas
- Formulário completo com validações
- Validação de CNPJ em tempo real
- Formatação automática de campos (CNPJ, telefone, CEP)
- Validação de email e telefone
- Endereço completo com validação de CEP

### Interface
- Design responsivo com Angular Material
- Tema personalizado
- Animações e transições suaves
- Feedback visual para validações

## 🎨 Validações Implementadas

### Email
- Formato válido de email
- Validação em tempo real

### Senha
- Mínimo 8 caracteres
- Pelo menos uma letra maiúscula
- Pelo menos uma letra minúscula
- Pelo menos um número
- Pelo menos um caractere especial

### CNPJ
- Validação completa dos dígitos verificadores
- Formatação automática (XX.XXX.XXX/XXXX-XX)
- Validação em tempo real

### Telefone
- Suporte para DDD + 8 ou 9 dígitos
- Formatação automática ((XX) XXXXX-XXXX)
- Validação em tempo real

### CEP
- Formato válido (XXXXX-XXX)
- Validação em tempo real

## 🚀 Scripts Disponíveis

```bash
# Desenvolvimento
npm start          # Inicia o servidor de desenvolvimento
npm run build      # Compila o projeto para produção
npm run watch      # Compila em modo watch

# Testes
npm test           # Executa os testes

# Formatação e Linting
npm run format     # Formata o código com Biome
npm run lint       # Executa o linter
npm run check      # Formata e verifica o código
```

## 🔒 Segurança

- Autenticação JWT
- Guards de rota para proteção de páginas
- Validação de dados no frontend e backend
- Sanitização de inputs

## 📱 Responsividade

O projeto é totalmente responsivo e funciona em:
- Desktop (1024px+)
- Tablet (768px - 1023px)
- Mobile (até 767px)

## 🎯 Rotas da Aplicação

- `/` - Redireciona para `/login`
- `/login` - Página de login/cadastro
- `/home` - Página principal (requer autenticação)
- `/cadastro-empresa` - Formulário de cadastro de empresa (requer autenticação)

## 🔧 Configuração do Biome

O projeto utiliza o Biome para formatação e linting. A configuração está no arquivo `biome.json` na raiz do projeto.

## 📝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 🤝 Suporte

Para suporte, entre em contato através dos canais disponibilizados pela equipe de desenvolvimento.
