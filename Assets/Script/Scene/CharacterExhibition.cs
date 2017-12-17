using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExhibition : MonoBehaviour
{
    [SerializeField]
    private Transform[] posTrans = null;
    [SerializeField]
    private GameObject[] Characters = null;

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
            }
        }
    }
}
