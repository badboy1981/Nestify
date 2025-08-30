using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
//using static ChargeStationEvent;
public class ChargeStationEventTests
{
    [Test]
    public void ChargeStatus_SetSameValue_DoesNotInvokeEventAgain()
    {
        var chargeEvent = new ChargeStationEvent()
        {
            ChargeStatus = ChargeStationEvent.ChargeStationState.Empty // مقدار اولیه متفاوت
        };

        int invokeCount = 0;
        chargeEvent.OnStatusChanged += (state) => invokeCount++;

        chargeEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.Charging; // تغییر واقعی
        chargeEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.Charging; // مقدار تکراری

        Assert.AreEqual(1, invokeCount); // فقط یک بار باید اجرا بشه
    }

    [Test]
    public void ChargeStatus_SetDifferentValues_InvokesEventEachTime()
    {
        var chargeEvent = new ChargeStationEvent();
        int invokeCount = 0;

        chargeEvent.OnStatusChanged += (state) => invokeCount++;

        chargeEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.Empty;
        chargeEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.Charging;
        chargeEvent.ChargeStatus = ChargeStationEvent.ChargeStationState.FullyCharged;

        Assert.AreEqual(3, invokeCount);
    }
}