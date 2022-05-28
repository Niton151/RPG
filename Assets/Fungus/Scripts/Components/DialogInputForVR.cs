using UnityEngine;
using UnityEngine.EventSystems;

namespace Fungus
{
    public class DialogInputForVR : DialogInput
    {
        protected override void Update()
        {
            if (EventSystem.current == null)
            {
                return;
            }

            if (writer != null && writer.IsWriting)
            {
                // ここを好きなキーに設定する
                if (Input.GetKeyDown(KeyCode.Return)
                    || Input.GetKeyDown(KeyCode.Z)
                    || Input.GetKeyDown(KeyCode.Space))
                {
                    SetNextLineFlag();
                }
            }

            if (nextLineInputFlag)
            {
                var inputListeners = gameObject.GetComponentsInChildren<Fungus.IDialogInputListener>();
                for (int i = 0; i < inputListeners.Length; i++)
                {
                    var inputListener = inputListeners[i];
                    inputListener.OnNextLineEvent();
                }
                nextLineInputFlag = false;
            }
        }
    }
}
