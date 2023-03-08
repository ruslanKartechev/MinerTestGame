namespace Game.Cam
{
    public struct CameraShakeArg
    {
        public CameraShakeArg(float duration, float magnitude)
        {
            Duration = duration;
            Magnitude = magnitude;
        }

        public float Duration;
        public float Magnitude;
        
        public static CameraShakeArg Small => new CameraShakeArg(0.2f, 0.5f);
        public static CameraShakeArg Mid => new CameraShakeArg(0.3f, 0.75f);
        public static CameraShakeArg Hard => new CameraShakeArg(0.4f, 1f);

    }
}