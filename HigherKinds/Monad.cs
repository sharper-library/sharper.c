namespace Sharper.C.HigherKinds
{
    public interface IMonad<T>
        : ISemimonad<T>
        , IApplicative<T>
        where T : struct, IMonad<T>
    {
    }
}
