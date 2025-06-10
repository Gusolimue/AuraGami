using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamMove
{
    public class CamMovement : MonoBehaviour
    {
        Vector3 cameraPosition;

        [Header("CameraSettings")]
        public float cameraSpeed;

        void Start() 
        {
            cameraPosition = this.transform.position;
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.D))
            {
                cameraPosition.x += cameraSpeed * Time.deltaTime;
            }

            if(Input.GetKey(KeyCode.A))
            {
                cameraPosition.x -= cameraSpeed * Time.deltaTime;
            }

            this.transform.position = cameraPosition;
        }
    }

}


