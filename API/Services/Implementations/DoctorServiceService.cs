using API.Models.DTOS;
using API.Models.Entities;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class DoctorServiceService : IDoctorServiceService
    {
        private readonly IMapper _mapper;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        public DoctorServiceService(IMapper mapper, IDoctorServiceRepository doctorServiceRepository)
        {
            _mapper = mapper;
            _doctorServiceRepository = doctorServiceRepository;
        }
        public async Task<IEnumerable<DoctorServiceDto>> GetDoctorServiceByDoctorId(int doctorId)
        {
            var services =  await _doctorServiceRepository.GetDoctorServiceByDoctorId(doctorId);
            return _mapper.Map<IEnumerable<DoctorServiceDto>>(services);
        }
    }
}