import { Component, OnInit } from '@angular/core';
import { UserService } from '../../user/user.service';
import { Router } from '@angular/router';
import { User } from '../../user/model/user-model.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  user!: User

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.userService.user$.subscribe(user => {
      this.user = user
    })
  }

  logout(): void {
    this.userService.logout();
    this.router.navigate(['login']);
  }

  navigateToLogin(): void {
    this.router.navigate(['/login'])
  }

  isLoggedIn(): boolean {
    if(this.user.id == 0) {
      return false;
    } else {
      return true;
    }
}
}
