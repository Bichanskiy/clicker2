using Configs.SkillsConfig;

public abstract class Skill
{
    public virtual void Initialize(SkillScope scope, SkillDataByLevel skillData)
    {
        
    }
    
    public virtual void OnSkillRegistered(){}

    public virtual void SkillProcess(){}
    
}