using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraCursorPointer : MonoBehaviour
    {
        private Vector3 mousePosition;

        private void Update()
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = 0; //-(transform.position.x - Camera.main.transform.position.x);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.LookAt(worldPosition, Vector3.forward);
            transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
        }

        public Vector3 GetCursorDirection()
        {
            return transform.up;
        }

        public Vector3 GetMouseWorldPosition()
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = 0; //-(transform.position.x - Camera.main.transform.position.x);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            return worldPosition;
        }
    }
}
