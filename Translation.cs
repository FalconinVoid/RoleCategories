using Exiled.API.Interfaces;

//RoleCategories © 2025 by FalconinVoid is licensed under CC BY-ND 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by-nd/4.0/

namespace RoleCategories
{
    public class Translation : ITranslation
    {
        public string SubcommandMissing { get; set; } = "Specify a subcommand: listcategories/lc, listroles/lr, give/g";
        public string RoundNotStarted { get; set; } = "Round not started.";
        public string NoPermissions { get; set; } = "No permissions! Contact an administrator for help!";
        public string ListingRoles { get; set; } = "Listing roles for category";
        public string NotFound { get; set; } = "!ERROR: NOT FOUND!";
        public string CategoryNotFound { get; set; } = "Category not found!";
        public string DefineCategory { get; set; } = "Missing arguments! Define category.";
        public string ChangedRoleAdmin { get; set; } = "Players got changed to role";
        public string ChangedRole { get; set; } = "Set your role to";
        public string Arguments { get; set; } = "Missing arguments: <role Name / role ID>";
        public string AvaiableCategories { get; set; } = "Avaiable Categories";
    }
}
