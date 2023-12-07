using System.Runtime.InteropServices;
using UnityEngine;

public static class Gameplay
{
    public static float deltaTime;
    public static float timeSinceStart;
    public static bool active = false;

    private static float _masterVolume;
    private static float _sfxVolume;
    private static float _musicVolume;
    private static float _dialogueVolume;

    public static float masterVolume
    {
        get { return _masterVolume; }
        set { _masterVolume = value <= 0 ? 0.001f : value; }
    }

    public static float sfxVolume
    {
        get { return _sfxVolume; }
        set { _sfxVolume = value <= 0 ? 0.001f : value; }
    }

    public static float musicVolume
    {
        get { return _musicVolume; }
        set { _musicVolume = value <= 0 ? 0.001f : value; }
    }
    public static float dialogueVolume
    {
        get { return _dialogueVolume; }
        set { _dialogueVolume = value <= 0 ? 0.001f : value; }
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
