using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Farum.QA.TestAutomationEssentials.Support.Hooks
{
    /// <summary>
    /// Class which contains methods, which will be executed before scenarios.
    /// </summary>
    [Binding]
    public sealed class BeforeScenarios
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Drivers.ConfigurationDriver _configurationDriver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public BeforeScenarios(ScenarioContext scenarioContext,
                               Drivers.ConfigurationDriver configurationDriver,
                               ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _configurationDriver = configurationDriver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        /// <summary>
        /// Example method which will be executed before each scenario.
        /// </summary>
        /*[BeforeScenario]
        public void ExampleMethod()
        {
            // declare something here to be executed before each scenario
        }*/

    }
}
