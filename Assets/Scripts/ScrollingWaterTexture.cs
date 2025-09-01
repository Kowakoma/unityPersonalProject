using UnityEngine;

public class ScrollingWaterTexture : MonoBehaviour
{
    // Scroll speed for X and Y axes;
    public Vector2 scrollSpeed = new Vector2(0f, -0.5f);
    
    private Renderer objectRenderer;
    
    // Name of the texture property in the shader
    private string textureProperty = "_MainTex";

    void Start()
    {
        // Get the Renderer component
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate new offset based on time
        Vector2 offset = Time.time * scrollSpeed;
        
        
        objectRenderer.material.SetTextureOffset(textureProperty, offset);
    }
}