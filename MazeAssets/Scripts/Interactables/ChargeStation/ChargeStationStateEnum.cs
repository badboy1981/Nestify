
public enum ChargeStationStateEnum
{
    Idle,         // Station is powered on but no Volt is connected
    Available,    // Station has charge available and is ready to supply Volt
    Charging,     // Station is actively charging Volt
    CoolingDown,  // Station is temporarily resting after charging or heavy use
    Empty,        // Station has no charge left to supply
    Disabled,     // Station is offline, broken, or intentionally turned off
    Overloaded,   // Station has exceeded safe usage limits or encountered an error

    // Capacity-based states (for internal tank percentage)
    Critical,     // < 10% of capacity
    Low,          // < 30% of capacity
    Normal,       // 30% - 80% of capacity
    High,         // 80% - 99% of capacity
    Full          // 100% of capacity
}





//public enum ChargeStationStateEnum
//{
//    VoltEnter,
//    VoltExit,
//    VoltFullCharged,

//    HasCharge,
//    NoCharge,

//    //Charging,
//    //FullyCharged,
//    Depleted, // When Volt's charge reaches zero

//    Full,
//    Partial,
//    Empty
//}