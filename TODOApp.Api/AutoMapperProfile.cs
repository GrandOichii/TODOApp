using AutoMapper;

namespace TODOApp.Api;

public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<UserTask, GetUserTask>();
            CreateMap<CreateUserTask, UserTask>();
        }
}