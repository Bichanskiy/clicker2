using System;
using System.Collections.Generic;
using Configs.SkillsConfig;
using Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace SkillsSystem
{
    public class SkillSystem
    {
        private SkillScope _scope;
        private SkillsConfig _skillsConfig;
        
        private Dictionary<SkillTrigger, List<Skill>> _skillsByTrigger;
        
        
        
        public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager)
        {
            var _scope = new SkillScope();
            var _skillsConfig = new SkillsConfig();
            _scope = new()
            {
                _enemyManager = enemyManager
            };
            _skillsByTrigger = new();
            
            foreach (var skill in openedSkills.Skills)
            {
                RegisterSkill(skill);
            }
        }

        public void InvokeTrigger(SkillTrigger trigger)
        {
            if (!_skillsByTrigger.ContainsKey(trigger)) return;
            
            var skillsToActivate = _skillsByTrigger[trigger];
            
            foreach (var skill in skillsToActivate)
            {
                skill.SkillProcess();
            }
        }

        public void RegisterSkill(SkillWithLevel skill)
        {
            var skillData = _skillsConfig.GetSkillDataByLevel(skill.Id, skill.Level);

            var skillType = Type.GetType($"Game.Skills.{skill.Id}");
            if (skillType == null)
            {
                throw new Exception($"Skill with {skill.Id} not found");
            }

            var skillInstance = Activator.CreateInstance(skillType) as Skill;
            if (skillInstance == null)
            {
                throw new Exception($"can not create skill with id {skill.Id}");
            }
            
            skillInstance.Initialize(_scope, skillData);

            if (!_skillsByTrigger.ContainsKey(skillData.Trigger))
            {
                _skillsByTrigger[skillData.Trigger] = new();
            }
            
            _skillsByTrigger[skillData.Trigger].Add(skillInstance);
            skillInstance.OnSkillRegistered();
        }
    }
}