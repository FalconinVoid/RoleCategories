using CommandSystem;
using Exiled.Permissions.Extensions;

namespace RoleCategories
{
    public enum PluginPermissions
    {
        Basic,
        Give,
        Admin
    }
    public static class PluginPermissionsManager
    {
        public static bool HasPermission(ICommandSender sender, PluginPermissions permission)
        {
            switch (permission)
            {
                case PluginPermissions.Basic:
                    return Permissions.CheckPermission(sender, "falcon.customroles.basic") || Permissions.CheckPermission(sender, "falcon.customroles.give") || Permissions.CheckPermission(sender, "falcon.customroles.admin");
                case PluginPermissions.Give:
                    return Permissions.CheckPermission(sender, "falcon.customroles.give" ) || Permissions.CheckPermission(sender, "falcon.customroles.admin");
                case PluginPermissions.Admin:
                    return Permissions.CheckPermission(sender, "falcon.customroles.admin");
            }
            return false;
        }
    }
}
