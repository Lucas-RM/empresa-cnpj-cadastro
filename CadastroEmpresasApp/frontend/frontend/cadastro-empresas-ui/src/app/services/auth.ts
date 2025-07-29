import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';

import { UsuarioLogin, UsuarioCadastro, RespostaAutenticacao, Usuario } from '../models/usuario.model';
import { API_ENDPOINTS } from '../constants/api.constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private usuarioAtualSubject = new BehaviorSubject<Usuario | null>(null);
  public usuarioAtual$ = this.usuarioAtualSubject.asObservable();

  constructor(private http: HttpClient) {
    this.carregarUsuarioDoStorage();
  }

  login(credenciais: UsuarioLogin): Observable<RespostaAutenticacao> {
    return this.http.post<RespostaAutenticacao>(API_ENDPOINTS.AUTH.LOGIN, credenciais).pipe(
      tap((resposta) => {
        this.salvarToken(resposta.token);

        const usuario: Usuario = {
          nome: resposta.nome,
          email: resposta.email
        };

        this.salvarUsuario(usuario);
        this.usuarioAtualSubject.next(usuario);
      })
    );
  }

  cadastro(usuario: UsuarioCadastro): Observable<RespostaAutenticacao> {
    return this.http.post<RespostaAutenticacao>(API_ENDPOINTS.AUTH.CADASTRO, usuario).pipe(
      tap((resposta) => {
        this.salvarToken(resposta.token);

        const usuarioObj: Usuario = {
          nome: resposta.nome,
          email: resposta.email
        };

        this.salvarUsuario(usuarioObj);
        this.usuarioAtualSubject.next(usuarioObj);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.usuarioAtualSubject.next(null);
  }

  estaLogado(): boolean {
    const token = this.obterToken();
    const temToken = !!token;
    return temToken;
  }

  obterToken(): string | null {
    return localStorage.getItem('token');
  }

  private salvarToken(token: string): void {
    localStorage.setItem('token', token);
  }

  private salvarUsuario(usuario: Usuario): void {
    localStorage.setItem('usuario', JSON.stringify(usuario));
  }

  private carregarUsuarioDoStorage(): void {
    const token = this.obterToken();
    const usuarioStr = localStorage.getItem('usuario');

    if (token && usuarioStr) {
      try {
        const usuario = JSON.parse(usuarioStr);
        this.usuarioAtualSubject.next(usuario);
      } catch (error) {
        this.logout();
      }
    }
  }

  getUsuarioAtual(): Usuario | null {
    return this.usuarioAtualSubject.value;
  }
}
