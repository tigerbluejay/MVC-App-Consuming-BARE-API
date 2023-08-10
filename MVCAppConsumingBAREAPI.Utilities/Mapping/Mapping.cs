using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAppConsumingBAREAPI.Utilities.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // define here why exactly we are performing these mappings.
            // where in the application are they being used and for what reason.

            //CreateMap<ApartmentComplexDTO, ApartmentComplexCreateDTO>().ReverseMap();
            //CreateMap<ApartmentComplexDTO, ApartmentComplexUpdateDTO>().ReverseMap();

            //CreateMap<ApartmentUnitDTO, ApartmentUnitCreateDTO>().ReverseMap();
            //CreateMap<ApartmentUnitDTO, ApartmentUnitUpdateDTO>().ReverseMap();

        }
    }
}
