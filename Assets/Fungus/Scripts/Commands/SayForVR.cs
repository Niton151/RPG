using UnityEngine;

namespace Fungus
{
    [CommandInfo("Narrative", 
        "SayForVR", 
        "Writes text in a dialog box.")]
    [AddComponentMenu("")]
    public class SayForVR : Say
    {
        // 子オブジェクトから、指定のタグが付いたTransformを探す。
        protected Transform FindTagInChildren(Transform t, string tagName)
        {
            if (t == null)
            {
                return null;
            }

            if (t.tag == tagName)
            {
                return t;
            }

            foreach (Transform child in t)
            {
                Transform s = FindTagInChildren(child, tagName);

                if (s)
                {
                    return s;
                }
            }

            return null;
        }

        public Transform talker;

        public override void OnEnter()
        {
            Transform from = FindTagInChildren(talker, "Head") ?? talker;
            ((SayDialogForVR)setSayDialog).SetDialogTransform(from);
            base.OnEnter();
        }
    }
}
