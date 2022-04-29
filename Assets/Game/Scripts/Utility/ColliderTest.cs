using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ColliderTest
{
    public IObservable<Unit> OnTestFinished => _onFinishedSubject;
    private AsyncSubject<Unit> _onFinishedSubject = new AsyncSubject<Unit>();

    private void Start()
    {
        
    }

    public async void Clash(GameObject a, GameObject b)
    {
        if (a.TryGetComponent<Collider>(out var cola) && b.TryGetComponent<Collider>(out var colb))
        {
            cola.enabled = false;
            colb.enabled = false;
            
            a.transform.position = Vector3.zero;
            b.transform.position = new Vector3(0, 3, 0);
        }
        else
        {
            throw new MissingComponentException();
        }

        cola.enabled = true;
        colb.enabled = true;

        if (a.TryGetComponent<Rigidbody>(out var rba))
        {
            rba.useGravity = false;
            rba.isKinematic = true;
        }
        else
        {
            Debug.Log($"{a.name}はRigidbodyを持っていません");
        }

        if (!b.TryGetComponent<Rigidbody>(out _))
        {
            Debug.Log($"{b.name}はRigidbodyを持っていません");
            var rbb = b.AddComponent<Rigidbody>();
            rbb.useGravity = true;
        }

        if (cola.isTrigger == false && colb.isTrigger == false)
        {
            await rba.OnCollisionEnterAsObservable().ToUniTask(true);
        }
        else
        {
            await rba.OnTriggerEnterAsObservable().ToUniTask(true);
        }

        _onFinishedSubject.OnNext(Unit.Default);
        _onFinishedSubject.OnCompleted();

        _onFinishedSubject = new AsyncSubject<Unit>();
    }
}
