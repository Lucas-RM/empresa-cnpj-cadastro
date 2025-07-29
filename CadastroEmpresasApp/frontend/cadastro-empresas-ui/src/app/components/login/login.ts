import { Component, OnInit } from '@angular/core';
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
import { Location } from '@angular/common';

import { AuthService } from '../../services/auth';
import { UsuarioCadastro, UsuarioLogin } from '../../models/usuario.model';

@Component({
  selector: 'app-login',
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
    MatProgressSpinnerModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  cadastroForm!: FormGroup;
  isLoading = false;
  mostrarSenha = false;
  mostrarSenhaCadastro = false;
  modoCadastro = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
    private location: Location
  ) {
    this.inicializarFormularios();
  }

  ngOnInit(): void {
    this.loginForm.get('email')?.valueChanges.subscribe(email => {
      if (email && !this.validarEmail(email)) {
        this.loginForm.get('email')?.setErrors({ emailInvalido: true });
      }
    });

    this.cadastroForm.get('email')?.valueChanges.subscribe(email => {
      if (email && !this.validarEmail(email)) {
        this.cadastroForm.get('email')?.setErrors({ emailInvalido: true });
      }
    });
  }

  private validarEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  private inicializarFormularios(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    });

    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(8)]],
      confirmarSenha: ['', [Validators.required]]
    }, { validators: this.confirmarSenhaValidator });
  }

  confirmarSenhaValidator(group: FormGroup) {
    const senha = group.get('senha');
    const confirmarSenha = group.get('confirmarSenha');

    if (senha && confirmarSenha) {
      const iguais = senha.value === confirmarSenha.value;
      if (!iguais) {
        confirmarSenha.setErrors({ senhasDiferentes: true });
      } else {
        if (confirmarSenha.hasError('senhasDiferentes')) {
          confirmarSenha.setErrors(null);
        }
      }
    }

    return null;
  }

  alternarModo(): void {
    this.modoCadastro = !this.modoCadastro;

    if (this.modoCadastro) {
      this.location.go('/cadastrar');
    } else {
      this.router.navigate(['/login']);
    }

    this.loginForm.reset();
    this.cadastroForm.reset();
  }

  toggleMostrarSenha(): void {
    this.mostrarSenha = !this.mostrarSenha;
  }

  toggleMostrarSenhaCadastro(): void {
    this.mostrarSenhaCadastro = !this.mostrarSenhaCadastro;
  }

  async onSubmit(): Promise<void> {
    if (this.modoCadastro) {
      await this.cadastrar();
    } else {
      await this.fazerLogin();
    }
  }

  private async fazerLogin(): Promise<void> {
    if (this.loginForm.valid) {
      this.isLoading = true;
      const credenciais: UsuarioLogin = this.loginForm.value;

      try {
        await this.authService.login(credenciais).toPromise();
        this.router.navigate(['/home']);
        this.mostrarMensagem('Login realizado com sucesso!');
      } catch (error: any) {
        this.mostrarMensagem(
          error.error|| 'Erro ao fazer login. Verifique suas credenciais.',
          'erro'
        );
      } finally {
        this.isLoading = false;
      }
    }
  }

  private async cadastrar(): Promise<void> {
    if (this.cadastroForm.valid) {
      this.isLoading = true;
      const dadosCadastro: UsuarioCadastro = {
        nome: this.cadastroForm.get('nome')?.value,
        email: this.cadastroForm.get('email')?.value,
        senha: this.cadastroForm.get('senha')?.value
      };

      try {
        await this.authService.cadastro(dadosCadastro).toPromise();
        this.router.navigate(['/home']);
        this.mostrarMensagem('Cadastro realizado com sucesso!');
      } catch (error: any) {
        this.mostrarMensagem(
          error.error || 'Erro ao realizar cadastro.',
          'erro'
        );
      } finally {
        this.isLoading = false;
      }
    }
  }

  private mostrarMensagem(mensagem: string, tipo: 'sucesso' | 'erro' = 'sucesso'): void {
    this.snackBar.open(mensagem, 'Fechar', {
      duration: tipo === 'erro' ? 5000 : 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }

  // Métodos para exibir erros de validação
  getEmailError(form: FormGroup): string {
    const emailControl = form.get('email');
    if (emailControl?.hasError('required')) {
      return 'Email é obrigatório';
    }
    if (emailControl?.hasError('emailInvalido')) {
      return 'Email inválido';
    }
    return '';
  }

  getSenhaError(form: FormGroup): string {
    const senhaControl = form.get('senha');
    if (senhaControl?.hasError('required')) {
      return 'Senha é obrigatória';
    }
    return '';
  }

  getNomeCadastroError(): string {
    const nomeControl = this.cadastroForm.get('nome');
    if (nomeControl?.hasError('required')) {
      return 'Nome é obrigatório';
    }
    if (nomeControl?.hasError('minlength')) {
      return 'Nome deve ter pelo menos 3 caracteres';
    }
    return '';
  }

  getSenhaCadastroError(): string {
    const senhaControl = this.cadastroForm.get('senha');
    if (senhaControl?.hasError('required')) {
      return 'Senha é obrigatória';
    }
    if (senhaControl?.hasError('minlength')) {
      return 'Senha deve ter pelo menos 8 caracteres';
    }
    return '';
  }

  getConfirmarSenhaError(): string {
    const confirmarSenhaControl = this.cadastroForm.get('confirmarSenha');
    if (confirmarSenhaControl?.hasError('required')) {
      return 'Confirmação de senha é obrigatória';
    }
    if (confirmarSenhaControl?.hasError('senhasDiferentes')) {
      return 'Senhas não coincidem';
    }
    return '';
  }
}
