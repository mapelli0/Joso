using System.Threading.Tasks;

namespace Joso.Gpio {
    internal interface IGpioService {
        Task TestGpio();
    }
}