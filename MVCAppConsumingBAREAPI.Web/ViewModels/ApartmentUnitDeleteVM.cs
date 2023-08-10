using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAppConsumingBAREAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAppConsumingBAREAPI.Models.ViewModels
{
    // This view model is to display in the view the Apartment Unit Information
    // and a list of associated Apartement Complexes in the view's dropdown.
    public class ApartmentUnitDeleteVM
    {
        public ApartmentUnitDTO ApartmentUnit { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApartmentComplexList { get; set; }

        public ApartmentUnitDeleteVM()
        {

            ApartmentUnit = new ApartmentUnitDTO();
        }
    }

}
