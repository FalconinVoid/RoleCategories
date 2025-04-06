using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using UncomplicatedCustomRoles.API.Features;
using UncomplicatedCustomRoles.API.Interfaces;
using UncomplicatedCustomRoles.Extensions;

//RoleCategories © 2025 by FalconinVoid is licensed under CC BY-ND 4.0. To view a copy of this license, visit https://creativecommons.org/licenses/by-nd/4.0/

namespace RoleCategories
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class CommandHandler : ICommand
    {
        public string Command { get; } = "serverroles";
        public string[] Aliases { get; } = new string[] { "srvr", "srvroles" };
        public string Description { get; } = "Short srvr or srvroles.";
        private readonly Translation translation = Plugin.Instance.Translation;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"{translation.SubcommandMissing}: listcategories/lc, listroles/lr, give/g";
            if (!arguments.IsEmpty())
            {
                bool success;
                switch (arguments.FirstElement())
                {
                    case "listcategories":
                    case "lc":
                        if (PluginPermissionsManager.HasPermission(sender, PluginPermissions.Basic))
                        {
                            response = ListCategories();
                            return true;
                        }
                        break;
                    case "listroles":
                    case "lr":
                        if (PluginPermissionsManager.HasPermission(sender, PluginPermissions.Basic))
                        {
                            response = ListRoles(arguments, out success);
                            return success;
                        }
                        break;
                    case "give":
                    case "g":
                        if (PluginPermissionsManager.HasPermission(sender, PluginPermissions.Admin))
                        {
                            response = GiveAdmin(arguments, out success);
                            return success;
                        }
                        else
                        {
                            if (Round.IsLobby)
                            {
                                response = translation.RoundNotStarted;
                                return false;
                            }
                            response = Give(sender, arguments, out success);
                            return success;
                        }
                }
                response = translation.NoPermissions;
            }
            return false;
        }
        public string ListCategories()
        {
            StringBuilder builder = new StringBuilder();
            List<string> categoriesUpper = new List<string>();
            builder.AppendLine($"{translation.AvaiableCategories}:");
            foreach (string category in Plugin.Instance.Config.RoleCategory.Values)
            {
                if (!categoriesUpper.Contains(category.ToUpper()))
                {
                    categoriesUpper.Add(category.ToUpper());
                }
            }
            foreach (string category in categoriesUpper)
            {
                builder.AppendLine(category);
            }
            return builder.ToString();
        }
        public string ListRoles(ArraySegment<string> arguments, out bool success)
        {
            success = false;
            StringBuilder builder = new StringBuilder();
            if (arguments.Count >= 2)
            {
                builder.AppendLine($"{translation.ListingRoles} {arguments.Array[2].ToUpper()}:");
                List<int> roles = Plugin.Instance.Config.RoleCategory.Where(pair => pair.Value.ToUpper() == arguments.Array[2].ToUpper()).Select(pair => pair.Key).ToList();
                if (roles.Count > 0)
                {
                    success = true;
                    foreach (int role in roles)
                    {
                        if (CustomRole.TryGet(role, out ICustomRole customRole))
                        {
                            builder.AppendLine($"[{role}] {customRole.Name}");
                        }
                        else
                        {
                            builder.AppendLine($"[{role}] {translation.NotFound}");
                        }
                    }
                }
                else
                {
                    builder.AppendLine(translation.CategoryNotFound);
                }
            }
            else
            {
                builder.AppendLine(translation.DefineCategory);
                builder.AppendLine(ListCategories());
            }
            return builder.ToString();
        }
        public string GiveAdmin(ArraySegment<string> arguments, out bool success)
        {
            string response;
            success = false;
            if (arguments.Count >= 2)
            {
                ICustomRole customRole = GetCustomRole(arguments.Array[2]);
                if (customRole != null)
                {
                    StringBuilder builder = new StringBuilder($"{translation.ChangedRoleAdmin} {customRole.Name}:");
                    Player[] players = GetPlayers(arguments);
                    if (players.Count() > 0)
                    {
                        foreach (Player player in players)
                        {
                            if (!player.IsHost)
                            {
                                builder.Append($" {player.DisplayNickname}");
                                player.SetCustomRole(customRole);
                            }
                        }
                        response = builder.ToString();
                        success = true;
                    }
                    else
                    {
                        response = translation.NotFound;
                    }
                }
                else
                {
                    response = translation.NotFound;
                }
            }
            else
            {
                response = $"{translation.Arguments}: <role Name / role ID> <player nicknames / player IDs>";
            }
            return response;
        }
        public string Give(ICommandSender sender, ArraySegment<string> arguments, out bool success)
        {
            string response;
            success = false;
            if (arguments.Count >= 2)
            {
                ICustomRole customRole = GetCustomRole(arguments.Array[2]);
                if (customRole != null)
                {
                    if (!Permissions.CheckPermission(sender, $"falcon.customroles.{Plugin.Instance.Config.RoleCategory[customRole.Id].ToLower()}"))
                    {
                        response = translation.NoPermissions;
                    }
                    else
                    {
                        response = $"{translation.ChangedRole} {customRole.Name}";
                        Player.Get(sender).SetCustomRole(customRole);
                        success = true;
                    }
                }
                else
                {
                    response = translation.NotFound;
                }
            }
            else
            {
                response = $"{translation.Arguments}:  <role Name / role ID>";
            }
            return response;
        }
        private ICustomRole GetCustomRole(string argument)
        {
            foreach (ICustomRole customRole in CustomRole.List)
            {
                int.TryParse(argument, out int customRoleId);
                if (customRole.Name.ToUpper() == argument.ToUpper() || customRole.Id == customRoleId)
                {
                    return customRole;
                }
            }
            return null;
        }
        private Player[] GetPlayers(ArraySegment<string> arguments)
        {
            List<Player> players = new List<Player>();
            foreach (string argument in arguments.Array.ToList())
            {
                int.TryParse(argument, out int playerId);
                if (Player.TryGet(argument, out Player player))
                {
                    players.Add(player);
                }
                else if (Player.TryGet(playerId, out Player playerAlt))
                {
                    players.Add(playerAlt);
                }
            }
            return players.ToArray();
        }
    }
}