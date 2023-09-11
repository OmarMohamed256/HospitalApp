import { Component } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from 'src/app/icons/icon-subset';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  constructor(private iconSetService: IconSetService)
  {
    iconSetService.icons = { ...iconSubset };
  }

}
