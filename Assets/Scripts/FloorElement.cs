using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FloorElement : MonoBehaviour, IPooleable

{
    public virtual void SetColor(ColorStatus colorStatus) { }

    public bool IsBeingUsed()
    {
        throw new System.NotImplementedException();
    }

    public void OnEnterPool()
    {
        throw new System.NotImplementedException();
    }

    public void OnLeavePool()
    {
        throw new System.NotImplementedException();
    }
}
