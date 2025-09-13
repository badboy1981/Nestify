public enum ChargeStationStateEnum
{
    VoltEnter,
    VoltExit,
    VoltFullCharged,

    HasCharge,
    NoCharge,

    Charging,
    FullyCharged,
    Depleted, // When Volt's charge reaches zero

    Full,
    Partial,
    Empty
}