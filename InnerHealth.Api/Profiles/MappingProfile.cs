using AutoMapper;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Models;

namespace InnerHealth.Api.Profiles;

/// <summary>
/// Configuração do AutoMapper pra converter entidades em DTOs e vice-versa.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // UserProfile
        CreateMap<UserProfile, UserProfileDto>().ReverseMap();

        // Water
        CreateMap<WaterIntake, WaterIntakeDto>().ReverseMap();
        CreateMap<CreateWaterIntakeDto, WaterIntake>();
        CreateMap<UpdateWaterIntakeDto, WaterIntake>();

        // Sunlight
        CreateMap<SunlightSession, SunlightSessionDto>().ReverseMap();
        CreateMap<CreateSunlightSessionDto, SunlightSession>();
        CreateMap<UpdateSunlightSessionDto, SunlightSession>();

        // Meditation
        CreateMap<MeditationSession, MeditationSessionDto>().ReverseMap();
        CreateMap<CreateMeditationSessionDto, MeditationSession>();
        CreateMap<UpdateMeditationSessionDto, MeditationSession>();

        // Sleep
        CreateMap<SleepRecord, SleepRecordDto>().ReverseMap();
        CreateMap<CreateSleepRecordDto, SleepRecord>();
        CreateMap<UpdateSleepRecordDto, SleepRecord>();

        // Physical Activity
        CreateMap<PhysicalActivity, PhysicalActivityDto>().ReverseMap();
        CreateMap<CreatePhysicalActivityDto, PhysicalActivity>();
        CreateMap<UpdatePhysicalActivityDto, PhysicalActivity>();

        // Task
        CreateMap<TaskItem, TaskItemDto>().ReverseMap();
        CreateMap<CreateTaskItemDto, TaskItem>();
        CreateMap<UpdateTaskItemDto, TaskItem>();
    }
}