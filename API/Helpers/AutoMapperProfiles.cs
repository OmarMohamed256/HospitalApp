using API.Models.DTOS;
using API.Models.DTOS.ImageDtos;
using API.Models.DTOS.InventoryDtos;
using API.Models.DTOS.InvoiceDtos;
using API.Models.Entities;
using AutoMapper;
using HospitalApp.Models.Entities;
using webapi.Entities;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserInfoDto>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => new UserRoleDto
                {
                    RoleId = ur.Role.Id,
                    RoleName = ur.Role.Name
                }).ToList()));

            CreateMap<AppUser, ClinicDoctorDto>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.BookedWithAppointments))
                .ReverseMap();

            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<Appointment, ClinicAppointmentDto>().ReverseMap();

            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<ServiceInventoryItem, CreateServiceInventoryItemDTO>()
                .ForMember(dest => dest.QuantityNeeded, opt => opt.MapFrom(src => src.QuantityNeeded))
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.InventoryItemId))
                .ReverseMap();
            CreateMap<Service, CreateServiceDTO>()
            .ForMember(dest => dest.ServiceInventoryItems, opt => opt.MapFrom(src => src.ServiceInventoryItems)).ReverseMap();
            CreateMap<ServiceInventoryItemDto, ServiceInventoryItem>()
                .ForMember(dest => dest.InventoryItem, opt => opt.MapFrom(src => src.InventoryItem))
                .ReverseMap();

            CreateMap<InvoiceDoctorService, CreateInvoiceDoctorServiceDto>().ReverseMap();
            CreateMap<InvoiceDoctorService, InvoiceDoctorServiceDto>().ReverseMap();

            CreateMap<CustomItem, CreateCustomItemDto>().ReverseMap();
            CreateMap<CustomItem, CustomItemDto>().ReverseMap();

            CreateMap<CreateInvoiceDto, Invoice>()
                .ForMember(dest => dest.InvoiceDoctorService, opt => opt.MapFrom(src => src.InvoiceDoctorServices))
                .ForMember(dest => dest.CustomItems, opt => opt.MapFrom(src => src.CustomItems))
                .ReverseMap();

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(dest => dest.InvoiceDoctorService, opt => opt.MapFrom(src => src.InvoiceDoctorServices))
                .ForMember(dest => dest.CustomItems, opt => opt.MapFrom(src => src.CustomItems))
                .ForMember(dest => dest.Appointment, opt => opt.MapFrom(src => src.Appointment))
                .ForMember(dest => dest.InvoiceMedicines, opt => opt.MapFrom(src => src.InvoiceMedicines))
                .ReverseMap();

            CreateMap<DoctorServiceDto, DoctorService>().ReverseMap();
            CreateMap<MedicineDto, Medicine>().ReverseMap();
            CreateMap<DoctorWorkingHoursDto, DoctorWorkingHours>().ReverseMap();
            CreateMap<InventoryItemDto, InventoryItem>().ReverseMap();
            CreateMap<SupplyOrderDto, SupplyOrder>().ReverseMap();
            CreateMap<SellOrderDto, SellOrder>().ReverseMap();
            CreateMap<InvoiceMedicineDto, InvoiceMedicine>()
            .ForMember(dest => dest.Medicine, opt => opt.MapFrom(src => src.Medicine))
            .ReverseMap();
            
            
            CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.Patient))
            .ReverseMap();

            CreateMap<Clinic, ClinicDto>()
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
            .ReverseMap();
            CreateMap<Clinic, CreateClinicDto>().ReverseMap();
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore()); // Assuming SecurityStamp should be ignored during mapping.
        }

    }
}