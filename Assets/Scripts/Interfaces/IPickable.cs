using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    public void OnPicked(Transform attachPoint);
    public void OnDropped();
}
