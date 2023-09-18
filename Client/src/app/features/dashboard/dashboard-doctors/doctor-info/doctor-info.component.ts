import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserData } from 'src/app/models/userData';

@Component({
  selector: 'app-doctor-info',
  templateUrl: './doctor-info.component.html',
  styleUrls: ['./doctor-info.component.scss']
})
export class DoctorInfoComponent implements OnInit {
  user: UserData | undefined;
  activePane = 0;
  constructor(private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    })
  }
  onTabChange($event: number) {
    this.activePane = $event;
  }
}
