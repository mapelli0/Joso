using System;
using System.Threading.Tasks;
using Windows.Devices.Gpio;


namespace Joso.Gpio {
    internal class GpioService: IGpioService, IDisposable {
        private const int RELAY_1 = 6;
        private const int RELAY_2 = 13;
        private const int RELAY_3 = 19;
        private const int RELAY_4 = 26;

        private readonly GpioController _gpioController;
        private readonly GpioPin _pinRelay1;
        private readonly GpioPin _pinRelay2;
        private readonly GpioPin _pinRelay3;
        private readonly GpioPin _pinRelay4;

        public GpioService() {
            this._gpioController = GpioController.GetDefault();
            if (this._gpioController == null) {
                throw new NotSupportedException("Could not initialize GPIO Controller");
            }

            this._pinRelay1 = this.InitializePin(RELAY_1);
            this._pinRelay2 = this.InitializePin(RELAY_2);
            this._pinRelay3 = this.InitializePin(RELAY_3);
            this._pinRelay4 = this.InitializePin(RELAY_4);
            
        }

        private GpioPin InitializePin(int gpioPinNumer) {
            var pin = this._gpioController.OpenPin(gpioPinNumer);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin.Write(GpioPinValue.High);            
            return pin;
        }

        private async Task Flip(GpioPin pin, int milliseconds) {
            pin.Write(GpioPinValue.Low);
            await Task.Delay(milliseconds);
            pin.Write(GpioPinValue.High);
        }

        #region Implementation of IGPIOService
        public async Task TestGpio() {
            int t = 5000;

            await this.Flip(this._pinRelay1, t);
            await this.Flip(this._pinRelay2, t);
            await this.Flip(this._pinRelay3, t);
            await this.Flip(this._pinRelay4, t);            
        }
        #endregion

        #region Implementation of IDisposable
        public void Dispose() {
            this._pinRelay1.Dispose();
            this._pinRelay2.Dispose();
            this._pinRelay3.Dispose();
            this._pinRelay4.Dispose();
        }
        #endregion

    }
}