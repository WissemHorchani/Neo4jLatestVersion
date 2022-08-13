using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using Neo4jClient.DataAnnotations;
using Neo4jCrud.Entities;
using Neo4jCrud.Models.Request;

namespace Neo4jCrud.Controllers
{
    
    [ApiController]
    [Produces("application/json")]
    public class ProfilesController : ControllerBase
    {

        private const string ApiRoutePrefix = "/api/v1/profiles";

        private readonly IGraphClient _client;

        public ProfilesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpPost(ApiRoutePrefix)]
        public async Task<IActionResult> CreateAsync([FromBody] ProfileCreationRequest model)
        {
            var result = await _client.WithAnnotations().Cypher
                .Create(p => p.Pattern<Profile>("profile"))
                .Set("profile.id = $id")
                .WithParam("id", Guid.NewGuid().ToString())
                .Set("profile.firstName = $firsName")
                .WithParam("firsName", model.FirstName)
                .Set("profile.lastName = $lastName")
                .WithParam("lastName", model.LastName)
                .Set("profile.fullName = $fullName")
                .WithParam("fullName", model.FirstName + " " + model.LastName)
                .Set("profile.createdAt = $createdAt")
                .WithParam("createdAt", DateTimeOffset.UtcNow)
                .Return(profile => profile.As<Profile>())
                .ResultsAsync;
            return Ok(result);
        }

        [HttpGet(ApiRoutePrefix)]
        public async Task<IActionResult> RetrieveAsync()
        {
            
            var result = await _client.WithAnnotations().Cypher
                .Match(p => p.Pattern<Profile>("profile"))
                    .Return(profile => profile.As<Profile>()).ResultsAsync;
            return Ok(result.ToList());
        }

        [HttpGet(ApiRoutePrefix + "/{profileId:guid}")]
        public async Task<IActionResult> RetrieveByIdAsync(Guid profileId)
        {

            var result = await _client.WithAnnotations().Cypher
                .Match(p => p.Pattern<Profile>("profile").Constrain(profile => profile.Id == profileId.ToString()))
                .Return(profile => profile.As<Profile>()).ResultsAsync;
            return Ok(result.ToList());
        }


        [HttpDelete(ApiRoutePrefix + "/{profileId:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid profileId)
        {

            var result = _client.WithAnnotations().Cypher
                .Match(p => p.Pattern<Profile>("profile").Constrain(profile => profile.Id == profileId.ToString()));

            await result.DetachDelete("profile").ExecuteWithoutResultsAsync();

            return Ok("Profile successfully deleted.");
        }
    }
}
