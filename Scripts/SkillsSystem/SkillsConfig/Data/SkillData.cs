using System;
using System.Collections.Generic;

namespace Configs.SkillsConfig
{
    [Serializable]
    public struct SkillData
    {
        public string SkillId;
        public List<SkillDataByLevel> SkillsLevel;
    }
}