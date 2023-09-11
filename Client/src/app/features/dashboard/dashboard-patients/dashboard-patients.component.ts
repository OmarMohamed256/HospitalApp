import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard-patients',
  templateUrl: './dashboard-patients.component.html',
  styleUrls: ['./dashboard-patients.component.scss']
})
export class DashboardPatientsComponent implements OnInit{
  constructor(private toastrService: ToastrService)
  {
  }
  ngOnInit(): void {
  }
    
}
