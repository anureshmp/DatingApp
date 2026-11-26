import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NgClass } from '@angular/common';
import { Nav } from '../layout/nav/nav';
import { AccountService } from '../core/services/account-service';
import { Home } from '../features/home/home';
import { User } from '../types/user';

@Component({
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
  imports: [Nav, RouterOutlet, NgClass]
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  protected router = inject(Router);
  private http = inject(HttpClient);
  protected readonly title = signal('client');
  protected members = signal<User[]>([]);

    ngOnInit(): void {
      this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
}
