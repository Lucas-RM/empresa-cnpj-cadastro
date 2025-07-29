import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';

import { EmpresaService, CadastroEmpresaRequest } from '../../services/empresa';

@Component({
  selector: 'app-cadastro-empresa',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatToolbarModule
  ],
  templateUrl: './cadastro-empresa.html',
  styleUrl: './cadastro-empresa.scss'
})
export class CadastroEmpresaComponent {
  empresaForm: FormGroup;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private empresaService: EmpresaService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.empresaForm = this.fb.group({
      cnpj: ['', [Validators.required]]
    });
  }

  formatarCNPJ(event: any): void {
    let value = event.target.value.replace(/\D/g, '');
    if (value.length <= 14) {
      value = value.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$/, '$1.$2.$3/$4-$5');
      this.empresaForm.patchValue({ cnpj: value });
    }
  }

  async onSubmit(): Promise<void> {
    if (this.empresaForm.valid) {
      this.isLoading = true;
      const cnpj = this.empresaForm.get('cnpj')?.value;

      try {
        const request: CadastroEmpresaRequest = { cnpj };
        await this.empresaService.cadastrarEmpresa(request).toPromise();
        this.snackBar.open('Empresa cadastrada com sucesso!', 'Fechar', {
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'top'
        });
        this.router.navigate(['/home']);
      } catch (error: any) {
        let mensagemErro = 'Erro ao cadastrar empresa.';

        if (error.error) {
            mensagemErro = error.error;
        }

        this.snackBar.open(mensagemErro, 'Fechar', {
          duration: 5000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
      } finally {
        this.isLoading = false;
      }
    }
  }

  getCNPJError(): string {
    const cnpjControl = this.empresaForm.get('cnpj');
    if (cnpjControl?.hasError('required')) {
      return 'CNPJ é obrigatório';
    }
    return '';
  }

  voltarParaHome(): void {
    this.router.navigate(['/home']);
  }
}
