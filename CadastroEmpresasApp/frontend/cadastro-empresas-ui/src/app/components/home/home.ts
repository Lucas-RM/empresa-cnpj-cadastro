import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';

import { AuthService } from '../../services/auth';
import { EmpresaService } from '../../services/empresa';
import { EmpresaListagem, RespostaListagemEmpresas } from '../../models/empresa.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatToolbarModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class HomeComponent implements OnInit {
  empresas: EmpresaListagem[] = [];
  isLoading = false;

  // Propriedades de paginação
  paginaAtual = 1;
  tamanhoPagina = 6;
  totalEmpresas = 0;
  totalPaginas = 0;

  constructor(
    private authService: AuthService,
    private empresaService: EmpresaService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.carregarEmpresas();
  }

  async carregarEmpresas(): Promise<void> {
    this.isLoading = true;

    try {
      const resposta = await this.empresaService.listarEmpresas({
        pagina: this.paginaAtual,
        tamanho: this.tamanhoPagina
      }).toPromise();

      if (resposta) {
        this.empresas = resposta.dados;
        this.totalEmpresas = resposta.total;
        this.paginaAtual = resposta.paginaAtual;
        this.tamanhoPagina = resposta.paginaTamanho;
        this.totalPaginas = Math.ceil(this.totalEmpresas / this.tamanhoPagina);
      }
    } catch (error: any) {
      let mensagem = 'Erro ao carregar empresas.';
      if (error.status === 401) {
        mensagem = 'Sessão expirada. Faça login novamente.';
        this.authService.logout();
        this.router.navigate(['/login']);
      } else if (error.error?.message) {
        mensagem = error.error.message;
      }

      this.snackBar.open(mensagem, 'Fechar', { duration: 5000 });
    } finally {
      this.isLoading = false;
    }
  }

  paginaAnterior(): void {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
      this.carregarEmpresas();
    }
  }

  paginaProxima(): void {
    if (this.paginaAtual < this.totalPaginas) {
      this.paginaAtual++;
      this.carregarEmpresas();
    }
  }

  get podeIrParaAnterior(): boolean {
    return this.paginaAtual > 1;
  }

  get podeIrParaProxima(): boolean {
    return this.paginaAtual < this.totalPaginas;
  }

  get informacaoPagina(): string {
    const inicio = (this.paginaAtual - 1) * this.tamanhoPagina + 1;
    const fim = Math.min(this.paginaAtual * this.tamanhoPagina, this.totalEmpresas);
    return `${inicio}-${fim} de ${this.totalEmpresas}`;
  }

  formatarData(data: string): string {
    return new Date(data).toLocaleDateString('pt-BR');
  }

  navegarParaCadastro(): void {
    this.router.navigate(['/cadastro-empresa']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
    this.snackBar.open('Logout realizado com sucesso!', 'Fechar', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }

  getUsuarioAtual(): string {
    const usuario = this.authService.getUsuarioAtual();
    return usuario ? usuario.nome : 'Usuário';
  }
}
