using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    // �J�����̈ʒu���������邽�߂̃X�N���v�g�BCameraTest�����邯�ǂ��������قڃ��C��


    List<Transform> playerTransform = new List<Transform>();

    float height;
    // �J�����̍���

    public void ChangePlayerCount(List<Transform> playerCount)
    {
        playerTransform = playerCount;
    }

    private void Update()
    {
        if(playerTransform.Count == 0) return;

        Vector3 pos = Vector3.zero;
        // �v�Z�p�̃��[�J��Vector3��ݒ�

        foreach(Transform t in playerTransform)
        {
            pos += t.position;
            // �v���C���[�̍��W��pos�ɑ����Ă���
        }

        Vector3 center = pos / playerTransform.Count;
        // ���������W���v���C���[�̐��Ŋ����đS���̈ʒu�̕��ς����߂�

        transform.position = center;
        // ���߂����ς̍��W���I�u�W�F�N�g�ɓK�p����

        height = 0;
        // ��x���������Z�b�g����

        center.z = center.z * 16 / 9;


        foreach(Transform t in playerTransform)
        {
            Vector3 tPos = new Vector3(t.position.x, t.position.y, t.position.z * 16 / 9);
            // �v���C���[�̈ʒu���v�Z����B�J�����̏c����̊֌W�ł߂�ǂ������v�Z���ɂȂ��Ă���B�������l������܂킩���ĂȂ�

            float f = Vector3.Distance(center, tPos);
            // �v���C���[�̈ʒu�ƒ��S�̈ʒu�̍����ׂ�

            if (f < 0) f *= -1;
            // ���̒l���}�C�i�X�ɂȂ�����v���X�ɕς���

            height = Mathf.Max(height,f);
            // ���S�����ԗ��ꂽ�v���C���[��T���B
        }
    }

    public float Height()
    {
        return height;
        // �֐����Ă΂ꂽ�獂���̒l��Ԃ�
    }

    public int PlayerCount()
    {
        return playerTransform.Count;
    }
}
