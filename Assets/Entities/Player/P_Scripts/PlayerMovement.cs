using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    public float MoveSpeed { get { return moveSpeed;} set { moveSpeed = value; } }
    [SerializeField] private float velocityDampenLerpOnMove = .1f;
    private Rigidbody rb;
    private Vector3 direction;
    private Camera playerCam;

    [Header("Slope handling: ")] 
    [SerializeField] private float maxSlopeAngle, playerHeight;
    private RaycastHit slopeHit;
    
    private float freeVelocityTime;

    public bool IsMoving { get; private set; }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
    }
    
    private void Update()
    {
        IsMoving = direction.magnitude > 0f;

        // movement relative to camera angle
        Vector3 camForward = playerCam.transform.forward;
        Vector3 camRight = playerCam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        
        float dirVertical = Input.GetAxisRaw("Vertical"), dirHorizontal = Input.GetAxisRaw("Horizontal");
        direction = dirVertical * camForward.normalized + dirHorizontal * camRight.normalized;
    }
    
    private void FixedUpdate()
    {
        if (freeVelocityTime > 0f)
            freeVelocityTime -= Time.deltaTime;
        else
            DampenRBVelocity(velocityDampenLerpOnMove);
        
        Move(direction * (moveSpeed * Time.fixedDeltaTime));
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * .5f + .3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0f;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
    
    private void Move(Vector3 moveVector)
    {
        if (GameManager.Instance.FreezePlayer)
            return;
        
        rb.velocity += moveVector;
    }
    
    private void DampenRBVelocity(float lerpValue)
    {
        Vector3 targetVelocity = new(0f, rb.velocity.y, 0f);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, lerpValue);
    }
    
    public void AddImpulseForce(Vector3 force, float releaseVelocityDampenTime = 0f)
    {
        rb.AddForce(force, ForceMode.Impulse);
        freeVelocityTime = releaseVelocityDampenTime;
    }
    
    public Vector3 Direction()
    {
        return direction.normalized;
    }
}
