using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    // �J�����̈ʒu���������邽�߂̃X�N���v�g�BCameraTest�����邯�ǂ��������قڃ��C��

    public List<Transform> players = new List<Transform>();
    // ���݂̃v���C���[���𐔂��邽�߂̃��X�g�B10/10���_��GameManager�ɂ܂Ƃ߂��ق��������ƍl���Ă���B

    float height;
    // �J�����̍���

    private void Update()
    {
        Vector3 pos = Vector3.zero;
        // �v�Z�p�̃��[�J��Vector3��ݒ�

        foreach(Transform t in players)
        {
            pos += t.position;
            // �v���C���[�̍��W��pos�ɑ����Ă���
        }

        Vector3 center = pos / players.Count;
        // ���������W���v���C���[�̐��Ŋ����đS���̈ʒu�̕��ς����߂�

        transform.position = center;
        // ���߂����ς̍��W���I�u�W�F�N�g�ɓK�p����

        height = 0;
        // ��x���������Z�b�g����

        foreach(Transform t in players)
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
}
