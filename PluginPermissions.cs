using CommandSystem;
using Exiled.Permissions.Extensions;

//RoleCategories © 2025 by FalconinVoid is licensed under CC BY-ND 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by-nd/4.0/

namespace RoleCategories
{
    public enum PluginPermissions
    {
        Basic,
        Admin
    }
    public static class PluginPermissionsManager
    {
        public static bool HasPermission(ICommandSender sender, PluginPermissions permission)
        {
            switch (permission)
            {
                case PluginPermissions.Basic:
                    return Permissions.CheckPermission(sender, "falcon.customroles.basic") || Permissions.CheckPermission(sender, "falcon.customroles.admin");
                case PluginPermissions.Admin:
                    return Permissions.CheckPermission(sender, "falcon.customroles.admin");
            }
            return false;
        }
    }
}
