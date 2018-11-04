using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    private Camera mainCamera;

    [Tooltip("The global scroll speed of the parallax.")]
    public float scrollSpeed;

    [Tooltip("The list of all the forest layers in the parallax.")]
    public List<Layer> forestLayers;

    [Tooltip("The list of all the layers in the parallax.")]
    public Counter counter;

    private float initialScrollSpeed = 6.0f;

    /// <summary>
    /// Setup the layers.
    /// </summary>
    void Start()
    {
        mainCamera = Camera.main;
        foreach (Layer forestLayer in forestLayers)
        {
            if (forestLayer.shouldBeSpawnedRandomly)
            {
                forestLayer.RandomlySpawn();
            }
            else
            {
                forestLayer.Spawn(0.0f);
            }

            CheckPosition(forestLayer);
        }
    }

    /// <summary>
    /// Update the layers.
    /// </summary>
    void Update()
    {
        float time = Time.deltaTime * scrollSpeed;
        foreach (Layer forestLayer in forestLayers)
        {
            CheckPosition(forestLayer);

            forestLayer.Update(this, mainCamera, time);
        }

        Layer firstLayer = forestLayers[0];
        Renderer spriteRenderer = firstLayer.GetSpriteRendererAtIndex(0);

        bool shouldUpdateDistanceMeter = false;
        shouldUpdateDistanceMeter = firstLayer.UpdateDistance(spriteRenderer);
        if (shouldUpdateDistanceMeter)
        {
            counter.UpdateDistance();
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
