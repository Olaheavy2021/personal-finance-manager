using PersonalFinanceManager.Client;
using Reqnroll.BoDi;

namespace PersonalFinanceManager.API.Test.Drivers;

[Binding]
public static class DriverRegistration
{
    [BeforeTestRun]
    public static void BeforeTestRun(IObjectContainer diContainer)
    {
        diContainer.RegisterFactoryAs(_ => new ApiDriver()).InstancePerContext();
        diContainer.RegisterFactoryAs(_ => _.Resolve<ApiDriver>().HttpClient).InstancePerContext();
        //diContainer.RegisterTypeAs<PFMv1Client, IPFMv1Client>()
         // .InstancePerContext();
        diContainer.RegisterFactoryAs(_ => _.Resolve<ApiDriver>().PFMv1Client).InstancePerContext();
    }

    [AfterTestRun]
    public static void AfterTestRun(IObjectContainer diContainer)
    {
    }
}
