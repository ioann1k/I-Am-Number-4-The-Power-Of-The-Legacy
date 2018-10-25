using System.Collections.Generic;

using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera mainCamera;

    [Tooltip("The global scroll speed of the parallax.")]
    public float scrollSpeed;

    [Tooltip("The list of all the forest layers in the parallax.")]
    public List<Layer> forestLayers;

    [Tooltip("The list of all the school layers in the parallax.")]
    public List<Layer> schoolLayers;

    private float initialScrollSpeed = 6.0f;

    /// <summary>
    /// Setup the layers.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        foreach (Layer forestLayer in forestLayers)
        {
            forestLayer.Spawn(0f);

            CheckPosition(forestLayer);
        }
    }

    /// <summary>
    /// Update the layers.
    /// </summary>
    void Update()
    {
        // TODO: Count distance based on layers spawned
        float time = Time.deltaTime * scrollSpeed;
        foreach (Layer forestLayer in forestLayers)
        {
            CheckPosition(forestLayer);

            forestLayer.Update(this, mainCamera, time);
        }
    }

    private void CheckPosition(Layer layer)
    {
        GameObject spriteToDestroy = layer.CheckPosition(mainCamera);
        if (spriteToDestroy != null)
        {
            Destroy(spriteToDestroy);
        }
    }
}
