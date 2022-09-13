using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeHub.ClassLib.Model
{
    [Owned]
    public class Report
    {
        public bool BlockApproved { get; set; }
        public bool AdminConfirmed { get; set; }
    }
}
