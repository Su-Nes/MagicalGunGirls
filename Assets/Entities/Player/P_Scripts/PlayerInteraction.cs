using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private Transform interactIcon;
    
    [SerializeField] private float sphereCastDistance = 1f;
    [SerializeField] private float sphereCastRadius = 2f;
    [SerializeField] private LayerMask layerMask;

    private Vector3 direction;
    private Interactable currentInteractable;
    

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        if (playerMovement.IsMoving)
            direction = playerMovement.Direction();

        Vector3 sphereOrigin = transform.position + direction * sphereCastDistance;
        Debug.DrawLine(sphereOrigin - direction * sphereCastRadius, sphereOrigin + direction * sphereCastRadius, Color.green);
        // get interactable and interact if it's done been gotten
        currentInteractable = GetInteractable(sphereOrigin, sphereCastRadius);
        if (currentInteractable is not null)
        {
            interactIcon.gameObject.SetActive(true);
            interactIcon.position = currentInteractable.InteractIconPosition();
            
            if (Input.GetButtonDown("Interact")) // interact!!
                currentInteractable.OnInteract();
        }else
            interactIcon.gameObject.SetActive(false); // disable focus object if no interaction possible
    }

    private Interactable GetInteractable(Vector3 sphereCentre, float radius)
    {
        // get interactables and sort them by distance
        Collider[] interactableObjects = Physics.OverlapSphere(sphereCentre, radius, layerMask);
        interactableObjects = interactableObjects.OrderBy(c => (sphereCentre - c.transform.position).sqrMagnitude).ToArray();
        if (interactableObjects.Length == 0)
        {
            currentInteractable = null;
            interactIcon.gameObject.SetActive(false);
            return null; // don't do shit if no interactables found
        }
        
        // get closest interactable and focus on it
        return interactableObjects[0].GetComponent<Interactable>();
    }
}
