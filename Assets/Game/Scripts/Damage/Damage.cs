using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    public Damage(IAttacker attacker, float value)
    {
        Value = value;
        Attacker = attacker;
    }

    public float Value { get; }
    public IAttacker Attacker { get; }
}
