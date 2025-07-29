import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';

import { Empresa, EmpresaListagem, RespostaListagemEmpresas } from '../models/empresa.model';
import { API_ENDPOINTS } from '../constants/api.constants';

export interface CadastroEmpresaRequest {
  cnpj: string;
}

@Injectable({
  providedIn: 'root',
})
export class EmpresaService {
  constructor(private http: HttpClient) {}

  listarEmpresas(): Observable<EmpresaListagem[]> {
    return this.http.get<RespostaListagemEmpresas>(API_ENDPOINTS.EMPRESA.LISTAR).pipe(
      map(response => response.dados)
    );
  }

  cadastrarEmpresa(empresa: CadastroEmpresaRequest): Observable<Empresa> {
    return this.http.post<Empresa>(API_ENDPOINTS.EMPRESA.CADASTRAR, empresa);
  }
}
