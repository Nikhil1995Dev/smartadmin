using AutoMapper;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Mappings
{
    public class MapingProfile:Profile
    {
        public MapingProfile()
        {
            CreateMap<Client,ClientViewModel>().ReverseMap();
              CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<EmployeeMaster, EmployeeMasterViewModel>().ReverseMap();
            CreateMap<Availability,AvailabilityViewModel>().ReverseMap();
            CreateMap<Timing, TimingViewModel>().ForMember(dest => dest.ClientName, opt => opt.MapFrom(s => s.Client.ClientName)); 
            CreateMap<ContentManager, CMSViewModel>().ReverseMap();
            CreateMap<ImageGallary, ImageGallaryViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Appointment, AppointmentsViewModel>().ReverseMap();
            CreateMap<List<HraAssessment>, List<HraAssessmentViewModel>>().ReverseMap();
            CreateMap<BroadCast, BroadCastViewModel>().ReverseMap();
            CreateMap<OherBooking, OherBookingViewModel>().ReverseMap();
            CreateMap<AppointmentDocument, AppointmentDocumentViewModel>().ReverseMap();
        }
    }
}
