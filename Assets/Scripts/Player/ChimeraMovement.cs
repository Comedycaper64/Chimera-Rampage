using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraMovement : MonoBehaviour
    {
        [SerializeField]
        private Animator bodyAnimator;

        [SerializeField]
        private Transform visualTransform;
        private InputManager inputManager;
        private bool canMove;
        private float movementSpeed;

        private void Awake()
        {
            canMove = true;
            movementSpeed = GetComponent<ChimeraStats>().movementSpeed;
            inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            if (canMove)
            {
                transform.position +=
                    new Vector3(inputManager.MovementValue.x, inputManager.MovementValue.y)
                    * movementSpeed
                    * Time.deltaTime;
                if (inputManager.MovementValue.x < 0)
                {
                    visualTransform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (inputManager.MovementValue.x > 0)
                {
                    visualTransform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            bodyAnimator.SetFloat(
                "Speed",
                Mathf.Abs(inputManager.MovementValue.x) + Mathf.Abs(inputManager.MovementValue.y)
            );
        }

        public void ToggleMove(bool enable)
        {
            canMove = enable;
        }
    }
}
