using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public class SayDialogForVR : SayDialog
    {
        Vector3 prevPosition = Vector3.zero;
        Quaternion prevRotation = Quaternion.identity;
        Vector3 prevScale = Vector3.zero;
        Vector3 toScale;
        public float speed = 0.05f;
        public Vector3 localPosition = new Vector3(0.0f, -0.222f, 2.3732f);        
        public Transform mainCamera;
        private Transform _player;

        Vector3 GetDialogPosition()
        {
            return mainCamera.position
                + mainCamera.right * localPosition.x
                + mainCamera.up * localPosition.y
                + mainCamera.forward * localPosition.z;                
        }

        Quaternion GetDialogRotation()
        {
            return Quaternion.LookRotation(GetDialogPosition() - mainCamera.position);
        }

        public void SetDialogTransform(Transform from)
        {
            prevPosition = from ? from.position: GetDialogPosition();
            prevRotation = GetDialogRotation();
            prevScale = from ? Vector3.zero : toScale;
        }

        protected override void Start()
        {
            toScale = transform.localScale;
            mainCamera = mainCamera ?? Camera.main.transform;
            _player = GameObject.FindWithTag("Player").transform;
            base.Start();
        }

        protected override void LateUpdate()
        {
            if (Vector3.Distance(_player.position, this.transform.position) > 5)
            {
                Stop();
                Clear();
                targetAlpha = 0f;
            }
            base.LateUpdate();
        }

        protected override void UpdateAlpha()
        {
            transform.position = Vector3.Lerp(prevPosition, GetDialogPosition(), speed);
            transform.rotation = Quaternion.Lerp(prevRotation, GetDialogRotation(), speed);
            transform.localScale = Vector3.Lerp(prevScale, toScale, speed);
            prevPosition = transform.position;
            prevRotation = transform.rotation;
            prevScale = transform.localScale;
            base.UpdateAlpha();
        }
    }
}
