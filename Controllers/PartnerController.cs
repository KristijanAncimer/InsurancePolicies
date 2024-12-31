using InsurancePolicies.Application.Handlers.Partners.Commands.Create;
using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;
using InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;
using InsurancePolicies.Application.Handlers.Partners.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicies.Api.Controllers;

public class PartnerController : Controller
{
    private readonly IMediator _mediator;

    public PartnerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageSize = 10, int pageNumber = 1)
    {
        var partners = await _mediator.Send(GetAllPartnersRequest.Create(pageSize, pageNumber));
        return View(partners);
    }
    [HttpGet("Partner/GetAllPartners")]
    public async Task<IActionResult> GetAllPartners(int pageSize = 10, int pageNumber = 1)
    {
        var partners = await _mediator.Send(GetAllPartnersRequest.Create(pageSize, pageNumber));
        return Json(partners);
    }
    [HttpGet("Partner/GetPartnerDetails")]
    public async Task<IActionResult> GetPartnerDetails(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { error = "Invalid ID." });
        }

        try
        {
            var partnerDetails = await _mediator.Send(GetPartnerByIdRequest.Create(id));
            if (partnerDetails == null)
            {
                return NotFound(new { error = "Partner not found." });
            }
            return Json(partnerDetails);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
    [HttpPost]
    [Route("Partner/AddPartner")]
    public async Task<IActionResult> AddPartner(string firstName, string lastName, string address, string partnerNumber, string? croatianPIN,
        PartnerTypeId partnerTypeId, string createdByUser, bool isForeign, string externalCode, Gender gender)
    {
        Console.WriteLine("AddPartner endpoint hit");
        try
        {
            var result = await _mediator.Send(CreatePartnerCommand.Create(firstName, lastName, address, partnerNumber, croatianPIN, partnerTypeId,
                createdByUser, isForeign, externalCode, gender));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
