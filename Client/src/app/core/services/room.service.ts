import { Injectable } from '@angular/core';
import { RoomParams } from 'src/app/models/roomParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Room } from 'src/app/models/room';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  baseUrl = environment.apiUrl;
  roomParams: RoomParams = {
    pageNumber: 1,
    pageSize: 15,
  };
  roomCache = new Map();

  constructor(private http: HttpClient) { }

  getRooms(roomParams: RoomParams) {
    var response = this.roomCache.get(Object.values(roomParams).join("-"));
    if (response) {
      return of(response);
    }
    let params = getPaginationHeaders(roomParams.pageNumber, roomParams.pageSize);
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
}
