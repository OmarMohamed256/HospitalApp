using API.Constants;
using API.Errors;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using HospitalApp.Models.Entities;

namespace API.Services.Implementations
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IMapper _mapper;
        public SpecialityService(ISpecialityRepository specialityRepository,
        IMapper mapper)
        {
            _specialityRepository = specialityRepository;
            _mapper = mapper;
        }

        public async Task<SpecialityDto> AddUpdateSpeciality(SpecialityDto specialityDto)
        {
            var speciality = _mapper.Map<Speciality>(specialityDto);
            var oldSpeciality = await _specialityRepository.GetSpecialityByIdAsync(speciality.Id);

            if(oldSpeciality == null)
                _specialityRepository.AddSpeciality(speciality);
            else
                _specialityRepository.UpdateSpeciality(speciality);
            
            if (await _specialityRepository.SaveAllAsync()) return _mapper.Map<SpecialityDto>(speciality);
            
            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add speciality");
        }

        public async Task<IEnumerable<SpecialityDto>> GetAllSpecialitiesAsync()
        {
            var specalities = await _specialityRepository.GetSpecialitiesAsync();
            return _mapper.Map<IEnumerable<SpecialityDto>>(specalities);
        }
    }
}