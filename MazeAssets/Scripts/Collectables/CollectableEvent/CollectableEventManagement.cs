using UnityEngine;

namespace Collectable
{
    [CreateAssetMenu(fileName = "CollectableEventManagement", menuName = "Collectable/Event/Collectable Event Management")]

    public class CollectableEventManagement : ScriptableObject
    {
        public CollectableEvent collectableEvent;
    }
}