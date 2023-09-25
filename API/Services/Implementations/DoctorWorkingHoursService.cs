using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class DoctorWorkingHoursService : IDoctorWorkingHoursService
    {
        private readonly IDoctorWorkingHoursRepository _doctorWorkingHoursRepository;
        private readonly IMapper _mapper;
        public DoctorWorkingHoursService(IDoctorWorkingHoursRepository doctorWorkingHoursRepository, IMapper mapper)
        {
            _doctorWorkingHoursRepository = doctorWorkingHoursRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DoctorWorkingHoursDto>> GetDoctorWorkingHoursByDoctorIdAsync(int doctorId)
        {
            var doctorWorkingHours = await _doctorWorkingHoursRepository.GetDoctorWorkingHoursByDoctorIdAsync(doctorId);
            return _mapper.Map<IEnumerable<DoctorWorkingHoursDto>>(doctorWorkingHours);
        }
    }
}