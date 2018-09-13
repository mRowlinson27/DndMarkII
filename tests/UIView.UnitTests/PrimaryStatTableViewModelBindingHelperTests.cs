
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

    [TestFixture]
    public class PrimaryStatTableViewModelBindingHelperTests
    {
        private PrimaryStatTableViewModelBindingHelper _bindingHelper;

        private IPrimaryStatViewModelFactory _viewModelFactory;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = A.Fake<IPrimaryStatViewModelFactory>();
            _bindingHelper = new PrimaryStatTableViewModelBindingHelper(_viewModelFactory);
        }

        [Test]
        public void Rebind_EmptyToBeginWith_CreatesNewViewModels()
        {
            //Arrange
            var currentData = new ObservableCollection<IPrimaryStatViewModel>();
            var newItem = new UiPrimaryStat();
            var newData = new List<UiPrimaryStat> {newItem};

            var fakeViewModel = A.Fake<IPrimaryStatViewModel>();
            A.CallTo(() => _viewModelFactory.Create(newItem)).Returns(fakeViewModel);

            //Act
            _bindingHelper.Rebind(currentData, newData);

            //Assert
            currentData.FirstOrDefault().Should().Be(fakeViewModel);
        }

        [Test]
        public void Rebind_HasValueToBeginWith_Overwrites()
        {
            //Arrange
            var newItem = new UiPrimaryStat();
            var newData = new List<UiPrimaryStat> { newItem };

            var fakeViewModel = A.Fake<IPrimaryStatViewModel>();
            var currentData = new ObservableCollection<IPrimaryStatViewModel> { fakeViewModel };

            //Act
            _bindingHelper.Rebind(currentData, newData);

            //Assert
            A.CallToSet(() => fakeViewModel.PrimaryStat).To(newItem).MustHaveHappened();
        }

        [Test]
        public void Rebind_LosesData_RemovesExcess()
        {
            //Arrange
            var newData = new List<UiPrimaryStat>
            {
                new UiPrimaryStat()
            };

            var fakeViewModel1 = A.Fake<IPrimaryStatViewModel>();
            var fakeViewModel2 = A.Fake<IPrimaryStatViewModel>();
            var currentData = new ObservableCollection<IPrimaryStatViewModel> { fakeViewModel1, fakeViewModel2 };

            //Act
            _bindingHelper.Rebind(currentData, newData);

            //Assert
            currentData.Should().HaveCount(1);
        }
    }
}
