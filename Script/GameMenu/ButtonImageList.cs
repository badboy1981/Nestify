using UnityEngine;

namespace GameMenu
{
    [CreateAssetMenu(fileName = "MenuButtonImageList", menuName = "My Asset/MenuButtonImageList")]

    public class ButtonImageList : ScriptableObject
    {
        public Sprite NewGameSprite;
        public Sprite ContinueSprite;
        public Sprite OptionSprite;
        public Sprite ExitSprite;
        public Sprite CrossSprite;
        public Sprite ButtonLevelNumber;
    }
}