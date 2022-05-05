using UnityEngine;

namespace CatCode
{
    public class CameraTrack : MonoBehaviour
    {
        public Transform playerViewPos;

        private void LateUpdate()
        {
            var transformObject = transform;

            transformObject.SetPositionAndRotation(playerViewPos.position, playerViewPos.rotation);
        }
    }
}


