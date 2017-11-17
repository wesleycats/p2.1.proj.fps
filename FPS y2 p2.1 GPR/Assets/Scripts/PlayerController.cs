using UnityEngine;

[RequireComponent(typeof(PlayerEngine))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    private PlayerEngine engine;
    private InputManager input;
    private ConfigurableJoint joint;

    [SerializeField]
    private float speedMulti = .2f;
    [SerializeField]
    private float maxSpeed = 4f;
    [SerializeField]
    private float sensitivity = 2f;
    [SerializeField]
    private float jumpForce = 5f;
    private float currentSpeed = 5;
    private bool moving;
    private float speed = 4;
    private bool _shooting;

    void Start()
    {
        engine = GetComponent<PlayerEngine>();
        joint = GetComponent<ConfigurableJoint>();
        input = GameObject.Find("GameManager").GetComponent<InputManager>();
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        Vector3 movHorizontal = this.transform.right * input.xMov();
        Vector3 movVertical = this.transform.forward * input.zMov();
        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed;

        float _cameraRotationX = input.xRot() * sensitivity;
        Vector3 _rotation = new Vector3(0f, input.yRot(), 0f) * sensitivity;
        Vector3 _jumpUp = ((this.transform.up) * input.jump()) * jumpForce;

        _shooting = input.shoot() != 0;
        moving = input.zMov() != 0 || input.xMov() != 0;
        if(moving)
        {
            speed += 0.5f;
        }
        else if(!moving)
        {
            speed = engine.Approach(speed, 0f, 20 * Time.deltaTime);
        }
        speed = Mathf.Clamp(speed, -5, 5);

        engine.Shoot(_shooting);
        engine.Move(_velocity);
        engine.Jump(_jumpUp);
        engine.Rotate(_rotation);
        engine.RotateCamera(_cameraRotationX);
        //print(_shooting);
    }
}
