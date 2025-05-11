using Configs.LevelConfig;
using Health_bar;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour 
    { 
        [SerializeField] private Transform _enemyContainer; 
        [SerializeField] private EnemyConfig _enemiesConfig; 
        private EnemyData _currentEnemyData; 
        private Enemy _currentEnemy; 
        private HealthBar _healthBar; 
        private Timer.Timer _timer; 
        private LevelData _levelData;
        private int _currentEnemyIndex;

        public event UnityAction<bool> OnLevelPassed;
        public void Initialize(HealthBar healthBar, Timer.Timer timer) 
        { 
            _timer = timer; 
            _healthBar = healthBar;
        }
        private void SpawnEnemy()
        {
            _currentEnemyIndex++;

            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];

            if (currentEnemy.IsBoss)
            {
                _timer.Initialize(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            _currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id);
            _timer.SetActive(currentEnemy.IsBoss);
            InitHpBar(currentEnemy.Hp);
        
            _currentEnemy.Initialize(_currentEnemyData, currentEnemy.Hp); 
        }
    
        public void DamageCurrentEnemy(float damage) 
        { 
            _currentEnemy.DoDamage(damage);
        } 
    
        private void HandleEnemyDeath() 
        { 
            OnLevelPassed?.Invoke(false);
        }
    
        public void StartLevel(LevelData levelData) 
        { 
            _levelData = levelData;
            _currentEnemyIndex = -1;
            if (_currentEnemy == null) 
            { 
                _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemy.OnDead += () => SpawnEnemy();
                _currentEnemy.OnDamaged += _healthBar.DecreaseValue; 
            }
            SpawnEnemy();
        }

        private void InitHpBar(float Health)
        {
            _healthBar.ShowHpBar(); 
            _healthBar.SetMaxValue(Health);
        }
    
        
    }
}
    

