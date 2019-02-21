using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public class CameraControl : MonoBehaviour
    {
        public GameObject vPivot, hPivot, cameraHolder;
        public float rotationSensitivity, verticalSensitivity, zoomFactor, turnMultiplier;
        private Vector3 scaleFactor;
        private Vector3 aimPoint, currentOffset;
        private float scroll, mouseX, mouseY;
        private System.Drawing.Point mouseLocation;
        private ShipControl playerCharacter;
        public static GameObject target;
        private bool targetChanged;

        public float lerpSpeed;
        private float moveStartTime;
        // Use this for initialization
        void Start()
        {
            scaleFactor = new Vector3(zoomFactor, zoomFactor, zoomFactor);
            aimPoint = new Vector3(0, 0, 0);
            Camera.main.depthTextureMode = DepthTextureMode.Depth;
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null)
            {
                return;
            }
            scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.position = Vector3.Lerp(transform.position, target.transform.position, moveStartTime / lerpSpeed);
            moveStartTime += Time.deltaTime;
            if (scroll != 0)
            {
                scaleFactor.Set(0, 0, zoomFactor * scroll * Time.deltaTime);
                cameraHolder.transform.Translate(scaleFactor, Space.Self);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                mouseLocation = System.Windows.Forms.Cursor.Position;
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                System.Windows.Forms.Cursor.Position = mouseLocation;
            }

            if (Input.GetMouseButton(0))
            {
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");
                if (vPivot.transform.localRotation.eulerAngles.x + mouseY * -verticalSensitivity * Time.smoothDeltaTime > 89.0f)
                {
                    vPivot.transform.localRotation = Quaternion.AngleAxis(89.0f, vPivot.transform.InverseTransformVector(vPivot.transform.right));
                }
                else if (vPivot.transform.localRotation.eulerAngles.x + mouseY * -verticalSensitivity * Time.smoothDeltaTime < 0.0f)
                {
                    vPivot.transform.localRotation = Quaternion.AngleAxis(0.0f, vPivot.transform.InverseTransformVector(vPivot.transform.right));
                }
                else
                {
                    vPivot.transform.Rotate(hPivot.transform.InverseTransformVector(vPivot.transform.right), mouseY * -verticalSensitivity * Time.smoothDeltaTime);
                }
                hPivot.transform.Rotate(hPivot.transform.InverseTransformVector(hPivot.transform.up), mouseX * rotationSensitivity * Time.smoothDeltaTime);

                Cursor.visible = false;


            }
            else if (Input.GetMouseButton(1))
            {
                //playerCharacter.move(Input.GetAxis("Mouse X") * turnMultiplier * Time.deltaTime, 0);
                UnityEngine.Cursor.visible = false;
            }
            else
            {

                UnityEngine.Cursor.visible = true;
            }


            cameraHolder.transform.LookAt(target.transform);


        }

        public void ChangeTarget(GameObject newTarget)
        {
            target = newTarget;
            moveStartTime = 0;
            playerCharacter = newTarget.GetComponent<ShipControl>();
        }
    }
}

