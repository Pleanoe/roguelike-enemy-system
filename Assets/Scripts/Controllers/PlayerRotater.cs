using System;
using UnityEngine;
public class PlayerRotater : MonoBehaviour
{
    public static event Action OnReadyToShoot; //Стрелять только когда повернулся
    private Camera mainCamera;
    private Plane groundPlane;
    private void Awake()
    {
        mainCamera = Camera.main;
        groundPlane = new Plane(Vector3.up, transform.position);
    }
    private void OnEnable() => InputManager.OnMouseHeld += RotatePlayer;
    private void OnDisable() => InputManager.OnMouseHeld -= RotatePlayer;
    private void RotatePlayer()
    {
       groundPlane.SetNormalAndPosition(Vector3.up, transform.position);
       Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       if (groundPlane.Raycast(ray,out float distance))
       {
           Vector3 hitPoint = ray.GetPoint(distance);
           hitPoint.y = transform.position.y;
           transform.LookAt(hitPoint);
       }
       OnReadyToShoot?.Invoke();
    }
}
