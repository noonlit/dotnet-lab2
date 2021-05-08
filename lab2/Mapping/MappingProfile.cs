using AutoMapper;
using Lab2.Models;
using Lab2.ViewModels;
namespace Lab2.Mapping
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Movie, MovieViewModel>();
    }
  }
}
