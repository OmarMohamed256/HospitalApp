using API.Constants;
using API.Errors;
using API.Helpers;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using HospitalApp.Models.Entities;

namespace API.Services.Implementations
{
    public class AppoinmentService : IAppoinmentService
    {
        private readonly IAppoinmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppoinmentService(IAppoinmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;

        }

        public async Task<AppointmentDto> CreateUpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            // before creating/updating an appointment make sure that doctor doesnt have an appointment booked at dateOfVisit
            // and make sure that patient doesnt already have an appointment at same time

            var appointment = _mapper.Map<Appointment>(appointmentDto);
            if (await IsAppointmentValid(appointment))
            {
                if (appointment.Id != 0) _appointmentRepository.UpdateAppointment(appointment);
                else
                {
                    appointment.Status = string.IsNullOrEmpty(appointment.Status) ? "booked" : appointment.Status;
                    _appointmentRepository.AddAppointment(appointment);
                }

                var result = await _appointmentRepository.SaveAllAsync();
                if (result) return _mapper.Map<AppointmentDto>(appointment);
            }
            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add/update appointment");
        }

        public async Task DeleteAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId) ?? throw new Exception("Appointment not found");
            if(appointment.Status == "finalized")
                throw new Exception("Appointment is finalized cannot delete");
            _appointmentRepository.DeleteAppointment(appointment);
            if(!await _appointmentRepository.SaveAllAsync()) throw new Exception("Can not delete appointment");
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId) ?? throw new Exception("Appointment not found");
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<PagedList<AppointmentDto>> GetAppointmentsAsync(AppointmentParams appointmentParams)
        {
            PagedList<Appointment> appointments = await _appointmentRepository.GetAppointmentsAsync(appointmentParams);
            var appointmentsDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return new PagedList<AppointmentDto>(appointmentsDto, appointments.TotalCount, appointments.CurrentPage, appointments.PageSize);
        }

        public async Task<PagedList<AppointmentDto>> GetAppointmentsForPatientAsync(AppointmentParams appointmentParams, int patientId)
        {
            PagedList<Appointment> appointments = await _appointmentRepository.GetAppointmentsByPatientIdAsync(appointmentParams, patientId);
            var appointmentsDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return new PagedList<AppointmentDto>(appointmentsDto, appointments.TotalCount, appointments.CurrentPage, appointments.PageSize);
        }

        public async Task<List<DateTime>> GetUpcomingAppointmentsDatesByDoctorIdAsync(int doctorId)
        {
            var upcomingAppointments = await _appointmentRepository.GetUpcomingAppointmentsDatesByDoctorIdAsync(doctorId);
            return upcomingAppointments;
        }

        private async Task<bool> IsAppointmentValid(Appointment appointment)
        {
            // Check doctor's availability, patient's availability, date of visit, etc.
            var patientAppointmentExist = await _appointmentRepository.GetAppointmentsForUserByDateOfVisit(appointment.DateOfVisit);
            if (patientAppointmentExist != null && patientAppointmentExist.Id != appointment.Id) throw new ApiException(HttpStatusCode.BadRequest, "Patient already has an appointment at same time");
            var doctorAppointmentList = await _appointmentRepository.GetUpcomingAppointmentsDatesByDoctorIdAsync(appointment.DoctorId);
            if (doctorAppointmentList.Any(date => date.Equals(appointment.DateOfVisit.Date) )) throw new ApiException(HttpStatusCode.BadRequest, "Doctor already has an appointment at the specified date.");
            return true;
        }
    }
}