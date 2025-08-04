import { Component, Renderer2 } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Sistema de Gerenciamento';
  isHomePage = false;
  
  constructor(private router: Router, private renderer: Renderer2) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      this.isHomePage = event.url === '/home';
      
      if (this.isHomePage) {
        this.renderer.addClass(document.body, 'home-page');
      } else {
        this.renderer.removeClass(document.body, 'home-page');
      }
    });
  }
}

