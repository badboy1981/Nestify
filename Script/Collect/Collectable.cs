using System;
using System.Linq;
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
        public virtual void Collect()
        {
            Destroy(gameObject);
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
        public virtual void SpeedChange()
        {

        }
    }
}