using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IAttacker, IDamageApplicable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ApplyDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }
}
