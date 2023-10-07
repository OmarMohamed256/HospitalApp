import { TimeSpan } from "../timeSpan";

export interface DoctorWorkingHours {
    doctorId: number;
    dayOfWeek: number;
    startTime: TimeSpan;
    endTime: TimeSpan;
}