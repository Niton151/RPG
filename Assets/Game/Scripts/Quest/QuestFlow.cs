using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Quest;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class QuestFlow : SerializedMonoBehaviour
{
    [OdinSerialize] private List<QuestBase> quests = new List<QuestBase>();
}
