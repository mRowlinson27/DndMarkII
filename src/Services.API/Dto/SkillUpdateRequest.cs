
namespace Services.API.Dto
{
    using System;

    public class SkillUpdateRequest
    {
        public Guid Id { get; set; }

        public int Ranks { get; set; }

        public bool Class { get; set; }
    }
}
