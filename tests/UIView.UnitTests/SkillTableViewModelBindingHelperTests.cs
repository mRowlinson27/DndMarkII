
namespace UIView.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using API;
    using FakeItEasy;
    using FluentAssertions;
    using Helpers;
    using NUnit.Framework;
    using UIModel.API.Dto;
    using UIUtilities.API;

    [TestFixture]
    public class SkillTableViewModelBindingHelperTests
    {
        private SkillTableViewModelBindingHelper _skillTableViewModelBindingHelper;

        private ISkillViewModelFactory _skillViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _skillViewModelFactory = A.Fake<ISkillViewModelFactory>();
            _skillTableViewModelBindingHelper = new SkillTableViewModelBindingHelper(_skillViewModelFactory);
        }

        [Test]
        public void Rebind_EmptyToBeginWith_CreatesNewViewModels()
        {
            //Arrange
            var currentData = new ObservableCollection<ISkillViewModel>();
            var newUiSkill = new UiSkill
            {
                Id = Guid.NewGuid()
            };
            var newData = new List<UiSkill>
            {
                newUiSkill
            };

            var fakeSkillViewModel = A.Fake<ISkillViewModel>();
            A.CallTo(() => _skillViewModelFactory.Create(newUiSkill)).Returns(fakeSkillViewModel);

            //Act
            _skillTableViewModelBindingHelper.Rebind(currentData, newData);

            //Assert
            currentData.FirstOrDefault().Should().Be(fakeSkillViewModel);
        }

        [Test]
        public void Rebind_HasValueToBeginWith_Overwrites()
        {
            //Arrange
            var newUiSkill = new UiSkill
            {
                Id = Guid.NewGuid()
            };
            var newData = new List<UiSkill>
            {
                newUiSkill
            };

            var fakeSkillViewModel = A.Fake<ISkillViewModel>();
            var currentData = new ObservableCollection<ISkillViewModel> {fakeSkillViewModel};

            //Act
            _skillTableViewModelBindingHelper.Rebind(currentData, newData);

            //Assert
            A.CallToSet(() => fakeSkillViewModel.Skill).To(newUiSkill).MustHaveHappened();
        }

        [Test]
        public void Rebind_LosesData_RemovesExcess()
        {
            //Arrange
            var newData = new List<UiSkill>
            {
                new UiSkill()
            };

            var fakeSkillViewModel1 = A.Fake<ISkillViewModel>();
            var fakeSkillViewModel2 = A.Fake<ISkillViewModel>();
            var currentData = new ObservableCollection<ISkillViewModel> { fakeSkillViewModel1, fakeSkillViewModel2 };

            //Act
            _skillTableViewModelBindingHelper.Rebind(currentData, newData);

            //Assert
            currentData.Should().HaveCount(1);
        }

        [Test]
        public void Model_OnSkillRequestTaskCompletion_SetsSkillViewModelBackgroundColour()
        {
            //Arrange
            var newData = new List<UiSkill> { new UiSkill(), new UiSkill() };

            var fakeViewModel0 = A.Fake<ISkillViewModel>();
            var fakeViewModel1 = A.Fake<ISkillViewModel>();
            A.CallTo(() => _skillViewModelFactory.Create(A<UiSkill>.Ignored)).ReturnsNextFromSequence(fakeViewModel0, fakeViewModel1);

            //Act
            _skillTableViewModelBindingHelper.Rebind(new ObservableCollection<ISkillViewModel>(), newData);

            //Assert
            A.CallToSet(() => fakeViewModel0.BackGroundColour).To(Constants.SkillModelEvenIndexBackGroundColour).MustHaveHappened();
            A.CallToSet(() => fakeViewModel1.BackGroundColour).To(Constants.SkillModelOddIndexBackGroundColour).MustHaveHappened();
        }
    }
}
