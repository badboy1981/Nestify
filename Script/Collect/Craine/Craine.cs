using UnityEngine;

public class Craine : MonoBehaviour
{
    [SerializeField] CrainKeySB KeyCollected;
    [SerializeField] Animator CraineAnimation;

    //Craine Name: A_Craine
    //Craine Key Name: A_CraineKey

    private void Start()
    {
        KeyCollected.KeysName.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (KeyCollected.KeysName.Contains($"{name}Key"))
            {
                Debug.Log($"Yoy have a key: {name}Key");
                CraineAnimation.SetBool("CheckKey", true);
            }
            else
            {
                Debug.Log($"You need key! {name}");
            }
        }
        else
        {
            Debug.Log($"OutSide: {other.name}");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CraineAnimation.SetBool("CheckKey", false);
    }
}