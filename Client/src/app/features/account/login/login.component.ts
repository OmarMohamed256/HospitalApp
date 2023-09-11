import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IconSetService } from '@coreui/icons-angular';
import { ROLES } from 'src/app/constants/roles';
import { AccountService } from 'src/app/core/services/account.service';
import { iconSubset } from 'src/app/icons/icon-subset';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  validationErrors: string[] = [];

  constructor(private iconSetService: IconSetService,
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router) {
    iconSetService.icons = { ...iconSubset };
  }
  ngOnInit(): void {
    this.intializeForm();
  }
  intializeForm() {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    })
  }
  login() {
    this.accountService.login(this.loginForm.value).subscribe(roles => {
      console.log(roles)
      if (roles.includes(ROLES.ADMIN) || roles.includes(ROLES.DOCTOR) || roles.includes(ROLES.RECEPTIONIST)) {
        this.router.navigate(['']); // Redirect to admin page
      } else {
        this.router.navigate(['']); // Redirect to user page
      }
    });
  }

}
