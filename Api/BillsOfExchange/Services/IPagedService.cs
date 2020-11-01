using BillsOfExchange.Dto;

namespace BillsOfExchange.Services
{
    public interface IPagedService<T>
    {
        PagedResultDto<T> GetList(PagedRequestDto request);
    }
}