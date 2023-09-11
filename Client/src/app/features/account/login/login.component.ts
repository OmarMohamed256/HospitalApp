import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IconSetService } from '@coreui/icons-angular';
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

  constructor(private iconSetService: IconSetService, private fb: FormBuilder, private accountService: AccountService)
  {
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
    this.accountService.login(this.loginForm.value).subscribe(response => {
      console.log(response)
    })
  }

}
