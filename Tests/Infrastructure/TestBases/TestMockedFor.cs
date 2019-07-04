using Moq;
using Moq.AutoMock;

namespace TH.Tests.Infrastructure.TestBases
{
    public class TestMockedFor<T> : TestArrangeActOn
        where T : class
    {
        protected AutoMocker Mocker;
        protected T SystemUnderTest;

        public TestMockedFor()
        {
            Mocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Empty);

            BeforeEachSharedSetup();
            BeforeEach();

            SystemUnderTest = Mocker.CreateInstance<T>();
        }

        protected virtual void BeforeEach() {}

        private void BeforeEachSharedSetup()
        {
            // NOTE: for any setup that is shared among all tests
        }
    }
}