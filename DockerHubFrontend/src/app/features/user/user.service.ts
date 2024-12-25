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

  user$ = new BehaviorSubject<User>({id: 0, email: "", username: "", password: "", role: -1});
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
            alert('Wrong email or password!')
            console.error('Login failed:', error);
          }
        )
      );
  }

  register(user: User): Observable<User> {
    return this.http.post<User>(environment.apiHost + 'authentication/register', user)
  }

  logout(): void {
    this.router.navigate(['/login']).then((_) => {
      this.tokenStorage.clear();
      this.user$.next({
        id: 0,
        email: '',
        username: "",
        password: "",
        role: -1,
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
      username: "",
      password: ""

    };
    this.user$.next(user);
  }
}
