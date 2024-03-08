using AutoMapper;
using NowaiTechAPI.DTO.OutputDto.ZugangDTO;
using NowaitechShared.DTO.InputDTO;
using NowaitechShared.DTO.OutputDTO.AktuellDTO;
using NowaitechShared.DTO.OutputDTO.LagerDTO;
using NowaitechShared.DTO.OutputDTO.POCDTO;
using NowaitechShared.DTO.OutputDTO.PODTO;
using NowaitechShared.Models.ExcelEntities.Aktuels;
using NowaitechShared.Models.ExcelEntities.Lager;
using NowaitechShared.Models.ExcelEntities.PO;
using NowaitechShared.Models.ExcelEntities.POC;
using NowaitechShared.Models.ExcelEntities.Zugang;
using NowaitechShared.Models.User;

namespace NowaitechShared.DTO.MappingProfile
{
    public class DtoProfiles : Profile
    {
        public DtoProfiles()
        {
            //Soruce -> target
            CreateMap<RouterAktuell, RouterAktuellDTO>();

            CreateMap<RouterAktuellOrderList, RouterAktuellOrderListDTO>();

            CreateMap<XWDMAktuell, XWDMAktuellDTO>();

            CreateMap<XWDMAktuellOrderList, XWDMAktuellOrderListDTO>();

            CreateMap<LagerCentral, LagerCentralDTO>();

            CreateMap<Cisco_PO, Cisco_PODTO>();

            CreateMap<Deltatel_PO, Deltatel_PODTO>();

            CreateMap<ZTE_PO, ZTE_PODTO>();

            CreateMap<JSLMultiProject, JSLMultiProjectDTO>();

            CreateMap<MultiProject, MultiProjectDTO>();

            CreateMap<RouterSwapAktuell, RouterSwapAktuellDTO>();

            CreateMap<ZugangsdatenAktuell, ZugangsdatenAktuellDTO>();

            CreateMap<User, UserDTO>();
        }
    }
}
