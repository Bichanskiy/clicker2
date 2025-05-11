using UnityEngine;
using UnityEngine.Events;

namespace Click_Button
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private ClickButton _clickButton;
        [SerializeField] private ClickButtonConfig _buttonConfig;

        public event UnityAction OnClicked;
        public void Initialize()
        {
            _clickButton.Initialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke()); // Подписываем кнопку на клик
        }

    }
}