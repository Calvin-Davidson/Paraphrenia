using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float walkingSpeed = 7.5f;
        [SerializeField] private float runningSpeed = 11.5f;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private float lookSpeed = 2.0f;
        [SerializeField] private float lookXLimit = 45.0f;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float groundCheckDistance = 0.1f;

        private Rigidbody _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX;

        private void Start()
        {
            _characterController = GetComponent<Rigidbody>();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
         
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical");
            float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal");
            _moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            
            _characterController.velocity = new Vector3(_moveDirection.x, _characterController.velocity.y, _moveDirection.z);

            _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        private bool IsGrounded()
        {
            Ray ray = new Ray(transform.position, Vector3.down * groundCheckDistance);
            bool didHit = Physics.Raycast(ray, out var hit, groundCheckDistance);

            if (!didHit) return false;
            return true;
            
        }
    }
}