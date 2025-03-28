using UnityEngine;

public class MouseAiming : MonoBehaviour
{
    private Camera mainCamera;
    private RaycastHit camRayHit;
    [SerializeField] private LayerMask rayLayerMask;
    private Vector3 cursorWorldPosition;
    public Vector3 CursorWorldPosition { get { return cursorWorldPosition; } }
    
    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (GameManager.Instance.FreezePlayer)
            return;
        
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(camRay.origin, camRay.direction * 99f, Color.blue);
        if (Physics.Raycast(camRay.origin, camRay.direction, out camRayHit, 99f, rayLayerMask))
            cursorWorldPosition = new(camRayHit.point.x, transform.position.y, camRayHit.point.z);

        transform.LookAt(cursorWorldPosition);
    }
    
    
    
    

    /*
    [Header("Viewport angle aim: ")]
    public Transform centreTransform;
    public Camera mainCamera;
    private int leftOrRight;
    
    private void Update()
    {
        
        
        // aiming by getting the viewport position of the player and finding the vector angle from that to the mouse position,
        // and using that to aim the gun in 3D.
        // this approach makes me feel good about my vector math knowledge, but it sucks dookie butt for actually aiming at stuff
        
        Vector2 screenCentre = mainCamera.WorldToScreenPoint(centreTransform.position);
        Vector2 mousePos = new(Input.mousePosition.x, Input.mousePosition.y);
        print(Vector2.Angle(-Vector2.up, screenCentre - mousePos));
        
        if ((int)mousePos.x >= screenCentre.x)
            leftOrRight = 1;
        else
            leftOrRight = -1;
        
        transform.localRotation = Quaternion.Euler(0f, Vector2.Angle(-Vector2.up, screenCentre - mousePos) * leftOrRight, 0f);
    }
    */
}
