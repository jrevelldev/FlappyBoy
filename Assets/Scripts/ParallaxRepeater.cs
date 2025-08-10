using System.Linq;
using UnityEngine;

public class ParallaxRepeater : MonoBehaviour
{
    public float speed = 0.6f;
    public bool obeyGameState = true;
    public Camera cam; // assign Main Camera, or it will find one

    private Transform[] tiles;
    private float tileWorldWidth;
    private float halfCamWidth;

    void OnEnable()
    {
        if (cam == null) cam = Camera.main;

        tiles = GetComponentsInChildren<SpriteRenderer>(false)
                    .Select(sr => sr.transform).ToArray();

        if (tiles.Length < 2)
        {
            Debug.LogWarning($"{name}: needs 2 child tiles with SpriteRenderers.");
            enabled = false; return;
        }

        var sr = tiles[0].GetComponent<SpriteRenderer>();
        tileWorldWidth = sr.bounds.size.x;

        SortTilesByX();
        UpdateCameraWidth();
    }

    void Update()
    {
        if (!Application.isPlaying) return; // prevents editor movement

        if (obeyGameState)
        {
            var gm = GameManager.Instance;
            if (gm != null && gm.CurrentState == GameManager.GameState.Idle)
                return;
        }

        if (cam == null) cam = Camera.main;
        UpdateCameraWidth();

        float dt = Time.deltaTime;
        transform.position += Vector3.left * speed * dt;

        Transform leftmost = tiles[0];
        Transform rightmost = tiles[tiles.Length - 1];

        float leftmostRightEdge = leftmost.position.x + tileWorldWidth * 0.5f;
        float screenLeft = cam.transform.position.x - halfCamWidth;

        if (leftmostRightEdge < screenLeft)
        {
            leftmost.position = new Vector3(
                rightmost.position.x + tileWorldWidth,
                leftmost.position.y,
                leftmost.position.z
            );
            SortTilesByX();
        }
    }

    void UpdateCameraWidth()
    {
        if (cam == null) return;
        halfCamWidth = cam.orthographicSize * cam.aspect;
    }

    void SortTilesByX()
    {
        tiles = tiles.OrderBy(t => t.position.x).ToArray();
    }
}
