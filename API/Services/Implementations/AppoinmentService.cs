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
        public async Task<PagedList<AppointmentDto>> GetAppointmentsForUser(AppointmentParams appointmentParams, int patientId)
        {
            PagedList<Appointment> appointments = await _appointmentRepository.GetAppointmentsForUser(appointmentParams, patientId);
            IEnumerable<AppointmentDto> appointmentsDto = appointments.Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                AppointmentSpecialityId = appointment.AppointmentSpecialityId,
                DateCreated = appointment.DateCreated,
                DateUpdated = appointment.DateUpdated,
                CreationNote = appointment.CreationNote,
                DateOfVisit = appointment.DateOfVisit,
                DrawUrl = appointment.DrawUrl,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Status = appointment.Status,
                Type = appointment.Type,
                Doctor = _mapper.Map<UserInfoDto>(appointment.Doctor),
                Speciality = _mapper.Map<SpecialityDto>(appointment.AppointmentSpeciality),
            }).ToList();

            return new PagedList<AppointmentDto>(appointmentsDto, appointments.TotalCount, appointments.CurrentPage, appointments.PageSize);
        }
    }
}