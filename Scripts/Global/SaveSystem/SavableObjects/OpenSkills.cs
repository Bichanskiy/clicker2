using System.Collections.Generic;
using Global.SaveSystem;

public class OpenedSkills : ISavable
{
    public List<SkillWithLevel> Skills = new(){new SkillWithLevel{Id = "AdditionalDamageSkill", Level = 1}};
}