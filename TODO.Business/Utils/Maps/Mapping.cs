using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Business.DTOs;
using TODO.DataAccess.Models;

namespace TODO.Business.Utils.Maps
{
    public sealed class Mapping
    {
        private static Lazy<Mapper> _mapper = new Lazy<Mapper>(() =>
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Todo, TodoCreateDto>().ReverseMap();

                cfg.CreateMap<TodoReturnDto, Todo>().ReverseMap()
                .ForMember(dest => dest.DueDate, act => act.MapFrom(val => val.DueDate.Value.ToString("yyyy-MM-dd")));

                cfg.CreateMap<Todo, TodoUpdateDto>().ReverseMap();
            });

            return new Mapper(configuration);
        });


        public static Mapper Mapper => _mapper.Value;
    }
}
