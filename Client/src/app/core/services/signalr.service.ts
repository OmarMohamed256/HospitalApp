import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  baseUrl = environment.baseUrl;

  constructor(private toastr: ToastrService) { }
  
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'hubs/appointment', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addAppointmentListner = () => {
    this.hubConnection.on('SendAppointmentFinalized', (response: any) => {
      this.showNotification(response);
    });
  }

  showNotification(response: any) {
    this.toastr.warning(response);
  }
}
