using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{
    public void Init(EnemyParameters enemyParameters)
    {
        EnemyParameters = enemyParameters;
    }

    public EnemyParameters EnemyParameters { get; private set; }
}
