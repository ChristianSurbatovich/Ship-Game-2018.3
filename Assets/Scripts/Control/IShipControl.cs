using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public interface IShipControl
    {



        void SetMovement(float x, float y);

        void OpenGunports();

        void CollideWith(Collision other);


        void StopCollision(Collision other);


        void InCollision(Collision other);

        void Sink(Vector3 direction, float sinkTime, float flipSpeed, float sinkSpeed);

        void FireWeapon(Vector3[] aimPoints, short id, short weaponID);

        Vector3[] FireWeapon(Vector3 aimpoint, short id, short weaponID);

        void AimAtPoint(Vector3 aimPoint);

        void SetStat(short statID, float value);
        void SetStat(short statID, int value);
        void SetStat(byte[] action);
        float GetStat(short StatID);
        float GetAbilityCooldown(short id);

        Vector3 Position();
        Vector3 LocalRotation();
        void SeaHeight(ref float[] outArray);

        GameObject GetAttachPoint(string name);
    }
}

