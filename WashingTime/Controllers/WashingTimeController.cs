using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WashingTime.Dtos;
using WashingTime.Dtos.Filters;
using WashingTime.Entities.WashingTime;
using WashingTime.QueryExtensions.Pagination;
using WashingTime.Infrastructure.Filters;
using WashingTimeFilter = WashingTime.Dtos.Filters.WashingTimeFilter;

namespace WashingTime.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WashingTimeController : ControllerBase
    {
        private readonly IWashingTimeRepository _repository;
        private readonly IMapper _mapper;

        public WashingTimeController(IWashingTimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public PaginatedResult<Entities.WashingTime.WashingTime> Get(
            [FromQuery] PaginationDto pagination,
            [FromQuery(Name = "")] WashingTimeFilter filter,
            [FromQuery] string include
        )
        {
            return _repository.Query.ApplyFilters(filter).Paginate(pagination.Page, pagination.PageSize);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Entities.WashingTime.WashingTime> Store([FromBody] WashingTimeDto washingTimeDto)
        {
            var helpRequest = _mapper.Map<Entities.WashingTime.WashingTime>(washingTimeDto);
            helpRequest.UserId =
                User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            _repository.Add(helpRequest);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return helpRequest;
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Entities.WashingTime.WashingTime> Update(Guid id, [FromBody] WashingTimeDto washingTimeDto)
        {
            var helpRequest = _mapper.Map<Entities.WashingTime.WashingTime>(washingTimeDto);
            helpRequest.Id = id;
            _repository.Update(helpRequest);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return helpRequest;
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Entities.WashingTime.WashingTime> Delete(Guid id)
        {
            var washingTime = await _repository.GetAsync(id);
            await _repository.DeleteAsync(washingTime);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return washingTime;
        }
    }
}