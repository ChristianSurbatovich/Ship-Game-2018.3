using UnityEngine;

namespace ShipGame
{
    public class ShipCollision : MonoBehaviour
    {
        private IShipControl parentShip;

        // Use this for initialization
        void Start()
        {
            parentShip = GetComponentInParent(typeof(IShipControl)) as IShipControl;
            transform.localPosition = Vector3.zero;
        }

        void OnCollisionEnter(Collision otherObject)
        {

            print("Collided with " + otherObject.gameObject);

            parentShip.CollideWith(otherObject);



        }

        void OnCollisionStay(Collision otherObject)
        {

            print("Still in collision with " + otherObject.gameObject);

            parentShip.InCollision(otherObject);



        }

        private void OnCollisionExit(Collision otherObject)
        {
            print("Stopped colliding with " + otherObject.gameObject);
            parentShip.StopCollision(otherObject);
        }

    }

}
