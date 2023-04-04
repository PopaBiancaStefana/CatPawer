using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRaycastHandler : MonoBehaviour
{
    private const float RayCastMaxDistance = 99f;
    private static LayerMask IgnoredLayerMask;

    void Awake()
    {
        IgnoredLayerMask = LayerMask.GetMask("Bullet", "Ignore Raycast");
    }

    static public Vector3 GetCameraTargetPosition()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, RayCastMaxDistance, ~IgnoredLayerMask))
            return hit.point;

        return ray.GetPoint(RayCastMaxDistance);
    }
}