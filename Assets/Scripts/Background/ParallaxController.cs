using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float speed { get; set; }
    private float x;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        float x = meshRenderer.material.mainTextureOffset.x;
    }

    void Update()
    {
        float y = meshRenderer.material.mainTextureOffset.y;

        meshRenderer.material.mainTextureOffset = 
            new Vector2(x, y - Time.deltaTime * speed);
    }
}
