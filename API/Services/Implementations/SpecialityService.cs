using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Constants;
using API.Errors;
using API.Models.DTOS;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using HospitalApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<SpecialityDto> AddSpeciality(SpecialityDto specialityDto)
        {
            var speciality = _mapper.Map<Speciality>(specialityDto);
            _specialityRepository.AddSpeciality(speciality);
            bool addSpecialityResult = await _specialityRepository.SaveAllAsync();
            if (addSpecialityResult)
            {
                specialityDto.Id = speciality.Id;
                return specialityDto;
            }
            throw new ApiException(HttpStatusCode.InternalServerError, "Failed to add speciality");
        }

        public async Task<IEnumerable<SpecialityDto>> GetAllSpecialitiesAsync()
        {
            var specalities = await _specialityRepository.GetSpecialitiesAsync();
            return _mapper.Map<IEnumerable<SpecialityDto>>(specalities);
        }
    }
}