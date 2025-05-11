using System;
using SkillsSystem;

namespace Configs.SkillsConfig
{
    [Serializable]
    public struct SkillDataByLevel
    {
        public int Level;
        public float Value;
        public SkillTrigger Trigger;
        public float TriggerValue;
    }
}