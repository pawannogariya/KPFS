import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpUrlEncodingCodec } from '@angular/common/http';
import { retry } from 'rxjs/operators';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public baseUrl: string;
  public headers: HttpHeaders;
  public readonly encoding = new HttpUrlEncodingCodec();

  public constructor(
    private readonly httpClient: HttpClient,
  ) {
    this.baseUrl = environment.apiUrl;
    this.headers = this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
  }

  public setToken(token: string | null | undefined) {
    if (token) {
      this.headers = this.headers.set('Authorization', `Bearer ${token}`);
    } else {
      this.headers = this.headers.delete('Authorization');
    }
  }

  public get<TResponse>(
    path: string,
    params?: { [param: string]: string | undefined | null | string[] },
    retryCount: number = 0
  ): Promise<TResponse> {
    const request = this.httpClient
      .get<TResponse>(`${this.baseUrl}${path}`, { params: this.cleanupParams(params), responseType: 'json', headers: this.headers })
      .pipe(retry(retryCount));

    return lastValueFrom(request);
  }

  public post<TResponse>(path: string, data: any = null): Promise<TResponse> {
    const request = this.httpClient.post<TResponse>(`${this.baseUrl}${path}`, data, {
      headers: this.headers
    });

    return lastValueFrom(request);
  }

  public put<TResponse>(path: string, data: any): Promise<TResponse> {
    const request = this.httpClient.put<TResponse>(`${this.baseUrl}${path}`, data, {
      headers: this.headers
    });

    return lastValueFrom(request);
  }

  public delete<TResponse>(path: string): Promise<TResponse> {
    const request = this.httpClient.delete<TResponse>(`${this.baseUrl}${path}`, {
      headers: this.headers
    });

    return lastValueFrom(request);
  }

  private cleanupParams(params: { [param: string]: string | null | undefined | string[] } | null | undefined): { [param: string]: string | string[] } {
    let cleanParams: { [param: string]: string | string[] } = {};
    if (!params) {
      return cleanParams;
    }

    for (let key in params) {
      if (params[key]) {
        cleanParams[key] = params[key]!;
      }
    }

    return cleanParams;
  }

  public getDownloadableDocument(
    path: string,
    params?: { [param: string]: string | undefined | null | string[] },
    retryCount: number = 0
  ) {

    const request = this.httpClient
      .get(`${this.baseUrl}${path}`, {
        params: this.cleanupParams(params),
        responseType: 'blob',
        headers: this.headers
      })
    return request;
  }
}
