
public enum VoltChargeStateEnum
{
    Charging,       // When Volt is being charged
    FullyCharged,   // When Volt is at maximum charge
    Normal,         // When Volt's charge is between 30% and maximum
    Partial,        // When Volt's charge is between 20% and 30%
    Low,            // When Volt's charge is below 20%
    Critical,       // When Volt's charge is below 10%
    Emergency,      // When Volt's charge is below 5%
    Empty           // When Volt's charge reaches zero
}