﻿using Application.Movies;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _mediator.Send(new List.Query());
            if (result == null) return NotFound();     
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new Get.Query { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Movie movie)
        {
            var result = await _mediator.Send(new Create.Command { Movie = movie });
            if (result == null) return BadRequest("Failed to create a movie");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id, [FromBody] Movie movie)
        {
            var result = await _mediator.Send(new Edit.Command { Movie = movie });
            if (result == null) return BadRequest("Failed to edit a movie");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new Delete.Command { Id = id });
            if(!result) return NotFound();
            return NoContent();
        }
    }
}
