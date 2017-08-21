using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls;
using Joso.Gpio;

namespace Joso
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            var service = new GpioService();
            await service.TestGpio();
            deferral.Complete();
        }
    }

}
