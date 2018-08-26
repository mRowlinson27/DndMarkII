
namespace Database.API.Dto
{
    using System.Collections.Generic;

    public class Model
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public IEnumerable<PrimaryStat> PrimaryStats { get; set; }

        public IEnumerable<Skill> Skills { get; set; }
    }
}
