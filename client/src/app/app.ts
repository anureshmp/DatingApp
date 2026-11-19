import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgClass } from '../../node_modules/@angular/common/types/_common_module-chunk';
import { Nav } from '../layout/nav/nav';

@Component({
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
  imports: [Nav]
})
export class App implements OnInit {

  private http = inject(HttpClient);
  protected readonly title = signal('client');
  protected members = signal<any>([]);

    ngOnInit(): void {
      this.http.get('https://localhost:5001/api/members').subscribe(
        {
          next: response => this.members.set(response),
          error: error => console.log(error),
          complete: () => console.log('Completed the http request')
        }
      )
  }
}
