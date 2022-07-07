using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MapperServices
{
    public  class IMapperService:Profile
    {
        public IMapperService()
        {
            CreateMap<DepartmentImageDto, DepartmentImage>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<SliderDto, Slider>();
            CreateMap<PositionDto, Position>();
            CreateMap<TeacherDto, Teacher>();
        }
    }
}
