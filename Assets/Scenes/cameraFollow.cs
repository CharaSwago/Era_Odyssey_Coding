using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffet;
    public Vector3 posOffet;

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffet, ref velocity, timeOffet);
    }
}
