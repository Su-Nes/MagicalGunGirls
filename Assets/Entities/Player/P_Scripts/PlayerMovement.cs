using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float velocityDampenLerpOnMove = .1f;
    private Rigidbody rb;
    private Vector3 direction;
    
    private float freeVelocityTime;
    
    private bool isMoving;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        isMoving = direction.magnitude > 0f;
    }
    
    private void FixedUpdate()
    {
        if (freeVelocityTime > 0f)
            freeVelocityTime -= Time.deltaTime;
        else
            DampenRBVelocity(velocityDampenLerpOnMove);
        
        Move(direction * (moveSpeed * Time.fixedDeltaTime));
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
    
    public bool IsMoving()
    {
        return isMoving;
    }
    
    public Vector3 Direction()
    {
        return direction.normalized;
    }
}
