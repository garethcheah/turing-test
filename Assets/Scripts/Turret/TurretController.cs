using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float _attackRange = 10.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;

    private Transform _target;

    // Start is called before the first frame update
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_target != null)
        {
            if (TargetIsInRange())
            {
                RotateTurretTowardsTarget();
            }
        }
    }

    private bool TargetIsInRange()
    {
        return Vector3.Distance(_target.position, transform.position) <= _attackRange;
    }

    private void RotateTurretTowardsTarget()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = _target.position - transform.position;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationSpeed * Time.deltaTime, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
