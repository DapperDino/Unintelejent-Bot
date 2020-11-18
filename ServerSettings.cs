using System.Collections.Generic;

namespace UnintelejentBot
{
    public record ServerSettings(ulong RolesReactMessageId, Dictionary<ulong, ulong> EmojiRoles);
}
