using System;
using UnityEngine;

public interface IDestroyable
{
    event Action<GameObject> ODestroy;
}
