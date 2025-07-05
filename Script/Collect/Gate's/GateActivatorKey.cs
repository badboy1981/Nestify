using System.Linq;
using UnityEngine;

namespace Collectable
{
    public class GateActivatorKey : Collectable
    {
        [SerializeField] Gate.StoneHatchKeyListRef KeysList;
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            ChangeBoolByName();
        }
        private void ChangeBoolByName()
        {
            KeysList.GatesPropertyList.Find(g => g.SignLabel == name.ElementAt(3).ToString()).keysLists.Find(j => j.KeyName == name).Collected = true;
        }
        private void ChangeBoolByTag()
        {
            KeysList.GatesPropertyList.Find(g => g.SignLabel == tag.Last<char>().ToString());
        }
    }
}