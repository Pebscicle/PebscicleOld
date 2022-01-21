using Microsoft.AspNetCore.Components;
using PebMaps.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PebMaps.Data
{
    public class State
    {
        [Inject] StateService stateService { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
    }
}
