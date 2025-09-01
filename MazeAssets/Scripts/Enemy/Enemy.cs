using UnityEngine;
using Assets.MazeAssets.Scripts.Parent;

internal class Enemy : Parent
{
    [SerializeField] protected EnemyList Enemies; // "ShadowBot" or "BulkBot"
    [SerializeField] protected ChargeManagment _ChargeManagment;
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Enemy: Enter trigger with {other.gameObject.name}");
            PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log($"Enemy: Exited trigger with {other.gameObject.name}");
            StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
        }
    }
    protected int HandleChargeSteal(EnemyList enemyTag)
    {
        return enemyTag switch
        {
            EnemyList.ShadowBot => (int)EnemyList.ShadowBot,
            EnemyList.BulkBot => (int)EnemyList.BulkBot,
            _ => 0,
        };
    }
    
    private void Temp()
    {
        //float shadowBotTheftRate = chargeManagment.ChargeTheftRates.ShadowBot;
        //chargeManagment.UpdateVoltCharge(-shadowBotTheftRate);
        //Debug.Log($"ShadowBot stole {shadowBotTheftRate} charge from Volt. Current VoltChargeLevel: {chargeManagment.ChargeVoltStatus.VoltChargeLevel}");
        if (chargeManagment.ChargeVoltStatus.VoltChargeLevel <= 0)
        {
            Debug.Log("Volt's charge is depleted!");
        }
    }
}