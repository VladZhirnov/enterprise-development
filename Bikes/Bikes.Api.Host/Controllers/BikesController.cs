using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for managing bikes
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController(
    IBikeService service,
    IRentalService rentalService,
    ILogger<BikesController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all bikes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<BikeDto>>> GetAll()
    {
        try
        {
            var result = await service.GetBikes();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting bikes");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about bike by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BikeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> Get(int id)
    {
        try
        {
            var entity = await service.GetBike(id);
            if (entity == null)
            {
                logger.LogWarning("Bike with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all rentals for this bike
    /// </summary>
    [HttpGet("{id:int}/rentals")]
    [ProducesResponseType(typeof(List<RentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalDto>>> GetRentals(int id)
    {
        try
        {
            var bike = await service.GetBike(id);
            if (bike == null)
            {
                logger.LogWarning("Bike with id {Id} not found when getting rentals", id);
                return NotFound();
            }

            var rentals = await rentalService.GetRentalsByBikeId(id);
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rentals for bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new bike
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BikeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> Create([FromBody] BikeCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for bike creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateBike(dto);
            var createdEntity = await service.GetBike(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for bike creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating bike");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update bike by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BikeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeDto>> Update(int id, [FromBody] BikeCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for bike update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateBike(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Bike with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for bike update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete bike by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteBike(id);
            if (!deleted)
            {
                logger.LogWarning("Bike with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting bike with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}