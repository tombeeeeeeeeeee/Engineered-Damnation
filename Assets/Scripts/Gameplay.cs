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

    public static void ChildrenMaterialColour(GameObject obj, int index, Color colour)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if(meshRenderer)
        {
            Material[] mats = meshRenderer.materials;
            mats[index].color = colour;
            meshRenderer.materials = mats;
        }
        for (int i = 0; i < obj.transform.childCount; i++)
            ChildrenMaterialColour(obj.transform.GetChild(i).gameObject, index, colour);
    }
}
