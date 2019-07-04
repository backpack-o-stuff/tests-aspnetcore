using System;

namespace TH.Tests.Infrastructure.TestBases
{
    public class TestArrangeActOn
    {
        protected void Arrange(Action arrange)
        {
            arrange();
        }

        protected void Act(Action act)
        {
            act();
        }

        protected TResult Act<TResult>(Func<TResult> act)
        {
            return act();
        }
    }
}