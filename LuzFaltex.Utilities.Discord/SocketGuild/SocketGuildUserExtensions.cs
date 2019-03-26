using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuzFaltex.Utilities.Discord.SocketGuild
{
    public static class SocketGuildUserExtensions
    {
        public static bool IsMemberOf(this SocketGuildUser user, ulong roleId)
            => IsMemberOf(user, user.Guild.GetRole(roleId));
        public static bool IsMemberOf(this SocketGuildUser user, IRole role)
            => user.Roles.Contains(role);
    }
}
