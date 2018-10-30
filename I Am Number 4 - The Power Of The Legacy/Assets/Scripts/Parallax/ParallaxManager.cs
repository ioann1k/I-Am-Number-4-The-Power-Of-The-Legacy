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

    [Tooltip("The list of all the school layers in the parallax.")]
    public List<Layer> schoolLayers;

    private float initialScrollSpeed = 6.0f;

    private int forestSpawnCount = 0;
    private int maxForestSpawnCount = 4;
    
    private bool shouldUpdateSchool = false;

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

        forestSpawnCount++;
    }

    /// <summary>
    /// Update the layers.
    /// </summary>
    void Update()
    {
        float time = Time.deltaTime * scrollSpeed;
        if (forestSpawnCount == maxForestSpawnCount)
        {
            InitializeSchool();

            forestSpawnCount = 0;
        }
        else
        {
            foreach (Layer forestLayer in forestLayers)
            {
                CheckPosition(forestLayer);

                forestLayer.Update(this, mainCamera, time);
            }
        }

        if (shouldUpdateSchool)
        {
            foreach (Layer forestLayer in forestLayers)
            {
                CheckPosition(forestLayer);

                forestLayer.Update(this, mainCamera, time);
            }
        }
    }

    private void InitializeSchool()
    {

        shouldUpdateSchool = true;
    }

    private void CheckPosition(Layer layer)
    {
        GameObject spriteToDestroy = layer.CheckPosition(mainCamera);
        if (spriteToDestroy != null)
        {
            Destroy(spriteToDestroy);

            forestSpawnCount++;
        }
    }
}
