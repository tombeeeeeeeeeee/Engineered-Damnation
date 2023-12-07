using System.Runtime.InteropServices;
using UnityEngine;

public static class Gameplay
{
    public static float deltaTime;
    public static float timeSinceStart;
    public static bool active = false;
    public static float masterVolume
    {
        get { return masterVolume; }
        set { masterVolume = masterVolume <= 0 ? 0.001f : masterVolume; }
    }

    public static float sfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = sfxVolume <= 0 ? 0.001f : sfxVolume; }
    }

    public static float musicVolume
    {
        get { return musicVolume; }
        set { musicVolume = musicVolume <= 0 ? 0.001f : musicVolume; }
    }
    public static float dialogueVolume
    {
        get { return dialogueVolume; }
        set { dialogueVolume = dialogueVolume <= 0 ? 0.001f : dialogueVolume; }
    }

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
