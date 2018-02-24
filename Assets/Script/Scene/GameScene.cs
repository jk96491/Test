using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {

    [SerializeField]
    private Character playerCharacter = null;
    [SerializeField]
    private Transform[] wayPointTrans = null;
    [SerializeField]
    private int currentWayPointIndex = 0;

    void Start ()
    {
        AllSceneManager.Instance.Initialized(AllSceneManager.SceneType.GAME);
        InitPlayerCharacater();
    }

    private void InitPlayerCharacater()
    {
        Time.timeScale = 0;
        int characterID = UserManager.Instance.localUser.FindPartyCharacterByIndex(0);

        if (GameData.INVALID_ID != characterID)
        {
            CharacterRecord.CharacterInfo userInfo = GameData.Instance.characterRecord.FindCharacterInfoByID(characterID);

            if (null != userInfo)
            {
                GameObject charPrefab = ResourceUtil.RoadPrefab(userInfo.modelID);

                if (null != charPrefab)
                {
                    GameObject characterObj = Instantiate(charPrefab, wayPointTrans[currentWayPointIndex].position, wayPointTrans[currentWayPointIndex].rotation);
                    currentWayPointIndex++;

                    playerCharacter = characterObj.GetComponent<Character>();

                    if (null != playerCharacter)
                    {
                        playerCharacter.SetCameraPos(Character.CameraPosType.VIEW1,Camera.main.transform, true, 0.5f, null);
                    }
                }
            }
        }
    }

    private void EndCameraMove()
    {
        Time.timeScale = 1;
        playerCharacter.GoToWayPoint(wayPointTrans[currentWayPointIndex++]);
    }

    public void Update()
    {
        playerCharacter.UpdateCharacter();
    }

    private void OnGUI()
    {
        /*
        if (GUI.Button(new Rect(10, 10, 50, 50), "Camera"))
            playerCharacter.SetCameraPos((Character.CameraPosType)Random.Range(0, 3),Camera.main.transform, false);
            */
        if (GUI.Button(new Rect(10, 10, 50, 50), "Camera"))
            playerCharacter.SetCameraPos(Character.CameraPosType.VIEW3, Camera.main.transform, false, 1.5f);
    }
}
