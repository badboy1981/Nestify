using Collectable.Gate;
using System.Linq;
public static class FindGateProperty
{
    // My code is designed to find a specific GateProperty within a GatePropertyGroup based on the gate name.
    public static GateProperty GetGateProperty(GatePropertyGroup Gpg, string gName)
    {
        //git hub copilot suggestion => return Gpg?.gateProperties?.Find(gateProperty => gateProperty.name[0] == Gpg.name[0]) ?? null;
        return Gpg.gateProperties.FirstOrDefault(g => g.name[0] == gName[0]);
    }
    // github Copilot: This method retrieves a GateProperty from a GatePropertyGroup by matching the gate name.
    //public static GateProperty gitCopilotGetGateProperty(GatePropertyGroup gatePropertyGroup, string gateName)
    //{
    //    if (gatePropertyGroup == null || gatePropertyGroup.gateProperties == null)
    //    {
    //        //Debug.LogError("GatePropertyGroup or its gateProperties list is null.");
    //        return null;
    //    }
    //    foreach (var gateProperty in gatePropertyGroup.gateProperties)
    //    {
    //        if (gateProperty.name.Equals(gateName, System.StringComparison.OrdinalIgnoreCase))
    //        {
    //            return gateProperty;
    //        }
    //    }
    //    //Debug.LogWarning($"No GateProperty found with the name: {gateName}");
    //    return null;
    //}
}
