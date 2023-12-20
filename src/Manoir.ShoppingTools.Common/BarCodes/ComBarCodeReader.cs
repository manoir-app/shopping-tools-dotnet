using Manoir.ShoppingTools.Common.Devices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Common.BarCodes
{
    public class ComPortBarCodeScanner : IBarCodeScanner
    {
        public class ConfigScannerCOM
        {
            public string Port { get; set; }
        }
        private ConfigScannerCOM _configData;
        private SerialPort _sp;



        private DeviceStatus _status = DeviceStatus.NotAvailable;
        public DeviceStatus Status
        {
            get
            {
                return _status;
            }
            protected set
            {
                if (_status != value)
                {
                    _status = value;
                    OnStatusChanged();
                }
            }
        }
        public event EventHandler StatusChanged;

        protected void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }


        public event EventHandler<CodeBarScannedEventArgs> BarCodeScanned;


        protected void OnBarCodeScanned(string barCode)
        {
            OnBarCodeScanned(barCode, BarCodeHelper.IdentifyCodeType(barCode));
        }

        protected void OnBarCodeScanned(string barCode, string barCodeType)
        {
            BarCodeScanned?.Invoke(this, new CodeBarScannedEventArgs()
            {
                BarCode = barCode,
                BarCodeType = barCodeType
            });
        }


        public event EventHandler<CodeBarErrorEventArgs> ScanError;

        protected void OnScanError(string errorCode)
        {
            OnScanError(errorCode, null, null);
        }


        protected void OnScanError(string errorCode, string errorDetails, Exception inner)
        {
            ScanError?.Invoke(this, new CodeBarErrorEventArgs()
            {
                ErrorCode = errorCode,
                ErrorDetails = errorDetails,
                InnerException = inner
            });
        }



        public event EventHandler NewValidConfigurationFound;
        public event EventHandler InvalidConfiguration;
        public event EventHandler ConnectionRestored;
        public event EventHandler ErrorRaised;

        public void ClearCache()
        {

        }


        public Guid Guid { get; private set; } = Guid.NewGuid();

        public void Configure(Guid deviceGuid, string name, string configData)
        {
            this.Guid = deviceGuid;

            try
            {
                _configData = JsonConvert.DeserializeObject<ConfigScannerCOM>(configData);

                if (_configData == null || _configData.Port == null)
                {
                    Status = DeviceStatus.NotAvailable;
                    return;
                }

                _sp = new SerialPort(_configData.Port);
                _sp.BaudRate = 9600;
                _sp.Parity = Parity.None;
                _sp.StopBits = StopBits.One;
                _sp.DataBits = 8;
                _sp.Handshake = Handshake.None;
                _sp.RtsEnable = true;
                _sp.DataReceived += new SerialDataReceivedEventHandler(OnCodeBarreScanne);
                _sp.ErrorReceived += new SerialErrorReceivedEventHandler(OnErrorReceived);
                _sp.PinChanged += new SerialPinChangedEventHandler(OnPinChanged);

                try
                {
                    _sp.Open();
                    Status = DeviceStatus.Active;
                }
                catch (Exception)
                {
                    string msg = $"Scanner de code barre déconnecté (Port: {_sp.PortName})";
                    Status = DeviceStatus.NotAvailable;
                    InvalidConfiguration?.Invoke(msg, null);
                }

                _thread = new Thread(new ThreadStart(CheckCurrentConnection));
                _thread.Start();
            }
            catch (PlatformNotSupportedException)
            {
                // on est sur une plateforme qui ne supporte pas
                // l'accès au port COM
                Status = DeviceStatus.NotAvailable;
            }
        }


        #region gestion de la lecture depuis le port COM

        public void OnCodeBarreScanne(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string codeBarre = sp.ReadExisting().Trim();

            // TODO: Vérifier qu'il s'agit bien d'un code barre (et pas un QR code par exemple)
            OnBarCodeScanned(codeBarre);
        }

        public void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnScanError(e.EventType.ToString());
        }

        public void OnPinChanged(object sender, SerialPinChangedEventArgs e)
        {

        }

        #endregion





        private Thread _thread;

        private bool _stop = false;

        public void CheckCurrentConnection()
        {
            while (!_stop)
            {
                Thread.Sleep(1000);

                if (_sp.IsOpen)
                    _sp.Close();

                try
                {
                    _sp.Open();
                    if (Status == DeviceStatus.Disconnected)
                    {
                        Status = DeviceStatus.Active;
                        string msg = $"Scanner de code barre reconnecté (Port: {_sp.PortName})";
                        Console.WriteLine(msg);
                        ConnectionRestored?.Invoke(this, EventArgs.Empty);
                    }
                }
                catch (Exception)
                {
                    if (Status == DeviceStatus.Active || Status == DeviceStatus.Available)
                    {
                        Status = DeviceStatus.Disconnected;
                        string msg = $"Scanner de code barre déconnecté (Port: {_sp.PortName})";
                        Console.WriteLine(msg);
                        InvalidConfiguration?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public DeviceConfigElement[] GetConfigElements()
        {
            return new DeviceConfigElement[]{
            new DeviceConfigElement() { Name = "Port", Type = typeof(string), DefaultValue="COM1" }
        };
        }

        public DeviceDescription GetDescription()
        {
            throw new NotImplementedException();
        }


        public void Restart()
        {
            throw new NotImplementedException();
        }



        public void Pause()
        {
            // appli : "Scanner, mets toi en veille"
            // *scanner se déconnecte du port COM*
            _sp.Close();
        }

        public void Resume()
        {
            _sp.Open();
        }

        public void Dispose()
        {
            _stop = true;
            Thread.Sleep(1000);
            try
            {
                if (_thread.IsAlive)
                    _thread.Interrupt();
            }
            catch (Exception)
            {

            }
            GC.SuppressFinalize(this);
            if (_sp != null) _sp.Dispose();
            Status = DeviceStatus.NotAvailable;
        }
    }
}
