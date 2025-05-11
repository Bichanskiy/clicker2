using Configs.SkillsConfig;
using Enemy;

public class AdditionalDamageSkill : Skill
{
    private EnemyManager _enemyManager;
    private SkillDataByLevel _skillData;
    public override void Initialize(SkillScope scope, SkillDataByLevel skillData)
    {
        _skillData = skillData;
        _enemyManager = scope._enemyManager;
    }

    public override void SkillProcess()
    {
        _enemyManager.DamageCurrentEnemy(_skillData.TriggerValue);
    }
}
