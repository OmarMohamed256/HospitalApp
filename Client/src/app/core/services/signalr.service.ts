import { EventEmitter, Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  baseUrl = environment.baseUrl;
  appointmentStatusChanged = new EventEmitter<any>();

  constructor(private toastr: ToastrService) { }
  
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl + 'hubs/appointment', {
        transport: signalR.HttpTransportType.ServerSentEvents
      })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addAppointmentListner = () => {
    this.hubConnection.on('SendAppointmentStatusChange', (response: any) => {
      console.log('SignalR event received', response);  // Log the response here
      this.appointmentStatusChanged.emit(response);
      this.showAppointmentStatusChanged(response.appointmentId, response.status)
    });
  }

  showAppointmentStatusChanged(appointmentId: number, status: string) {
    this.toastr.success(`Appointment with id ${appointmentId} changed status to ${status}`)
  }

}
