using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gameplay
{
    public static float deltaTime;
    public static float timeSinceStart;
    public static bool gameplayActive = true;

    public static void ChildrenLayerSet(GameObject obj, int layer)
    {
        obj.layer = layer;
        for(int i = 0; i < obj.transform.childCount; i++)
            ChildrenLayerSet(obj.transform.GetChild(i).gameObject, layer);
    }
}
