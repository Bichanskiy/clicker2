using UnityEngine;
using UnityEngine.UI;

namespace Click_Button
{
    [CreateAssetMenu(menuName="Configs/ClickButtonConfig", fileName="ClickButtonConfig")]
    public class ClickButtonConfig : ScriptableObject
    {
        public Sprite DefaultSprite;

        public ColorBlock ButtonColors;
        
    }
}
    