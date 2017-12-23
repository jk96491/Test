using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExhibition : MonoBehaviour
{
    [SerializeField]
    private Transform[] posTrans = null;
    [SerializeField]
    private Transform mainCameraPos = null;
    [SerializeField]
    private Transform[] characterCarmeraTrans = null;
    [SerializeField]
    private GameObject[] Characters = null;
    [SerializeField]
    private Transform mainCarmeraTrans = null;

    public void SetCharacter(int index_, int characterID_)
    {
        if(GameData.INVALID_ID == characterID_)
        {
            if(null != Characters[index_])
            {
                Characters[index_].SetActive(false);
            }
        }
        else
        {
            if (null != Characters[index_])
            {
                Destroy(Characters[index_]);
            }
            CharacterRecord.CharacterInfo charInfo = GameData.Instance.characterRecord.FindCharacterInfoByID(characterID_);

            if (null != charInfo)
            {
                GameObject charPrefab = ResourceUtil.RoadPrefab(charInfo.modelID);
                if (null == charPrefab)
                    return;

                Characters[index_] = Instantiate(charPrefab, posTrans[index_].localPosition, posTrans[index_].localRotation);
                if (null == Characters[index_])
                    return;

                if (null != posTrans[index_])
                    Characters[index_].transform.SetParent(posTrans[index_]);
                Characters[index_].SetActive(true);
            }
        }
        CameraManager.Instance.CameraMoving(mainCarmeraTrans.localPosition, mainCameraPos.localPosition, mainCarmeraTrans, 5f);
    }

    public void FocusCharacter(int index_)
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);

            if(i == index_)
            {
                Characters[i].SetActive(true);
            }
        }
        CameraManager.Instance.CameraMoving(mainCarmeraTrans.localPosition, characterCarmeraTrans[index_].localPosition, mainCarmeraTrans, 5f);
    }
}
