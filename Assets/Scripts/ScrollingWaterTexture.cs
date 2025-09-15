using UnityEngine;

public class ScrollingWaterTexture : MonoBehaviour
{

    // Scroll speed for X and Y axes;
    [SerializeField] private Vector2 _scrollSpeed = new Vector2(0f, -0.1f);

    private Renderer objectRenderer;
    public Vector3 flowSpeed;

    // Name of the texture property in the shader
    private string textureProperty = "_MainTex";

    void Start()
    {
        // Get the Renderer component
        objectRenderer = GetComponent<Renderer>();

        flowSpeed = new Vector3(_scrollSpeed.x, 0f, _scrollSpeed.y);
    }

    void Update()
    {
        // Calculate new offset based on time
        Vector2 offset = Time.time * _scrollSpeed;


        objectRenderer.material.SetTextureOffset(textureProperty, offset);
    }


}