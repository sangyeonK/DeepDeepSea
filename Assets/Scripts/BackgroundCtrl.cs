using System.Collections;
using System.Collections.Generic;
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
    private Animator bgAnimator;
    private int bgAnimatorParamID_playerVerticalSpeed;

    private AreaChanger areaChanger;


    void Awake()
    {
        bgAnimator = GetComponent<Animator>();
        bgAnimatorParamID_playerVerticalSpeed = Animator.StringToHash("playerVerticalSpeed");
    }

    void Start () {

        areaChanger = new AreaChanger(stageAreas.Length, OnAreaChangeSignal);

        // 초기 스테이지1 세팅
        for (int i = 0; i < stageAreas.Length; i++)
        {
            GameObject bgStageClone = Instantiate(stagePrefabs.stage_1, gameObject.transform);
            bgStageClone.transform.localPosition = stageAreas[i].transform.localPosition;
            Object.Destroy(stageAreas[i]);
            stageAreas[i] = bgStageClone;
            
        }

        ChangeStage(1);

        StartCoroutine(StageChange());

    }

    void Update()
    {
        // 배경 애니메이션 재생 ( 플레이어 속도에 영향 )
        bgAnimator.SetFloat(bgAnimatorParamID_playerVerticalSpeed, GameManager.Instance.PlayerVerticalSpeed);

        areaChanger.Update(gameObject.transform.localPosition.y);
    }

    IEnumerator StageChange()
    {
        yield return new WaitForSeconds(stageChangeTime);

        ChangeStage(currStage+1);

        StartCoroutine(StageChange());
    }
	
    void ChangeStage(int stage)
    {
        if (stage > MAX_STAGE)
        {
            return;
        }

        GameObject stageChangePrefab = null;

        switch (stage)
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


        if (stageChangePrefab)
        {
            
            GameObject currentArea = stageAreas[GetCurrentStageAreaIndex()];
            GameObject newObject = Instantiate(stageChangePrefab, currentArea.transform.position, Quaternion.identity);
            newObject.transform.Translate(new Vector2(0.0f, BACKGROUND_HEIGHT * -1f));

            // TODO : 임시로 스크린 윗부분에 이미지를 붙였는데, 좀 더 깔끔한 처리가 필요함
            GameObject tempObject = Instantiate(GetCurrentStagePrefab(), currentArea.transform.position, Quaternion.identity);
            tempObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            foreach (Transform child in tempObject.transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        currStage = stage;

        areaChanger.EnableAreaChangeSignal();

    }

    void OnAreaChangeSignal()
    {
        // 다음 area 와 그다음 area 2개의 이미지를 교체한다.
        GameObject stagePrefab = GetCurrentStagePrefab();
        
        int nextAreaIndex = GetNextStageAreaIndex();
        if (stageAreas[nextAreaIndex].GetComponent<BGStage>().stageNumber != stagePrefab.GetComponent<BGStage>().stageNumber) {
            GameObject stageClone = Instantiate(stagePrefab, gameObject.transform);
            stageClone.transform.localPosition = stageAreas[nextAreaIndex].transform.localPosition;
            Object.Destroy(stageAreas[nextAreaIndex]);
            stageAreas[nextAreaIndex] = stageClone;
        }


        int nnextAreaIndex = nextAreaIndex >= stageAreas.Length - 1 ? 0 : nextAreaIndex + 1;
        if (stageAreas[nnextAreaIndex].GetComponent<BGStage>().stageNumber != stagePrefab.GetComponent<BGStage>().stageNumber)
        {
            GameObject stageClone = Instantiate(stagePrefab, gameObject.transform);
            stageClone.transform.localPosition = stageAreas[nnextAreaIndex].transform.localPosition;
            Object.Destroy(stageAreas[nnextAreaIndex]);
            stageAreas[nnextAreaIndex] = stageClone;
        }
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

    int GetCurrentStageAreaIndex()
    {
        float y = gameObject.transform.localPosition.y;
        int index = Mathf.FloorToInt(y / BACKGROUND_HEIGHT);

        if (index >= stageAreas.Length)
            index = stageAreas.Length - 1;

        return index;
    }

    int GetNextStageAreaIndex()
    {
        float y = gameObject.transform.localPosition.y;
        int index = 1;

        while (y > 0.0f)
        {
            index++;
            if (index >= stageAreas.Length)
                index = 0;
            y -= BACKGROUND_HEIGHT;
        }

        return index;
    }
}

class AreaChanger
{
    private float areaRewindHeight;
    private int areaLength;
    private int areaChangeCount;

    private float startPositionY;
    private float signalLocalPositionY;
    private float lastLocalPositionY;

    private bool enableAreaChangeSignal;
    public delegate void SignalHandler();
    private SignalHandler signalHandler;
    public AreaChanger(int areaLength, SignalHandler signalHandler)
    {
        areaRewindHeight = BackgroundCtrl.BACKGROUND_HEIGHT * (areaLength - 1);
        this.areaLength = areaLength;
        this.signalHandler = signalHandler;

    }
    public void EnableAreaChangeSignal()
    {
        enableAreaChangeSignal = true;
        signalLocalPositionY = lastLocalPositionY + BackgroundCtrl.BACKGROUND_HEIGHT;
        if(signalLocalPositionY >= areaRewindHeight)
        {
            signalLocalPositionY -= areaRewindHeight;
        }
        areaChangeCount = areaLength - 1;       // signalHandler 를 1번 호출했으므로 -1
        signalHandler();
        
    }

    public void Update(float currLocalPositionY)
    {
        if(enableAreaChangeSignal)
        {
            if (IsEnteredNextArea(currLocalPositionY))
            {
                signalHandler();

                areaChangeCount--;
                if (areaChangeCount == 0)
                {
                    enableAreaChangeSignal = false;
                }
            }
        }

        lastLocalPositionY = currLocalPositionY;
    }

    private bool IsEnteredNextArea(float currLocalPositionY)
    {
        int a = Mathf.FloorToInt(lastLocalPositionY / BackgroundCtrl.BACKGROUND_HEIGHT);
        int b = Mathf.FloorToInt(currLocalPositionY / BackgroundCtrl.BACKGROUND_HEIGHT);

        return a != b ? true : false;
    }
}
