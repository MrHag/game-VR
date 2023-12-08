using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnControled : IDestroyable
{

    Spawner Spawner { get; set; }
    event Action Spawn;
}
