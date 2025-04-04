using System.Collections.Generic;

using Exiled.API.Interfaces;

namespace RoleCategories
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public Dictionary<int, string> RoleCategory { get; set; } = new Dictionary<int, string>
        {
            { 1, "ExampleCategory" },
            { 2, "examplecategory"}
        };
    }
}
