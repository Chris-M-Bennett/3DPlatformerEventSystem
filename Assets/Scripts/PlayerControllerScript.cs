using EventSystem;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour{
    [SerializeField][Tooltip("Sets speed player translates at")]
    private float speed = 10f;
    private  Vector3 _eulerAngleVelocity;
    [Header("References player's rigidbody component")]
    private Rigidbody _rb;
    [Header("Declares speeds for player rotating")]
    public float rotationSpeed = 10f;
        
    void Start(){
        _eulerAngleVelocity = Vector3.forward;//Sets axis for rigidbody rotates in
        _rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        _eulerAngleVelocity.y += moveHorizontal * rotationSpeed;
        Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity);
        _rb.MoveRotation(deltaRotation);
        _rb.AddForce(transform.forward * (speed * moveVertical));
    }

    /// <summary>
    /// Triggers the events for different types of collectible by checking their tags
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag($"Collectible")){
            Destroy(other.gameObject);
            EventManager.Instance.TriggerEvent("collected");
        }else if(other.gameObject.CompareTag($"TimePickup")){
            Destroy(other.gameObject);
            EventManager.Instance.TriggerEvent("moreTime");
        }else if(other.gameObject.CompareTag($"HealthPickup")){
            Destroy(other.gameObject);
            EventManager.Instance.TriggerEvent("healthUp");
        }
    }
}
