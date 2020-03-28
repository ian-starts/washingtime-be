using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WashingTime.Dtos;
using WashingTime.Entities.WashingTime;
using WashingTime.QueryExtensions.Include;
using WashingTime.QueryExtensions.Pagination;

namespace WashingTime.Controllers
{
    [ApiController]
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
            [FromQuery] string include
        )
        {
            return _repository.Query.IncludeMany(include).Paginate(pagination.Page, pagination.PageSize);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Entities.WashingTime.WashingTime> Store([FromBody] WashingTimeDto washingTimeDto)
        {
            var helpRequest = _mapper.Map<Entities.WashingTime.WashingTime>(washingTimeDto);
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
    }
}