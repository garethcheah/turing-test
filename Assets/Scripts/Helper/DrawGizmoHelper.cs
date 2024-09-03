using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoHelper : MonoBehaviour
{
    [SerializeField] private float _radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
