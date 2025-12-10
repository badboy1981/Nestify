using System;
using UnityEngine.Events;

[Serializable]
public class KeyCollectedEvent
{
    //[SerializeField] KeyProperty keyProperty;
    //private GameEntityProperty keyProperty;

    //public UnityAction<KeyProperty> OnKeyGet;
    public UnityAction<GameEntityProperty> OnCollectedKey;

    public GameEntityProperty CollectedKey
    {
        //get => ;
        set
        {
            //keyProperty = value;
            OnCollectedKey?.Invoke(value);
        }

        //public KeyProperty KeyProperty
        //{
        //    get => keyProperty;
        //    set
        //    {
        //        if (keyProperty.ID == value.ID)
        //            return;
        //        keyProperty = value;
        //        OnKeyGet?.Invoke(keyProperty);
        //    }
        //}
    }
}