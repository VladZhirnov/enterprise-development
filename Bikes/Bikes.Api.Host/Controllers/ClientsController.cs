using Bikes.Application.Contracts.Dtos;
using Bikes.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for managing clients
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(
    IClientService service,
    IRentalService rentalService,
    ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Returns a list of all clients
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        try
        {
            var result = await service.GetClients();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting clients");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns information about client by id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Get(int id)
    {
        try
        {
            var entity = await service.GetClient(id);
            if (entity == null)
            {
                logger.LogWarning("Client with id {Id} not found", id);
                return NotFound();
            }
            return Ok(entity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting client with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Returns all rentals for this client
    /// </summary>
    [HttpGet("{id:int}/rentals")]
    [ProducesResponseType(typeof(List<RentalDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RentalDto>>> GetRentals(int id)
    {
        try
        {
            var client = await service.GetClient(id);
            if (client == null)
            {
                logger.LogWarning("Client with id {Id} not found when getting rentals", id);
                return NotFound();
            }

            var rentals = await rentalService.GetRentalsByClientId(id);
            return Ok(rentals);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting rentals for client with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Create a new client
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for client creation: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var id = await service.CreateClient(dto);
            var createdEntity = await service.GetClient(id);

            return CreatedAtAction(nameof(Get), new { id }, createdEntity);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for client creation");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating client");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Update client by ID
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Update(int id, [FromBody] ClientCreateDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for client update: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var updated = await service.UpdateClient(id, dto);
            if (updated == null)
            {
                logger.LogWarning("Client with id {Id} not found for update", id);
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Bad request for client update with id {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating client with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    /// <summary>
    /// Delete client by ID
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteClient(id);
            if (!deleted)
            {
                logger.LogWarning("Client with id {Id} not found for deletion", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting client with id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}