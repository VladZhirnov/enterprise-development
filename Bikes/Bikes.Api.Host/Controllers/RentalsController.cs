using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for managing rentals
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController(
    IRentalService service,
    ILogger<RentalsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all rentals
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<RentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalDto>>> GetAll()
    {
        try
        {
            var result = await service.GetRentals();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rentals");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about rental by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Get(int id)
    {
        try
        {
            var entity = await service.GetRental(id);
            if (entity == null)
            {
                logger.LogWarning("Rental with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rental with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all rentals for a specific bike
    /// </summary>
    [HttpGet("bike/{bikeId:int}")]
    [ProducesResponseType(typeof(List<RentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalDto>>> GetRentalsByBike(int bikeId)
    {
        try
        {
            var rentals = await service.GetRentalsByBikeId(bikeId);
            if (rentals.Count == 0)
            {
                logger.LogWarning("No rentals found for bike with id {BikeId}", bikeId);
                return NotFound();
            }
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rentals for bike with id {BikeId}", bikeId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all rentals for a specific client
    /// </summary>
    [HttpGet("client/{clientId:int}")]
    [ProducesResponseType(typeof(List<RentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalDto>>> GetRentalsByClient(int clientId)
    {
        try
        {
            var rentals = await service.GetRentalsByClientId(clientId);
            if (rentals.Count == 0)
            {
                logger.LogWarning("No rentals found for client with id {ClientId}", clientId);
                return NotFound();
            }
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rentals for client with id {ClientId}", clientId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new rental
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RentalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Create([FromBody] RentalCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for rental creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateRental(dto);
            var createdEntity = await service.GetRental(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for rental creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating rental");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update rental by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RentalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RentalDto>> Update(int id, [FromBody] RentalCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for rental update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateRental(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Rental with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for rental update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating rental with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete rental by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteRental(id);
            if (!deleted)
            {
                logger.LogWarning("Rental with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting rental with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}