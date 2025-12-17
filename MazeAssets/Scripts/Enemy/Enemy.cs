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
        if (!other.CompareTag("Player")) return;
        //Debug.Log($"Enemy: Enter trigger with {other.gameObject.name}");
        PlaySoundByList(PrefabAudioLibrary.SoundCategoryLists);
        _ChargeManagment.ChargeModifier(tag);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //Debug.Log($"Enemy: Exited trigger with {other.gameObject.name}");
        StopSoundByList(PrefabAudioLibrary.SoundCategoryLists);
    }
    protected EnemyList GetEnemyTypeValue(EnemyList enemy)
    {
        return enemy switch
        {
            EnemyList.ShadowBot => EnemyList.ShadowBot,
            EnemyList.BulkBot => EnemyList.BulkBot,
            _ => 0
        };
    }
    protected void DecreaseStealCharge(EnemyList enemy)
    {
        _ChargeManagment.CVStatus.VoltChargeLevel -= (int)GetEnemyTypeValue(enemy);
        _ChargeManagment.CVStatus.VoltChargeLevel =
            Mathf.Clamp(_ChargeManagment.CVStatus.VoltChargeLevel,
            0,
            _ChargeManagment.CVStatus.MaxVoltCharge);
    }
}