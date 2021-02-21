using UnityEngine;

/// <summary>
/// Simple script to rotate an object around
/// </summary>
/// 
public enum RotationAxis { X, Y, Z }

public class RotateSpinner : MonoBehaviour
{
    [SerializeField]
    RotationAxis rotationAxis = RotationAxis.Z;

    [Tooltip("How fast should the object rotate")]
    public float speed = 1f;

    private void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            if(rotationAxis == RotationAxis.Z)
                this.transform.Rotate(new Vector3(0, 0, speed));
            else if(rotationAxis == RotationAxis.Y)
                this.transform.Rotate(new Vector3(0, speed, 0));
            else if (rotationAxis == RotationAxis.X)
                this.transform.Rotate(new Vector3(speed, 0, 0));
        }
    }
}
