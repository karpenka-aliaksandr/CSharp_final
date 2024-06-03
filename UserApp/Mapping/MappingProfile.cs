using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using UserApp.DTO;
using UserApp.Model;

namespace UserApp.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RoleType, RoleId>()
                .ConvertUsing(src => ConvertRoleTypeToRoleId(src));

            CreateMap<RoleId, RoleType>()
                .ConvertUsing(src => ConvertRoleIdToRoleType(src));
        }

        private RoleId ConvertRoleTypeToRoleId(RoleType roleType)
        {
            // Ваша логика для преобразования RoleType в RoleId
            switch (roleType)
            {
                case RoleType.Admin:
                    return RoleId.Admin;
                case RoleType.User:
                    return RoleId.User;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleType), roleType, null);
            }
        }

        private RoleType ConvertRoleIdToRoleType(RoleId roleId)
        {
            // Ваша логика для преобразования RoleId в RoleType
            switch (roleId)
            {
                case RoleId.Admin:
                    return RoleType.Admin;
                case RoleId.User:
                    return RoleType.User;
                default:
                    throw new ArgumentOutOfRangeException(nameof(roleId), roleId, null);
            }
        }

    }
}
