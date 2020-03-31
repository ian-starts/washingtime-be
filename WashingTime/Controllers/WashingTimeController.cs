using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using WashingTime.Dtos;
using WashingTime.Dtos.Filters;
using WashingTime.Entities.WashingTime;
using WashingTime.Exceptions;
using WashingTime.Identity;
using WashingTime.Infrastructure;
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
        private readonly IIdentityAccessor _identityAccessor;

        public WashingTimeController(
            IWashingTimeRepository repository,
            IMapper mapper,
            IIdentityAccessor identityAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _identityAccessor = identityAccessor;
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
            helpRequest.UserId = _identityAccessor.UserId;
            _repository.Add(helpRequest);
            try
            {
                await _repository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (DbUpdateException exception)
                when (exception.InnerException is PostgresException postgresException &&
                      postgresException.SqlState == SqlStateCodeStatic.UniqueViolation)
            {
                throw new UniqueConstraintViolationException("This time is already taken");
            }

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
            if (washingTime.UserId != _identityAccessor.UserId)
            {
                throw new EntityDeletionRestrictionException("You cannot delete another user's washingtime");
            }

            await _repository.DeleteAsync(washingTime);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            return washingTime;
        }
    }
}