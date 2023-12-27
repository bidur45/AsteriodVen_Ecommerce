using AutoMapper;
using Domain.Entities.ECommerce;
using Domain.Entities.Security;
using Presentation.DTOs.ECommerce;
using Presentation.DTOs.Security;

namespace Presentation.Utils
{
    public class MappingProfile:Profile

    {

        public MappingProfile() {
            CreateMap<AuthRequestDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<UserLoginInfoDTO, User>().ReverseMap();
            CreateMap<ProductListDTO, Product>().ReverseMap();
            CreateMap<ProductRequestDTO, Product>().ReverseMap();
            CreateMap<ProductResponseDTO, Product>().ReverseMap();



        }

    }
}
