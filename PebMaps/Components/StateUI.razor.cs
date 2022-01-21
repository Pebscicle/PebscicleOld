using Microsoft.AspNetCore.Components;
using PebMaps.Data;

namespace PebMaps.Components
{
    public partial class StateUI
    {

        [Parameter] public string Id { get; set; }
        [Parameter] public string Path { get; set; }
        [Parameter] public State Data { get; set; }
        public string BackgroundColor 
        {
            get;
            set;
        }

        private static string Style { get; set; } = "cursor:pointer;";

        protected override void OnInitialized()
        {
            //SetupColors();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                //SetupColors();
            }
        }

    }
}