using API.Helpers;
using API.Models.Entities;
using HospitalApp.Models.Entities;

namespace API.Repositories.Interfaces
{
    public interface IAppoinmentRepository
    {
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
        Task<PagedList<Appointment>> GetAppointmentsByPatientIdAsync(AppointmentParams appointmentParams, int patientId);
        Task<PagedList<Appointment>> GetAppointmentsByDoctorIdAsync(AppointmentParams appointmentParams, int doctorId);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<Appointment> GetAppointmentByIdAsyncNoTracking(int appointmentId);
        Task<List<DateTime>> GetUpcomingAppointmentsDatesByDoctorIdAsync(int doctorId);
        Task<PagedList<Appointment>> GetAppointmentsAsync(AppointmentParams appointmentParams);
        Task<Appointment> GetAppointmentsForUserByDateOfVisit(DateTime dateOfVisit);
        Task<(string Type, decimal? PriceVisit, decimal? PriceRevisit)> GetAppointmentTypeAndDoctorPricesAsync(int appointmentId);
        Task<bool> SaveAllAsync();
        Task<int> UpdateAppointmentInvoicedAsync(int appointmentId, string status, int invoiceId);
        void DeleteAppointmentMedicinesRange(ICollection<AppointmentMedicine> appointmentMedicines);
        Task<ICollection<AppointmentMedicine>> GetAppointmentMedicinesByWithMedicineAppointmentIdAsync(int appointmentId);
    }
}