namespace Common
{
    public class Timer
    {
        private float currentTime;

        public bool IsFinished => currentTime <= 0;


        public Timer(float startTime)
        {
            Start(startTime);
        }

        public void Start(float startTime)
        {
            currentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (currentTime <= 0) return;

            currentTime -= deltaTime;
        }
    }
}