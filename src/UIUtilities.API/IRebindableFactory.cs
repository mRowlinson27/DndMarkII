
namespace UIUtilities.API
{
    public interface IRebindableFactory<TResult, TDto> where TResult : IRebindable<TDto>
    {
        TResult Create(TDto dto);
    }
}
