using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointSpawner : MonoBehaviour
{
    public float width, height;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }
}
