using UnityEngine;

namespace Collectable
{
    public class Collectable : MonoBehaviour
    {
        public virtual void Collect()
        {
            Destroy(gameObject);
            Debug.Log("collected!");
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