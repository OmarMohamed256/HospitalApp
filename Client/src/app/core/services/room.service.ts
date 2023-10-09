import { Injectable } from '@angular/core';
import { RoomParams } from 'src/app/models/Params/roomParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Room } from 'src/app/models/RoomModels/room';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  baseUrl = environment.apiUrl;
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 15,
    includeUpcomingAppointments: false,
    appointmentDateOfVisit: undefined,
    roomSpecialityId: null
  };
  roomCache = new Map();

  constructor(private http: HttpClient) { }

  getRooms(roomParams: RoomParams) {
    var response = this.roomCache.get(Object.values(roomParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(roomParams.pageNumber, roomParams.pageSize);
    params = params.append('includeUpcomingAppointments', roomParams.includeUpcomingAppointments);
    if(roomParams.appointmentDateOfVisit)
      params = params.append('appointmentDateOfVisit', roomParams.appointmentDateOfVisit);
    if(roomParams.roomSpecialityId)
      params = params.append('roomSpecialityId', roomParams.roomSpecialityId);
    return getPaginatedResult<Room[]>(this.baseUrl + 'room/', params, this.http)
      .pipe(map(response => {
        this.roomCache.set(Object.values(roomParams).join("-"), response);
        return response;
      }));
  }

  resetParams() {
    this.roomParams = new RoomParams();
    return this.roomParams;
  }

  createRoom(room: Room) {
    return this.http.post<Room>(this.baseUrl + 'room', room);
  }
  
  updateRoom(room: Room) {
    return this.http.put<Room>(this.baseUrl + 'room', room);
  }
}
