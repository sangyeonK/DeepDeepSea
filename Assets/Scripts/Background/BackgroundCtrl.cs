using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour {
    
    public const int MAX_STAGE = 5;
    public const float BACKGROUND_HEIGHT = 19.2f;

    public GameObject screenObject;

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

    public GameObject[] stageAreas = new GameObject[4];

    public float stageChangeTime = 5.0f;

    private int currStage = 0;
    private int currArea = 0;
    private float posY_backgroundRewind;
    private bool activateStageChange = false;
   
    void Start () {

        posY_backgroundRewind = stageAreas.Last().transform.position.y;
        
        // 초기 스테이지1 세팅
        for (int i = 0; i < stageAreas.Length; i++)
        {
            GameObject bgStageClone = Instantiate(stagePrefabs.stage_1, gameObject.transform);
            bgStageClone.transform.localPosition = stageAreas[i].transform.localPosition;
            bgStageClone.GetComponent<BGStage>().AreaIndex = i;
            Object.Destroy(stageAreas[i]);
            stageAreas[i] = bgStageClone;
            
        }

        ChangeStage(1);

        StartCoroutine(StageChange());

    }

    private void OnValidate()
    {
        if(stageAreas.Length < 3)
        {
            Debug.LogWarning("`StageAreas` size should be more than 3");
            System.Array.Resize(ref stageAreas, 3);
        }
        
    }

    void Update()
    {
        if(screenObject.transform.position.y <= posY_backgroundRewind)
        {
            RewindBackground();
        }
    }

    IEnumerator StageChange()
    {
        yield return new WaitForSeconds(stageChangeTime);

        ChangeStage(currStage+1);

        StartCoroutine(StageChange());
    }
    	
    void OnStageAreaEnter(int areaIndex)
    {
        currArea = areaIndex;
        
        GameObject stagePrefab = GetCurrentStagePrefab();

        if (stageAreas[areaIndex].GetComponent<BGStage>().stageNumber != stagePrefab.GetComponent<BGStage>().stageNumber)
        {
            GameObject stageClone = Instantiate(stagePrefab, gameObject.transform);
            stageClone.transform.localPosition = stageAreas[areaIndex].transform.localPosition;
            stageClone.GetComponent<BGStage>().AreaIndex = areaIndex;
            Object.Destroy(stageAreas[areaIndex]);
            stageAreas[areaIndex] = stageClone;
        }

        if (activateStageChange)
        {

            GameObject stageChangePrefab = GetStageChangePrefab(currStage);
            GameObject newObject = Instantiate(stageChangePrefab,
                new Vector2(stageAreas[areaIndex].transform.position.x, stageAreas[areaIndex].transform.position.y),
                Quaternion.identity);


            activateStageChange = false;
        }


    }

    void ChangeStage(int stage)
    {
        if (stage >= MAX_STAGE)
        {
            return;
        }

        if (GetStageChangePrefab(stage))
        {
            activateStageChange = true;
        }

        currStage = stage;

    }


    GameObject GetCurrentStagePrefab()
    {
        GameObject stagePrefab = null;

        switch (currStage)
        {
            case 1:
                stagePrefab = this.stagePrefabs.stage_1;
                break;
            case 2:
                stagePrefab = this.stagePrefabs.stage_2;
                break;
            case 3:
                stagePrefab = this.stagePrefabs.stage_3;
                break;
            case 4:
                stagePrefab = this.stagePrefabs.stage_4;
                break;
            case 5:
                stagePrefab = this.stagePrefabs.stage_5;
                break;
            default:
                break;
        }

        return stagePrefab;
    }

    GameObject GetStageChangePrefab(int newStage)
    {
        GameObject stageChangePrefab = null;

        switch (newStage)
        {
            case 2:
                stageChangePrefab = this.stageChangePrefabs.stage_1_to_2;
                break;
            case 3:
                stageChangePrefab = this.stageChangePrefabs.stage_2_to_3;
                break;
            case 4:
                stageChangePrefab = this.stageChangePrefabs.stage_3_to_4;
                break;
            case 5:
                stageChangePrefab = this.stageChangePrefabs.stage_4_to_5;
                break;
            default:
                break;
        }

        return stageChangePrefab;
    }

    void RewindBackground()
    {
        float moveY = ( stageAreas.Length - 1 ) * BACKGROUND_HEIGHT * -1f;
        gameObject.transform.Translate(new Vector2(0.0f, moveY));
        posY_backgroundRewind = stageAreas.Last().transform.position.y;

        OnStageAreaEnter(0);
    }
}