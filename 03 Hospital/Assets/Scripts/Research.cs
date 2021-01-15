using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("offices").RemoveResource();
        if (target == null)
            return false;
        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeOffice", -1);

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("offices").AddResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreeOffice", 1);

        return true;
    }
}
