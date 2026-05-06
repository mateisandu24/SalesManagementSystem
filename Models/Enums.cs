using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementSystem.Models
{
    public enum Role
    {
        Admin = 1,
        User = 2
    }

    public enum Brand
    {
        ScottishFineSoaps = 1,
        BrandArkhitekts = 2,
        SuperFacialist = 3,
        Polaar = 4,
        SkinnyTan = 5,
        Other = 6
    }

    public enum MainCategory
    {
        BodyCare = 1,
        FootCare = 2,
        Bath = 3,
        Other = 4
    }

    public enum SubCategory
    {
        Lotion = 1,
        Scrub = 2,
        ShowerGel = 3,
        Soap = 4,
        Other = 5
    }
}