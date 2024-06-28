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
        public virtual void SpeedChange() { }

        //private bool Destroyable(string Collected)
        //{
        //    string[] destroyable = { "Coin", "Arrow", "Key" };
        //    Collected = Collected.Remove(Collected.IndexOf('_'), Collected.Length);
        //    return Array.Exists(destroyable, element => element == Collected);
        //}
        //public enum CollectableObject
        //{
        //    Coin = 1,
        //    Arrow = 2,
        //    Key = 3,
        //    Other = 4
        //}
    }
}