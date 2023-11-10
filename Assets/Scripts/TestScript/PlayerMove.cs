using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // �v���C���[�̈ړ��p�̃X�N���v�g
    public float speed = 1;

    PlayerInput playerInput;

    float x;
    float z;

    public Animator animator;

    public bool loading;
    float loadingTimer;

    // �ő�̉�]�p���x[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // �i�s�����Ɍ����̂ɂ����邨���悻�̎���[s]
    [SerializeField] private float _smoothTime = 0.1f;

    // �I�u�W�F�N�g�̐���
    [SerializeField] private Vector3 _forward = Vector3.forward;

    // �I�u�W�F�N�g�̏����
    [SerializeField] private Vector3 _up = Vector3.up;

    // ��]��
    [SerializeField] private Vector3 _axis = Vector3.up;

    private Transform _transform;

    // �O�t���[���̃��[���h�ʒu
    private Vector3 _prevPosition;

    private float _currentAngularVelocity;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        _transform = transform;

        _prevPosition = _transform.position;
    }
    
    void Update()
    {
        if (loading)
        {
            loadingTimer += Time.deltaTime;
            if(loadingTimer >= 3)
            {
                loadingTimer = 0;
                loading = false;
            }

            return;
        }

#if UNITY_EDITOR
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
#else
        x = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>().x;
        z = playerInput.currentActionMap.FindAction("Move").ReadValue<Vector2>().y;
#endif
        Vector3 movement = new Vector3(x * speed, 0, z * speed);

        gameObject.GetComponent<Rigidbody>().velocity = movement;

        if (movement != Vector3.zero) animator.SetBool("Walk", true);
        else animator.SetBool("Walk", false);

        
        // �L�����̉�]���@�̓R�s�y�ł�


        // ���݃t���[���̃��[���h�ʒu
        var position = _transform.position;

        // �ړ��ʂ��v�Z
        var delta = position - _prevPosition;

        // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
        _prevPosition = position;

        // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
        if (delta == Vector3.zero)
            return;

        // ��]�␳�v�Z
        var offsetRot = Quaternion.Inverse(Quaternion.LookRotation(_forward, _up));

        // ���[���h��Ԃ̑O���x�N�g���擾
        var forward = _transform.TransformDirection(_forward);

        // ��]���Ɛ����ȕ��ʂɓ��e�����x�N�g���v�Z
        var projectFrom = Vector3.ProjectOnPlane(forward, _axis);
        var projectTo = Vector3.ProjectOnPlane(delta, _axis);

        // ������̊p�x�������߂�
        var diffAngle = Vector3.Angle(projectFrom, projectTo);

        // ���݃t���[���ŉ�]����p�x�̌v�Z
        var rotAngle = Mathf.SmoothDampAngle(
            0,
            diffAngle,
            ref _currentAngularVelocity,
            _smoothTime,
            _maxAngularSpeed
        );

        // ������ł̉�]�̊J�n�ƏI�����v�Z
        var lookFrom = Quaternion.LookRotation(projectFrom);
        var lookTo = Quaternion.LookRotation(projectTo);

        // ���݃t���[���ɂ������]���v�Z
        var nextRot = Quaternion.RotateTowards(lookFrom, lookTo, rotAngle) * offsetRot;

        // �I�u�W�F�N�g�̉�]�ɔ��f
        _transform.rotation = nextRot;
    }
}
