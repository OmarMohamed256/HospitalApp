using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services.Implementations
{
    public class SpecialityService : ISpecialityService
    {
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IMapper _mapper;

        public SpecialityService(ISpecialityRepository specialityRepository, IMapper mapper)
        {
            _specialityRepository = specialityRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SpecialityDto>> GetAllSpecialitiesAsync()
        {
            var specalities = await _specialityRepository.GetSpecialitiesAsync();
            return _mapper.Map<IEnumerable<SpecialityDto>>(specalities);
        }
    }
}