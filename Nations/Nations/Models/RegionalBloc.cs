﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nations.Models
{
    using System.Collections.Generic;

    public class RegionalBloc
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public List<string> OtherAcronyms { get; set; }
        public List<string> OtherNames { get; set; }
    }
}
