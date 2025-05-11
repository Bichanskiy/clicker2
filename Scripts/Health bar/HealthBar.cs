using UnityEngine;
using UnityEngine.UI;

namespace Health_bar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void ShowHpBar()
        {
            gameObject.SetActive(true);
        }

        public void HideHpBar()
        {
            gameObject.SetActive(false);
        }
        public void SetMaxValue(float value)
        {
            _slider.maxValue = value;
            _slider.value = value;
        }

        public void DecreaseValue(float value)
        {
            _slider.value -= value;
        }
    }
}