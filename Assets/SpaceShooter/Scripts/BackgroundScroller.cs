using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    [SerializeField] private Material backgroundMaterial;
    private Vector2 offset;

    private void Start()
    {
        offset = Vector2.zero;
    }

    private void Update()
    {
        offset += new Vector2(0f, Time.deltaTime * scrollSpeed);
        backgroundMaterial.mainTextureOffset = offset;
    }
}