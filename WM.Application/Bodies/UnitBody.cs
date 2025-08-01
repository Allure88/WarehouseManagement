using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Domain.Entities;

namespace WM.Application.Bodies
{
    public class UnitBody
    {
        public string UnitDescription { get; set; } = string.Empty;
        public StateBody State { get; set; }
    }
}
