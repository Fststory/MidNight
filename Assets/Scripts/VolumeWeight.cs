using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeWeight : MonoBehaviour
{
    public Volume vol;

    public void SetWeight (float weight)
    {
        vol.weight = weight;
    }
}
