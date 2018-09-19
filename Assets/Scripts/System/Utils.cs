namespace DeepDeepSeaSystem
{
    public class Utils
    {
        public static string MakeTimeString(int sec)
        {
            return string.Format("{0:D2}:{1:D2}", (int)(sec / 60), sec % 60);
        }
    }
}
