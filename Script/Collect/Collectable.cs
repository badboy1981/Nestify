using UnityEngine;

namespace Collectable
{
    //public interface ICollectable
    //{
    //    public void Collect();
    //    public void SpeedChange();
    //}
    public class Collectable : MonoBehaviour
    {
        public virtual void Collect()
        {
            Destroy(gameObject);
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
        public virtual void SpeedChange()
        {

        }
    }
}