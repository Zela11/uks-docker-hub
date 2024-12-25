import { Injectable } from '@angular/core';
import { ACCESS_TOKEN, USER } from './constants';
import { AuthenticationResponse } from '../features/user/model/auth-response.model';

@Injectable({
    providedIn: 'root',
  })
  export class TokenStorage {
    constructor() {}
  
    saveAccessToken(response: AuthenticationResponse): void {
      localStorage.removeItem(USER);
      localStorage.setItem(USER, response.id.toString());

      localStorage.removeItem(ACCESS_TOKEN);
      localStorage.setItem(ACCESS_TOKEN, response.accessToken);
    }
  
    getAccessToken() {
      return localStorage.getItem(ACCESS_TOKEN);
    }
  
    clear() {
      localStorage.removeItem(ACCESS_TOKEN);
      localStorage.removeItem(USER);
    }
  }