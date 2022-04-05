using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public struct PlayerParameters
{
    public int Level { get; set; }
    
    public float JumpPower { get; set; }
    public float MoveSpeed { get; set; }
    public float RunSpeed { get; set; }
    
    public float MaxHP { get; set; }
    public float MaxMP { get; set; }
    public float MaxSP { get; set; }

    public int Atk { get; set; }
    public int Defence { get; set; }
    public int Magic { get; set; }
}