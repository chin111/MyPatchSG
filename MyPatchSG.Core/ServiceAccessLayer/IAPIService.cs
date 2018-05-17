namespace MyPatchSG.SAL
{
    public interface IAPIService
    {
        IMyPatchSGAPI Speculative { get; }
        IMyPatchSGAPI UserInitiated { get; }
        IMyPatchSGAPI Background { get; }
    }
}
