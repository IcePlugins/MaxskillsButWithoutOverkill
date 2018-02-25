using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxskillsButWithoutOverkill
{
    public class CommandMaxskills : IRocketCommand
    {
        #region Properties

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "maxskills";

        public string Help => "Gives a player maxskills.";

        public string Syntax => "/mxaskills";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "maxskills" };

        #endregion

        public void Execute(IRocketPlayer caller, string[] command)
        {
            MaxskillsButWithoutOverkill.instance.GrantSkills((UnturnedPlayer)caller);
            UnturnedChat.Say(caller, "You've been granted maxskills.");
        }
    }
}
