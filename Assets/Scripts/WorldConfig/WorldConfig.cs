using UnityEngine;

namespace CatCode.WorldConfig
{
    public class WorldConfig : MonoBehaviour
    {
        
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}