namespace LTFUtils
{
    public class RangeTimerBehaviour : TimerBehaviour<RangeTimer> 
    {
        private void Start() => _timer.Init();
    }
}