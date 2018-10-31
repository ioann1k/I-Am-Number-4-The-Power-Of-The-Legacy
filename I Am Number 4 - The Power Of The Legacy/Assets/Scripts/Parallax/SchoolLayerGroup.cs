using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class SchoolLayerGroup
{
    private Camera mainCamera;
    private SchoolParallaxManager parallaxManager;

    [Tooltip("The list of the different layer variations of the current layer group.")]
    public List<SchoolLayer> layers;

    public void Initialize(SchoolParallaxManager schoolParallaxManager)
    {
        mainCamera = Camera.main;
        parallaxManager = schoolParallaxManager;
    }

    public void Update(float time)
    {
        CheckPosition(layers[0]);
        foreach (SchoolLayer layer in layers)
        {
            layer.Update(parallaxManager, mainCamera, time);
        }
    }

    public void SpawnLayers(float positionAtWhichToSpawn = 0.0f)
    {
        foreach (SchoolLayer layer in layers)
        {
            layer.Initialize(parallaxManager);
            layer.Spawn(positionAtWhichToSpawn);
        }

        CheckPosition(layers[0]);
    }

    private bool CheckPosition(SchoolLayer layer)
    {
        bool shouldDestroy = false;
        GameObject spriteToDestroy = layer.CheckPosition(mainCamera);
        if (spriteToDestroy != null)
        {
            parallaxManager.DestroyObject(spriteToDestroy);
            shouldDestroy = true;
        }

        return shouldDestroy;
    }

    public bool ShouldRemoveGroup()
    {
        bool shouldRemoveGroup = false;
        bool resultFromCheck = CheckPosition(layers[0]);
        if (resultFromCheck)
        {
            shouldRemoveGroup = true;
        }
        //foreach (SchoolLayer layer in layers)
        //{
        //    bool resultFromCheck = CheckPosition(layer);
        //    if (resultFromCheck)
        //    {
        //        shouldRemoveGroup = true;
        //    }
        //}

        return shouldRemoveGroup;
    }
}
