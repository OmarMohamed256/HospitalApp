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
            var appointment = _mapper.Map<Appointment>(appointmentDto);

            if (appointment.Id != 0) _appointmentRepository.UpdateAppointment(appointment);
            else _appointmentRepository.AddAppointment(appointment);

            var result = await _appointmentRepository.SaveAllAsync();
            if (result) return _mapper.Map<AppointmentDto>(appointment);

            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add/update appointment");
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
    }
}