using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public Vector3 offset;

    void LateUpdate() {
            if (!GameManager.instance.isDead) {
                transform.position = target.position + offset;
            }
    }
}
