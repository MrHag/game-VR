using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnControled
{

    Spawner Spawner { get; set; }
    event Action Spawn;

    event Action<GameObject> ODestroy;
}
