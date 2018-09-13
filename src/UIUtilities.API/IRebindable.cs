
namespace UIUtilities.API
{
    public interface IRebindable<TDto>
    {
        void Rebind(TDto newBinding);
    }
}
