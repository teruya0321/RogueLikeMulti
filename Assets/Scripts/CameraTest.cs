using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // �J�����ɂ��Ă�����̃X�N���v�g�B����܂���Ȃ��C������
    public Transform target;
    // ���S�ƂȂ�I�u�W�F�N�g�̈ʒu���

    public TargetTest targetF;
    // ���S�̃I�u�W�F�N�g�ɂ������Ă�֐��B����擾����̂߂�ǂ���������public�����ǂ܂Ƃ߂Ă�������������Ȃ�

    void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position,target.position, Time.deltaTime * 2);
        // �J�������Ȃ߂炩�Ɉړ������邽�߂ɕϐ��ɖړI�̍��W�ƌ��݂̍��W��������

        pos.y = targetF.Height() + 5 - targetF.players.Count;
        // ���������߂�B�v���C���[�̐��ɂ���č����̉��������炷

        transform.position = pos;
        // ���������܂�����J�����̈ʒu�ɑ������
    }
}
