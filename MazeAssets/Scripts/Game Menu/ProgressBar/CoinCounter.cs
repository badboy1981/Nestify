
namespace GameMenu
{
    internal class CoinCounter : ProgressBar
    {
        private void OnEnable()
        {
            collectetedManagement.collectableEvent.OnCoinCollectedEvent += OnCoinCollectedEvent;
        }
        private void OnDisable()
        {
            collectetedManagement.collectableEvent.OnCoinCollectedEvent -= OnCoinCollectedEvent;
        }

        private void OnCoinCollectedEvent()
        {
            itemCounterText.text = $"{itemCounter++}";
            LevelData.CoinCounter = itemCounter;
        }
    }
}