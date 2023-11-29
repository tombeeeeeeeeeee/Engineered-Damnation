using System.Runtime.InteropServices;
using UnityEngine;

public static class Gameplay
{
    public static float deltaTime;
    public static float timeSinceStart;
    public static bool gameplayActive = true;
    public static float masterVolume;
    public static float sfxVolume;
    public static float musicVolume;
    public static float dialogueVolume;

    public static void ChildrenLayerSet(GameObject obj, int layer)
    {
        obj.layer = layer;
        for(int i = 0; i < obj.transform.childCount; i++)
            ChildrenLayerSet(obj.transform.GetChild(i).gameObject, layer);
    }

    public static void ChildrenMaterialColour(GameObject obj, Color colour)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if(renderer)
        {
            Material[] mats = renderer.materials;
            mats[0].color = colour;
            renderer.materials = mats;
        }
        
        for (int i = 0; i < obj.transform.childCount; i++)
            ChildrenMaterialColour(obj.transform.GetChild(i).gameObject, colour);
    }
}
