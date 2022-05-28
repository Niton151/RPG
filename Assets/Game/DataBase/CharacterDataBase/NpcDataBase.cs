using System.Collections.Generic;
using System.Linq;
using Game.DataBase.PlayerDataBase;
using Game.Scripts.NPC;
using Game.Scripts.Player.Skill;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.DataBase.CharacterDataBase
{
    //[CreateAssetMenu(fileName = "NpcDataBase", menuName = "CreateNpcDataBase")]
    public class NpcDataBase : SerializedScriptableObject
    {
        public List<BaseNpcData> npcs = new List<BaseNpcData>();

            public List<BaseNpcData> GetNpcList => npcs;
        
            public BaseNpcData FindData(NpcType type)
            {
                return npcs.FirstOrDefault(x => x.type == type);
            }
    }
}
