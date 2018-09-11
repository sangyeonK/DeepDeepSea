using DeepDeepSeaSystem;

class Global
{
    private static Global _instance;
    public static Global Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Global();
            }
            return _instance;
        }
    }

    private LocalPlayHistoryManager _localPlayHistoyManager;
    public LocalPlayHistoryManager LocalPlayHistoryManager
    {
        get
        {
            return _localPlayHistoyManager;
        }
    }

    private TemporarySavedDataManager _temporarySavedDataManager;
    public TemporarySavedDataManager TemporarySavedDataManager
    {
        get
        {
            return _temporarySavedDataManager;
        }
    }

    private Global()
    {
        _localPlayHistoyManager = new LocalPlayHistoryManager();
        _temporarySavedDataManager = new TemporarySavedDataManager();
    }

}