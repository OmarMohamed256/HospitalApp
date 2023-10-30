import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map, take } from 'rxjs';
import { User } from 'src/app/models/UserModels/user';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  const user: User = JSON.parse(localStorage.getItem('user')!);
  if (user) {
    accountService.setCurrentUser(user);
    return true;
  } else {
    let currentUser: User | any;
    accountService.currentUser$.pipe().subscribe(user => currentUser = user);
    if (currentUser != null && currentUser != undefined) {
      return true;
    }
  }
  toastr.error("Please login to continue")
  router.navigate(['/login']);
  return false
};
