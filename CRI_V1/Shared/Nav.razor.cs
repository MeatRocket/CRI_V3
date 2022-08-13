using CRI_V1.Data;
using CRI_V1.Pages;
using Microsoft.AspNetCore.Components;

namespace CRI_V1.Shared
{
    public partial class Nav
    {
        [Parameter]
        public bool show { get; set; }

        [Parameter]
        public CRI ActiveCRI { get; set; }

        public static async Task<string> Call(string path)
        {
            Files f = new();
            string s= await f.FillCRIContent(path);
            return s;
        }
    }
}
