using Game.DataBase.CharacterDataBase;
using Game.Scripts.NPC;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class NpcProvider
    {
        private static NpcDataBase DataBase = Resources.Load<NpcDataBase>("NpcDataBase");
        
        public static NpcCore Create(Vector3 pos, NpcType type)
        {
            var data = DataBase.FindData(type);
            var npc = Object.Instantiate(data.prefab, pos, Quaternion.identity).GetComponent<NpcCore>();
            npc.Init(data);
            return npc;
        }
    }
}
