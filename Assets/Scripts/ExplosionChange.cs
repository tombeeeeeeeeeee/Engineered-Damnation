using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionChange : MonoBehaviour
{
    [SerializeField] GameObject explodedVersion;

    public void Explode()
    {
        if(explodedVersion != null)
            explodedVersion.SetActive(true);
        gameObject.SetActive(false);
    }
}
