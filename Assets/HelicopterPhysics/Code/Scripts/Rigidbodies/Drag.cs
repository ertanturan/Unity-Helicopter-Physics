using UnityEngine;

public class Drag : MonoBehaviour
{
    #region Variables
    [Header("Drag Properties")]
    public float DragFactor = 0.05f;

    private Rigidbody _rb;
    #endregion


    #region builting methods
    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_rb)
        {
            float currentSpeed = _rb.velocity.magnitude;
            float finalDrag = currentSpeed * DragFactor;
            _rb.drag = finalDrag;
        }
    }
    #endregion
}
