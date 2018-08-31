
namespace Database.API.Dto
{
    using System;
    using System.Collections.Generic;

    public class Model
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public List<PrimaryStat> PrimaryStats { get; set; }

        public Dictionary<Guid, Skill> Skills { get; set; }
    }
}
