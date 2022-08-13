using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Neo4jCrud.Entities
{
    [Table("Profile")]
    public class Profile 
    {
        public Profile()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        private DateTimeOffset _dt;

        /// <summary>
        ///     Represents the id of the <see cref="Profile" />.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Represents the firstName of the <see cref="Profile" />.
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string? FirstName { get; set; }


        /// <summary>
        ///     Represents the lastName of the <see cref="Profile" />.
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string? LastName { get; set; }

        /// <summary>
        ///     Represents the full name of the <see cref="Profile" />..
        /// </summary>
        [JsonProperty(PropertyName = "fullName")]
        public string FullName => $"{FirstName} {LastName}";


        /// <summary>
        ///     Represents the creation date of the <see cref="Profile" />.
        /// </summary>
        [JsonProperty(PropertyName = "createdAt")]
        public DateTimeOffset CreatedAt
        {
            get => _dt;
            set => _dt = value.ToUniversalTime();
        }
    }
}
