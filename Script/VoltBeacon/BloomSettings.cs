namespace TelePort
{
    [System.Serializable]
    public struct BloomSettings
    {
        public float intensity;
        public float threshold;

        public BloomSettings(float intensity, float threshold)
        {
            this.intensity = intensity;
            this.threshold = threshold;
        }
    }
}