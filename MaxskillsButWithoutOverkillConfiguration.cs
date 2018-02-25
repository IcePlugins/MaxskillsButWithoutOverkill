using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxskillsButWithoutOverkill
{
    public class MaxskillsButWithoutOverkillConfiguration : IRocketPluginConfiguration
    {
        public List<string> ignoreTheseSkills;
        public bool enableMaxskillsByDefault;

        public void LoadDefaults()
        {
            ignoreTheseSkills = new List<string>() { "Overkill" };
            enableMaxskillsByDefault = true;
        }
    }
}
