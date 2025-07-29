import { Routes } from '@angular/router';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './services/auth';

// Guard para verificar se o usuário está logado
const authGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.estaLogado()) {
    return true;
  }
  
  router.navigate(['/login']);
  return false;
};

// Guard para redirecionar usuários logados
const redirectIfLoggedIn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (authService.estaLogado()) {
    router.navigate(['/home']);
    return false;
  }
  
  return true;
};

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { 
    path: 'login', 
    loadComponent: () => import('./components/login/login').then(m => m.LoginComponent),
    canActivate: [redirectIfLoggedIn]
  },
  { 
    path: 'home', 
    loadComponent: () => import('./components/home/home').then(m => m.HomeComponent),
    canActivate: [authGuard]
  },
  { 
    path: 'cadastro-empresa', 
    loadComponent: () => import('./components/cadastro-empresa/cadastro-empresa').then(m => m.CadastroEmpresaComponent),
    canActivate: [authGuard]
  },
  { path: '**', redirectTo: '/login' }
];
