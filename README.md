# tests-dotnetcore

Test practices and helpers in ASP.NET Core.

* NOTE: this is not an example of how to setup the other layers and systems of the application. See other repositories for better examples.

---

## Setup

- Command line > ClientLayer folder > Run: dotnet ef database update

#### Built With

- ASP.NET Core 2.2

---

## INTENT

#### AAA - Arrange > Act > Assert

Some semi-meaningful semi-meaningless inline delegate callers so that we can group the AAA structure of the tests without comments or regions... :D

#### Unit/Mockist > MockedFor<T> test base

For finely granular tests in isolation from the rest of the system, MockedFor<T> tests create a SUT (SystemUnderTest) and automatically mock out its dependencies. It exposes the auto mocker and allows you to mock as you usually would with all the regular Moq practices.
  
The trade-off with these tests is that the SUT is tested in a vacuum. While you can confirm that it does the work you expect it to, it does not give faith that it works in the larger system. Additionally, when test mocking practices are used heavily, they begin to bind the implementation of your code to the tests themselves. Doing this means that when you change your code (sometimes in small ways) that the tests will not be flexible enough to go unchanged. For these reasons I tend to shy away from mockist style testing more than I used to.

#### Component/Classical > IntegratedFor<T> test base
  
This test helper uses the dependency resolver to create a more realistic instance of the SUT (SystemUnderTest), with the caveat that you can override the resolvers registrations with fakes that you create. This will allow you to fake out external reaching dependencies so that those fakes will be pass in and treated as the actual dependency. All non-faked dependencies will be the real dependencies of the SUT.

The trade-off with these tests is that the tests are more flexible to system code changes but depending on how deeply the code flows through the SUTs dependencies could mean a more extensive setup in the tests themselves (arrange of a test). Additionally, changes to dependencies of component tested objects can find their tests breaking. I see this as both a boon and a curse, as it tells me when changes affect the system in large but also create more failing tests than one needs actually to deal with. Small changesets in between test runs help narrow down required troubleshooting, but sometimes it can't be helped. However, I have found that the flexibility to change a system more freely (causing fewer test changes) has shifted my opinion towards the want to use classical over mocked tests.

## MockedFor<T> Example

```
[TestClass]
public class MonsterServiceTests
    : MockedFor<MonsterService>
{
    [TestMethod]
    public void Find_When_AllIsWell()
    {
        Arrange(() =>
        {
            Mocker.GetMock<IMonsterRepository>()
                .Setup(x => x.Find(It.IsAny<int>()))
                .Returns(new Monster { Id = 1, Name = "Rawronidas", Power = 99 });
        });

        var result = Act(() => SUT.Find(1));

        Assert(() => 
        {
            result.Id.Should().Be(1, "id was invliad.");
        });
    }
}
```

## IntegratedFor<T> Example
```
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
}
```
