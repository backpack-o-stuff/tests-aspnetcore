using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TH.ClientLayer.Application.Monsters;
using TH.ClientLayer.Data.Monsters;
using TH.ClientLayer.Models;
using TH.Tests.Infrastructure.TestBases;

namespace TH.Tests.Application.MockStyleTests
{
    [TestClass]
    public class MonsterServiceTests
        : TestMockedFor<MonsterService>
    {
        [TestMethod]
        public void Find_When_AllIsWell()
        {
            Arrange(() =>
            {
                Mocker.GetMock<IMonsterRepository>()
                    .Setup(x => x.Find(It.IsAny<int>()))
                    .Returns(new Monster { Id = 1, Name = "Rawror", Power = 99 });
            });

            var result = Act(() => SystemUnderTest.Find(1));

            result.Id.Should().Be(1, "id was invliad.");
        }

        [TestMethod]
        public void Find_When_RecordDoesNotExist()
        {
            Arrange(() =>
            {
                Mocker.GetMock<IMonsterRepository>()
                    .Setup(x => x.Find(It.IsAny<int>()))
                    .Returns((Monster) null);
            });

            var result = Act(() => SystemUnderTest.Find(1));

            result.Should().BeNull("result was found");
        }

        [TestMethod]
        public void Find_When_DependencyNotMocked()
        {
            Arrange(() => {});

            var result = Act(() => SystemUnderTest.Find(1));

            result.Should().BeNull("mocked should have default returned empty/null/default.");
        }
    }
}