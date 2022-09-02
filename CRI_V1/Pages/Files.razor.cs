﻿using CRI_V1.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static CRI_V1.Data.CRIModel;
using CRI_V1.Data;

namespace CRI_V1.Pages
{
    public partial class Files
    {
        [Parameter]
        public string name { get; set; }
        [Parameter]
        public string project { get; set; }

        private static readonly HttpClient httpClient = new();
        public static String repositoryName;
        public static String repositoryproject;
        public static Boolean show = false ;
        public static CRITabList CRIList { get; set; } = new();
        public CRIModel.Tabs ActiveTab { get; set; } = new();
        public static List<CRIModel.Tabs> Tabs { get; set; } = new();
        private List<MarkupString> MarkupContents = new();

        protected override async Task OnInitializedAsync()
        {
            repositoryName = name;
            repositoryproject = project;
            await TestAsync();
        }
        public async Task TestAsync()
        {
            //await GetFilesFromCRI(GenerateFileUrl("/.criconfig.json"));
            await GetFilesFromCRI(GenerateFileUrl("/.criconfig.json"));
        }

        private static string GenerateFileUrl(string filePath)
        {
            return $"https://raw.githubusercontent.com/{repositoryName}/{repositoryproject}/main{filePath}";
        }

        public async Task GetFilesFromCRI(string url)
        {
            CRIList.Tabs.Clear();
            var tempResult = await GetFileContent(url);
            CRIList = JsonConvert.DeserializeObject<CRITabList>(tempResult);
            Console.WriteLine("this is sparta ! "+CRIList);
        }

        private static async Task<string> GetFileContent(string url)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MyApplication", "1"));
            return await httpClient.GetStringAsync(url);
        }

        public async Task FillCRIContent(List<string> Paths)
        {
            ActiveTab = CRIList.Tabs.First(key => key.Path == Paths);
            //filling contents if they arent filled
            if (ActiveTab.Contents.Count == 0)
                foreach (string path in Paths)
                {
                    var temp = await GetFileContent(GenerateFileUrl(path));
                    ActiveTab.Contents.Add(temp);
                }

            DemoContentToHtml(ActiveTab.Contents);
            this.StateHasChanged();
        }

        public void DemoContentToHtml(List<string> contents)
        {
            MarkupContents.Clear();

            foreach (string content in contents)
            {
                MarkupContents.Add((MarkupString)Markdig.Markdown.ToHtml(content ?? ""));
            }
        }
    }

}
