using UnityEngine;

public class VoltSound : MonoBehaviour
{
    [SerializeField] MyInputInit _InputControl;

    [SerializeField] private AudioLibrary PrefabAudioLibrary; // Audio library for Volt sounds
    [SerializeField] private SoundData soundDataEvant; // مرجع به SoundData
    [SerializeField] float fadeDuration = 5f; // مدت زمان برای افزایش/کاهش ولوم

    private bool isMoving;
    private bool isStarting;


    private void Awake()
    {
        Debug.Log($"Volt: Awake called on {gameObject.name}");
        if (soundDataEvant == null)
        {
            Debug.LogError($"Volt: soundData is null on {gameObject.name}");
        }
        if (PrefabAudioLibrary == null)
        {
            Debug.LogError($"Volt: PrefabAudioLibrary is null on {gameObject.name}");
        }
    }
    private void Start()
    {
        _InputControl.MoveEventStart += StartMoving;
        _InputControl.MoveEventCanceled += StopMoving;
    }
    //private void PlaySoundTest(Vector2 MoveValue)
    //{

    //}
    // فرض می‌کنیم این متد وقتی حرکت شروع می‌شه صدا زده می‌شه (مثلاً با ورودی بازیکن)
    public void StartMoving(Vector2 MoveValue)
    {
        Debug.Log($"Volt: StartMoving called with MoveValue: {MoveValue}");
        if (!isStarting && !isMoving)
        {
            Debug.Log("Volt: Starting movement");
            isStarting = true;
            soundDataEvant?.Play("Volt_Sound", PrefabAudioLibrary);
            soundDataEvant?.SetVolume("Volt_Sound", 0f, 1f, fadeDuration, true); // ولوم از 0 به 1 در fadeDuration ثانیه
            isStarting = false;
            isMoving = true;
        }
    }

    public void StopMoving(Vector2 MoveValue)
    {
        if (isMoving)
        {
            Debug.Log("Volt: Stopping movement");
            soundDataEvant?.SetVolume("Volt_Sound", 1f, 0f, fadeDuration, true); // توقف بعد از کاهش ولوم
            isMoving = false;
        }
    }

    // فرض می‌کنیم این متد وقتی حرکت متوقف می‌شه صدا زده می‌شه
    //public void StopMoving(Vector2 MoveValue)
    //{
    //    Debug.Log($"Volt: StopMoving called with MoveValue: {MoveValue}");
    //    //اگر در حال حرکت هستیم، صدا را متوقف می‌کنیم
    //    // و ولوم را به آرامی کاهش می‌دهیم
    //    // اگر در حال شروع حرکت هستیم، فقط ولوم را کاهش می‌دهیم
    //    if (isMoving)
    //    {
    //        Debug.Log("Volt: Stopping movement");
    //        soundDataEvant?.SetVolume("Volt_Sound", 1f, 0f, fadeDuration,true); // ولوم از 1 به 0 در fadeDuration ثانیه
    //        soundDataEvant?.StopSound("Volt_Sound"); // توقف بعد از کاهش ولوم
    //        isMoving = false;
    //    }
    //}
}