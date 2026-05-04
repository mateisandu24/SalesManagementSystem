using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public enum Brand
    {
        ScottishFineSoaps,
        BrandArkhitekts,
        SuperFacialist,
        Polaar,
        SkinnyTan,
        Other
    }

    public enum MainCategory
    {
        BodyCare,
        FootCare,
        Bath,
        Other
    }

    public enum SubCategory
    {
        Lotion,
        Scrub,
        ShowerGel,
        Soap,
        Other
    }
}