using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour {

    public int maxStage = 5;
    public const float BACKGROUND_HEIGHT = 19.2f;
    public GameObject screenObject;

    public enum StageNumber
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    [System.Serializable]
    public struct StagePrefab
    {
        public GameObject stage_1;
        public GameObject stage_2;
        public GameObject stage_3;
        public GameObject stage_4;
        public GameObject stage_5;
    }

    [SerializeField]
    private StagePrefab stagePrefabs;

    [System.Serializable]
    public struct StageChangePrefab
    {
        public GameObject stage_1_to_2;
        public GameObject stage_2_to_3;
        public GameObject stage_3_to_4;
        public GameObject stage_4_to_5;
    }

    [SerializeField]
    private StageChangePrefab stageChangePrefabs;
    public float stageChangeTime = 15.0f;
    public float rockTranslateSpeed = 0.1f;
    private StageNumber currStageNumber;
    private GameObject currStageObject;

    private void Awake()
    {
        currStageNumber = StageNumber.Stage1;
        currStageObject = InstantiateStagePrefab(stagePrefabs.stage_1, new Vector2(0.0f, 0.0f));
    }
    // Use this for initialization
    void Start () {

        StartCoroutine(StageChange());
    }
	

    IEnumerator StageChange()
    {
        yield return new WaitForSeconds(stageChangeTime);

        ChangeStage(currStageNumber+1);

        StartCoroutine(StageChange());
    }

    void ChangeStage(StageNumber stage)
    {
        if (stage > StageNumber.Stage5)
        {
            return;
        }

        float offsetY = BACKGROUND_HEIGHT * -1;
        GameObject stageChangePrefab = GetStageChangePrefab(stage);
        if (stageChangePrefab)
        {
            InstantiateStagePrefab(stageChangePrefab,
                new Vector2(0.0f, screenObject.transform.position.y + offsetY));
            offsetY -= BACKGROUND_HEIGHT;
        }
        GameObject stagePrefab = GetStagePrefab(stage);
        if (stagePrefab)
        {
            currStageObject.GetComponent<BGStage>().SetDisable();
            currStageObject = InstantiateStagePrefab(stagePrefab,
                new Vector2(0.0f, screenObject.transform.position.y + offsetY));
        }

        currStageNumber = stage;
    }

    GameObject GetStagePrefab(StageNumber stage)
    {
        GameObject stagePrefab = null;

        switch (stage)
        {
            case StageNumber.Stage1:
                stagePrefab = this.stagePrefabs.stage_1;
                break;
            case StageNumber.Stage2:
                stagePrefab = this.stagePrefabs.stage_2;
                break;
            case StageNumber.Stage3:
                stagePrefab = this.stagePrefabs.stage_3;
                break;
            case StageNumber.Stage4:
                stagePrefab = this.stagePrefabs.stage_4;
                break;
            case StageNumber.Stage5:
                stagePrefab = this.stagePrefabs.stage_5;
                break;
            default:
                break;
        }

        return stagePrefab;
    }

    GameObject GetStageChangePrefab(StageNumber newStage)
    {
        GameObject stageChangePrefab = null;

        switch (newStage)
        {
            case StageNumber.Stage2:
                stageChangePrefab = this.stageChangePrefabs.stage_1_to_2;
                break;
            case StageNumber.Stage3:
                stageChangePrefab = this.stageChangePrefabs.stage_2_to_3;
                break;
            case StageNumber.Stage4:
                stageChangePrefab = this.stageChangePrefabs.stage_3_to_4;
                break;
            case StageNumber.Stage5:
                stageChangePrefab = this.stageChangePrefabs.stage_4_to_5;
                break;
            default:
                break;
        }

        return stageChangePrefab;
    }

    GameObject InstantiateStagePrefab(GameObject stagePrefab, Vector2 pos)
    {
        GameObject newObject = Instantiate(stagePrefab, pos, Quaternion.identity, gameObject.transform);
        BGStage BGStage = newObject.GetComponent<BGStage>();
        if(BGStage)
        {
            BGStage.SetRockTranslateSpeed(rockTranslateSpeed);
        }
        return newObject;
    }
}
