using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet;
    void LateUpdate()
    {
        transform.position = target.position + offSet;
        transform.LookAt(target);
    }
}
