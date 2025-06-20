using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public event UnityAction<float> OnDamaged;
        public event UnityAction OnDead;
        private float _health;

        public void Initialize(EnemyData enemyData, float health)
        {
            _health = health; 
            _image.sprite = enemyData.Sprite;
        }

        public void DoDamage(float damage)
        {
            if (damage >= _health)
            {
                _health = 0;
                
                OnDamaged?.Invoke(damage);
                OnDead?.Invoke();
                return;
            }
            
            _health -= damage;
            OnDamaged?.Invoke(damage);
        }

        public float GetHealth()
        {
            return _health;
        }
    }
}
