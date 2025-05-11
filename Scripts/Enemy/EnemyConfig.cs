using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName="Configs/EnemyConfig", fileName="EnemyConfig")] 
    public class EnemyConfig : ScriptableObject
    {
        public List<EnemyData> Enemies;
        public Enemy EnemyPrefab;

        public EnemyData GetEnemy(string id)
        {
            foreach (var enemyData in Enemies)
            {
                if (enemyData.Id == id) return enemyData;
            }
        
            Debug.LogError($"Not found enemy with id {id}");
            return default;
        }
    }
}