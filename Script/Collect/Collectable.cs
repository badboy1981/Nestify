using UnityEngine;
using UnityEngine.Events;

namespace Collectable
{
    //public interface ICollectable
    //{
    //    public void Collect();
    //    public void SpeedChange();
    //}
    public class Collectable : MonoBehaviour
    {
        //private static int TotlaNumber = 1;
        public virtual void Collect()
        {
            Destroy(gameObject);
        }
        public virtual void SpeedChange() { }

        public enum CollectableObject
        {
            Coin = 1,
            Arrow = 2,
            Key = 3,
            Other = 4
        }
    }
}