import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { AccountService } from './core/services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild('toastContainer', { read: ViewContainerRef }) toastContainer!: ViewContainerRef;

  title = 'Client';
  constructor(private accountService: AccountService) { }
  ngOnInit() {
    this.setCurrentUser();
  }
  
  setCurrentUser() {
    const user = localStorage.getItem('user');
    if (user) {
      this.accountService.setCurrentUser(JSON.parse(user));
    }
    
  }
}
