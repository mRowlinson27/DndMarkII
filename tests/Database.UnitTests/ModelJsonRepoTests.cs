
namespace Database.UnitTests
{
    using API.Dto;
    using FakeItEasy;
    using NUnit.Framework;
    using Utilities.API.DAL;

    [TestFixture]
    public class ModelJsonRepoTests
    {
        private ModelJsonRepo _modelJsonRepo;

        private IJsonFile<Model> _jsonFile;

        [SetUp]
        public void Setup()
        {
            _jsonFile = A.Fake<IJsonFile<Model>>();

            _modelJsonRepo = new ModelJsonRepo(_jsonFile);
        }
    }
}
