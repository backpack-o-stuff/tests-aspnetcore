using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TH.ClientLayer.Application.Monsters;
using TH.ClientLayer.Data.Monsters;
using TH.ClientLayer.Models;
using TH.Tests.Infrastructure.TestBases;

namespace TH.Tests.Application.ComponentStyleTests
{
    [TestClass]
    public class MonsterServiceTests
        : IntegratedFor<MonsterService>
    {
        [TestCleanup]
        public void AfterEach()
        {
            var repository = Resolve<IMonsterRepository>();
            repository.RemoveRange(repository.All());
        }

        [TestMethod]
        public void Find_When_AllIsWell()
        {
            Arrange(() =>
            {
                var repository = Resolve<IMonsterRepository>();
                repository.Add(new Monster { Name = "Rawrgnar", Power = 99 });
            });

            var result = Act(() => SUT.Find(1));

            Assert(() => 
            {
                result.Id.Should().Be(1, "id was invliad.");
            });
        }

        [TestMethod]
        public void Find_When_RecordDoesNotExist()
        {
            Arrange(() => {});

            var result = Act(() => SUT.Find(1));

            Assert(() => 
            {
                result.Should().BeNull("result was found");
            });
        }
    }
}