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

        public async Task<AppointmentDto> CreateUpdateAppointmentAsync(AppointmentDto appointmentDto, bool canAddMedicines)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            if (await IsAppointmentValid(appointment))
            {
                var oldAppointment = await _appointmentRepository.GetAppointmentByIdAsync(appointment.Id);
                if (oldAppointment == null)
                {
                    // Check if medicine can be added
                    if (!canAddMedicines && appointment.AppointmentMedicines != null && appointment.AppointmentMedicines.Any())
                        throw new BadRequestException("Medicines cannot be added during appointment creation.");

                    appointment.Status = "booked";
                    _appointmentRepository.AddAppointment(appointment);
                }
                else
                {
                    if (oldAppointment.Status == "finalized")
                        throw new BadRequestException("Appointment is already finalized cannot update or add");
                    // Check if medicine can be added
                    if (!canAddMedicines && appointment.AppointmentMedicines != null && appointment.AppointmentMedicines.Any())
                        throw new BadRequestException("Medicines cannot be added during appointment update.");
                    oldAppointment.AppointmentMedicines = appointment.AppointmentMedicines;
                    oldAppointment.PatientId = appointment.PatientId;
                    oldAppointment.DoctorId = appointment.DoctorId;
                    oldAppointment.DateOfVisit = appointment.DateOfVisit;
                    oldAppointment.Status = appointment.Status;
                    oldAppointment.AppointmentSpecialityId = appointment.AppointmentSpecialityId;
                    oldAppointment.CreationNote = appointment.CreationNote;
                }
                var result = await _appointmentRepository.SaveAllAsync();
                if (result) return _mapper.Map<AppointmentDto>(appointment);
            }
            throw new Exception("Failed to add/update appointment");
        }

        private async Task<bool> IsAppointmentValid(Appointment appointment)
        {
            // Check DateOfVisit is in the future
            if (appointment.DateOfVisit <= DateTime.Now)
                throw new BadRequestException("Date of visit must be in the future.");
            // Check patient's availability etc.
            var patientAppointmentExist =
                await _appointmentRepository.GetAppointmentsForUserByDateOfVisit(appointment.DateOfVisit);
            if (patientAppointmentExist != null && patientAppointmentExist.Id != appointment.Id)
                throw new BadRequestException("Patient already has an appointment at same time.");
            // Check doctor's availability,
            var doctorAppointmentList =
                await _appointmentRepository.GetUpcomingAppointmentsDatesByDoctorIdAsync(appointment.DoctorId);
            if (doctorAppointmentList.Any(date => date.Equals(appointment.DateOfVisit.Date)))
                throw new BadRequestException("Doctor already has an appointment at the specified date.");

            return true;
        }

        public async Task DeleteAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsyncNoTracking(appointmentId) ?? throw new Exception("Appointment not found");
            if (appointment.Status == "finalized")
                throw new Exception("Appointment is finalized cannot delete");
            _appointmentRepository.DeleteAppointment(appointment);
            if (!await _appointmentRepository.SaveAllAsync()) throw new Exception("Can not delete appointment");
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsyncNoTracking(appointmentId) ?? throw new Exception("Appointment not found");
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
        public async Task<ICollection<MedicineDto>> GetMedicinesByAppointmentIdAsync(int appointmentId)
        {
            var appoinmentMedicineList =
                await _appointmentRepository.GetAppointmentMedicinesByWithMedicineAppointmentIdAsync(appointmentId);
            var medicineDtoList =
                appoinmentMedicineList.Select(am => am.Medicine).Select(m => _mapper.Map<MedicineDto>(m)).ToList();
            return medicineDtoList;
        }

        public async Task<PagedList<AppointmentDto>> GetAppointmentsForDoctorAsync(AppointmentParams appointmentParams, int doctorId)
        {
            PagedList<Appointment> appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(appointmentParams, doctorId);
            var appointmentsDto = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
            return new PagedList<AppointmentDto>(appointmentsDto, appointments.TotalCount, appointments.CurrentPage, appointments.PageSize);
        }
    }
}