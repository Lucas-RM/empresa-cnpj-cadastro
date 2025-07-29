import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';

import { Empresa, EmpresaListagem, RespostaListagemEmpresas } from '../models/empresa.model';
import { API_ENDPOINTS } from '../constants/api.constants';

export interface CadastroEmpresaRequest {
  cnpj: string;
}

export interface PaginacaoParams {
  pagina?: number;
  tamanho?: number;
}

@Injectable({
  providedIn: 'root',
})
export class EmpresaService {
  constructor(private http: HttpClient) {}

  listarEmpresas(params?: PaginacaoParams): Observable<RespostaListagemEmpresas> {
    let httpParams = new HttpParams();
    
    if (params?.pagina) {
      httpParams = httpParams.set('pagina', params.pagina.toString());
    }
    
    if (params?.tamanho) {
      httpParams = httpParams.set('tamanho', params.tamanho.toString());
    }

    return this.http.get<RespostaListagemEmpresas>(API_ENDPOINTS.EMPRESA.LISTAR, { params: httpParams });
  }

  cadastrarEmpresa(empresa: CadastroEmpresaRequest): Observable<Empresa> {
    return this.http.post<Empresa>(API_ENDPOINTS.EMPRESA.CADASTRAR, empresa);
  }
}
