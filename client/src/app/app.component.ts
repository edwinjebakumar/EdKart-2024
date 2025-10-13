import { Component, inject, OnInit, signal } from '@angular/core';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl ='https://localhost:5001/api/';
  private httpClient = inject(HttpClient);
  title = 'Edkart';
  products: Product[] = [];

  ngOnInit(): void {
    this.httpClient.get<Pagination<Product>>(this.baseUrl+'products').subscribe({
      next: response => this.products = response.data,
      error: error => console.error('There was an error!', error),
      complete: () => console.log('Request completed')
    });
  }
}
