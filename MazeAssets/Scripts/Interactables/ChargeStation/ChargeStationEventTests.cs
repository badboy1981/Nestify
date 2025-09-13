//using NUnit.Framework;
//using static ChargeStationStateEnum;
//public class ChargeStationEventTests
//{
//    [Test]
//    public void ChargeStatus_SetSameValue_DoesNotInvokeEventAgain()
//    {
//        var chargeEvent = new ChargeStationEvent()
//        {
//            ChargeStatus = ChargeStationStateEnum.Empty // مقدار اولیه متفاوت
//        };

//        int invokeCount = 0;
//        chargeEvent.OnStatusChanged += (state) => invokeCount++;

//        chargeEvent.ChargeStatus = ChargeStationStateEnum.Charging; // تغییر واقعی
//        chargeEvent.ChargeStatus = ChargeStationStateEnum.Charging; // مقدار تکراری

//        Assert.AreEqual(1, invokeCount); // فقط یک بار باید اجرا بشه
//    }

//    [Test]
//    public void ChargeStatus_SetDifferentValues_InvokesEventEachTime()
//    {
//        var chargeEvent = new ChargeStationEvent();
//        int invokeCount = 0;

//        chargeEvent.OnStatusChanged += (state) => invokeCount++;

//        chargeEvent.ChargeStatus = ChargeStationStateEnum.Empty;
//        chargeEvent.ChargeStatus = ChargeStationStateEnum.Charging;
//        chargeEvent.ChargeStatus = ChargeStationStateEnum.FullyCharged;

//        Assert.AreEqual(3, invokeCount);
//    }
//}