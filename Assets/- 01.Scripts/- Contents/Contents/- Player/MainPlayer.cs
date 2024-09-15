using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    [Header("---------- Values ----------")]
    [Range(1, 20)]
    public int Power = 3;
    [SerializeField] private bool IntheSphere = false;

    [Header("---------- Reference ----------")]
    [SerializeField] public GameObject character;
    public GameObject MainChar { get { return character; } }
    [SerializeField] private PlayerPosition _position;


    [Header("---------- HP ----------")]
    private int PlayerHP = 100;
    [SerializeField] private Image _hpUI;


    private Rigidbody rigidbody;
    private Animator animator;
    private Vector3 InputVect;

    private Vector3 OnSize = new Vector3(1, 1, 1);
    private Vector3 OnPos = new Vector3(0, 2.5f, 0);


    private Vector3 InSize = new Vector3(2, 2, 2);
    private Vector3 InPos = new Vector3(0, -2.5f, 0);

    private Vector2 PlayerVec = Vector2.zero;

    private Transform _camera;
    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    #region Unity LifeCycle
    void Start()
    {
        animator = character.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
#if UNITY_EDITOR
        ControllerPC();
#endif
        _position.Value = transform.position;
        if (InputVect != Vector3.zero)
        {

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        _hpUI.transform.parent.LookAt(_hpUI.transform.parent.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
    }

    private void ControllerPC()
    {
        InputVect.x = Input.GetAxisRaw("Horizontal");
        InputVect.z = Input.GetAxisRaw("Vertical");
    }

    public void SetVector(Vector2 joy)
    {
        InputVect.x = joy.x;
        InputVect.z = joy.y;
    }



    private void FixedUpdate()
    {
        if (InputVect != Vector3.zero)
        {
            if (rigidbody.velocity.magnitude < 10)
                rigidbody.AddForce(InputVect * Power);
        }

        CharacterPos();
    }


    public void GetDamage(int damage)
    {
        if (PlayerHP > 0)
        {
            InGameUI.Instance.HitVolume();
            PlayerHP -= damage;
            _hpUI.fillAmount = PlayerHP * 0.01f;
        }
        else
        {
            InGameUI.Instance.GameOver();
            Debug.Log("Dead");
        }
    }

    private bool isInvic = false;
    private Coroutine _invincibilityCor = null;

    private void StartInvincibilityTime()
    {
        if (_invincibilityCor != null)
        {
            StopCoroutine(_invincibilityCor);
            _invincibilityCor = null;
        }

        _invincibilityCor = StartCoroutine(Invincibility());
    }

    IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(0.75f);
        isInvic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseCointObject>())
        {
            //BaseObjectPool.Instance.ReturnCoin(other.GetComponent<BasePoolingObject>());
        }

    }

    #endregion Unity LifeCycle

    #region Method
    private void CharacterPos()
    {
        character.transform.position = this.transform.position + (IntheSphere ? InPos : OnPos);

        // 구의 회전 값으로 캐릭터의 회전 값을 설정하여 같이 회전하도록 함
        if (rigidbody.velocity != Vector3.zero)
            character.transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }
    #endregion UMethod
}
