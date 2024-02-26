using UnityEngine.Tilemaps;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Tilemap firstMap;
    public Tilemap lastMap;
    private Bounds bounds1;
    private Bounds bounds2;
    public float smoothTimeX;
    public float smoothTimeY;
    public bool Bounds;

    void Start()
    {
        bounds1 = firstMap.localBounds;
        bounds2 = lastMap.localBounds;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    void Update()
    {
        Vector3 minCameraEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane)); // left edge of my camera in x
        Vector3 maxCameraEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane)); // right edge of my camera in x

        if (Bounds)
        {
            if (minCameraEdge.x < bounds1.min.x) minCameraEdge.x = bounds1.min.x;
            if (maxCameraEdge.x > bounds2.max.x) maxCameraEdge.x = bounds2.max.x;
        }
    }
}
