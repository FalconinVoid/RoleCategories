using System.Collections.Generic;

using Exiled.API.Interfaces;

//RoleCategories © 2025 by FalconinVoid is licensed under CC BY-ND 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by-nd/4.0/

namespace RoleCategories
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public Dictionary<int, string> RoleCategory { get; set; } = new Dictionary<int, string>
        {
            { 1, "ExampleCategory" }
        };
    }
}
