namespace Mechadroids {
    /// <summary>
    /// Main interface template that can be implemented by any sort of entity handler
    /// </summary>
    public interface IEntityHandler {
        public IEntityState CurrentPlayerState { get; set; }
        public void Initialize();
        public void Tick();
        public void PhysicsTick();
        public void LateTick();
        public void Dispose();
        public void SwitchState(IEntityState nextState);
    }
}
