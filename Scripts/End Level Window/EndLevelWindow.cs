using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace End_Level_Window
{
    public class EndLevelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _loseLevelWindow;
        [SerializeField] private GameObject _winLevelWindow;

        [SerializeField] private Button _loseRestartButton;
        [SerializeField] private Button _winRestartButton;
        public event UnityAction OnRestartClicked;

        public void Initialize()
        {
            _loseRestartButton.onClick.AddListener(Restart);
            _winRestartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            OnRestartClicked?.Invoke();
            gameObject.SetActive(false);
        }

        public void ShowLoseWindow()
        {
            _loseLevelWindow.SetActive(true);
            _winLevelWindow.SetActive(false);
            gameObject.SetActive(true);
        }

        public void ShowWinWindow()
        {
            _winLevelWindow.SetActive(true);
            _loseLevelWindow.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
