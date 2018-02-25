using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using System.Reflection;
using Rocket.Unturned.Skills;
using UnityEngine;
using Rocket.Unturned.Events;
using Rocket.Unturned;
using Logger = Rocket.Core.Logging.Logger;

namespace MaxskillsButWithoutOverkill
{
    public class MaxskillsButWithoutOverkill : RocketPlugin<MaxskillsButWithoutOverkillConfiguration>
    {
        public static MaxskillsButWithoutOverkill instance;

        private FieldInfo[] skills;
        private ConstructorInfo skillCtor;

        protected override void Load()
        {
            instance = this;

            Logger.Log("Loading MaxskillsButWithoutOverkill. Skills that will not be maxskilled:\n");

            foreach (string s in Configuration.Instance.ignoreTheseSkills)
                Logger.Log("\t" + s + "\n");

            skills = typeof(UnturnedSkill).GetFields(BindingFlags.Static | BindingFlags.Public);

            skillCtor = typeof(UnturnedSkill).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
                new Type[] { typeof(byte), typeof(byte) }, null);

            if (Configuration.Instance.enableMaxskillsByDefault)
            {
                UnturnedPlayerEvents.OnPlayerRevive += OnRevive;
                U.Events.OnPlayerConnected += OnConnect;
            }
        }

        protected override void Unload()
        {
            if (Configuration.Instance.enableMaxskillsByDefault)
            {
                UnturnedPlayerEvents.OnPlayerRevive -= OnRevive;
                U.Events.OnPlayerConnected -= OnConnect;
            }
        }

        private void OnRevive(UnturnedPlayer p, Vector3 pos, byte a) => GrantSkills(p);
        private void OnConnect(UnturnedPlayer p) => GrantSkills(p);

        public void GrantSkills(UnturnedPlayer p)
        {
            p.MaxSkills();

            foreach (FieldInfo x in skills)
                if (Configuration.Instance.ignoreTheseSkills.Contains(x.Name))
                {
                    UnturnedSkill s = (UnturnedSkill)x.GetValue(null);
                    p.SetSkillLevel(s, 0);
                }
        }
    }
}
