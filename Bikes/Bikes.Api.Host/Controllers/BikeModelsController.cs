using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for managing bike models
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(
    IBikeModelService service,
    IBikeService bikeService,
    ILogger<BikeModelsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all bike models
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BikeModelDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<BikeModelDto>>> GetAll()
    {
        try
        {
            var result = await service.GetBikeModels();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting bike models");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about bike model by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BikeModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> Get(int id)
    {
        try
        {
            var entity = await service.GetBikeModel(id);
            if (entity == null)
            {
                logger.LogWarning("Bike model with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns bikes for this model
    /// </summary>
    [HttpGet("{id:int}/bikes")]
    [ProducesResponseType(typeof(List<BikeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<BikeDto>>> GetBikes(int id)
    {
        try
        {
            var model = await service.GetBikeModel(id);
            if (model == null)
            {
                logger.LogWarning("Bike model with id {Id} not found when getting bikes", id);
                return NotFound();
            }

            var bikes = await bikeService.GetBikesByModelId(id);
            return Ok(bikes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting bikes for bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new bike model
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BikeModelDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> Create([FromBody] BikeModelCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for bike model creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateBikeModel(dto);
            var createdEntity = await service.GetBikeModel(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for bike model creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating bike model");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update bike model by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BikeModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BikeModelDto>> Update(int id, [FromBody] BikeModelCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for bike model update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateBikeModel(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Bike model with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for bike model update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete bike model by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteBikeModel(id);
            if (!deleted)
            {
                logger.LogWarning("Bike model with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting bike model with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}