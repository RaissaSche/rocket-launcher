using UnityEngine;

public class CustomCamera : MonoBehaviour {

    public GameObject rocket;
    public float offset;

    private void LateUpdate() {
        var rocketPosition = rocket.transform.position;
        var cameraPosition = new Vector3(rocketPosition.x + offset, rocketPosition.y, rocketPosition.z + offset);
        transform.position = cameraPosition;
        transform.LookAt(rocket.transform);
    }
}