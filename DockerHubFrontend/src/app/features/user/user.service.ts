import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { User } from './model/user-model.model';
import { environment } from 'src/env/environment';
import { TokenStorage } from 'src/app/shared/token.service';
import { AuthenticationResponse } from './model/auth-response.model';
import { BehaviorSubject, tap } from 'rxjs';
import { Credentials } from './model/credentials.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  user$ = new BehaviorSubject<User>({id: 0, email: "", role: ""});
  jwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router, private tokenStorage: TokenStorage) { }

  login(login: Credentials): Observable<any> {
    return this.http
      .post<AuthenticationResponse>(
        environment.apiHost + 'authentication/login',
        login,
        { observe: 'response' }
      )
      .pipe(
        tap(
          (authenticationResponse: any) => {
            this.tokenStorage.saveAccessToken(authenticationResponse.body);
            this.setUser();
          },
          (error) => {
            //this.toastr.error('Lozinka ili e-mail nisu ispravni!')
            console.error('Login failed:', error);
          }
        )
      );
  }

  /*register(client: Client): Observable<Client> {
    return this.http.post<Client>(environment.apiHost + 'authentication/register', client)
  }**/

  logout(): void {
    this.router.navigate(['/login']).then((_) => {
      this.tokenStorage.clear();
      this.user$.next({
        id: 0,
        email: '',
        role: '',
      });
    });
  }

  private setUser(): void {
    const accessToken = this.tokenStorage.getAccessToken() || "";
    if (!accessToken) {
      console.error('Token nije prisutan.');
      return;
    }
    const decodedToken = this.jwtHelperService.decodeToken(accessToken);
    console.log(decodedToken)
    const user: User = {
      id: decodedToken.id,
      email: decodedToken.email,
      role: decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ],

    };
    this.user$.next(user);
  }
}
