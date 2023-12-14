using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

// [Route("[controller]")]       // this line of code is unnecessary, otherwise it will overwrite the [Route("api/[controller]")] from BaseApiController, making all the endpoint without "api/" as prefix.
public class BuggyController : BaseApiController
{
    private readonly DataContext _context;

    public BuggyController(DataContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("auth")] // api/buggy/auth
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }
    [HttpGet("not-found")]  // api/buggy/not-found
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = _context.Users.Find(-1);

        if (thing == null) return NotFound();

        return thing;
    }
    [HttpGet("server-error")]    // api/buggy/server-error
    public ActionResult<string> GetSeverError()
    {
        var thing = _context.Users.Find(-1);

        var thingToReturn = thing.ToString();  //This will for sure return null Reference exception error

        return thingToReturn;
    }
    [HttpGet("bad-request")]    // api/buggy/bad-request
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request!");

    }

}
