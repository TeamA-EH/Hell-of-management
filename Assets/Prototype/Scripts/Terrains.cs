using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrains : MonoBehaviour
{
    public enum ESurfaceType { Defaulty, Icy, Sticky };
    public ESurfaceType _surfaceType = ESurfaceType.Defaulty;

    public ESurfaceType Esurfacetype { get { return _surfaceType; } }
}
