using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manoir.ShoppingTools.Common.Devices
{
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum DeviceGenericStatus
    {
        /// <summary>
        /// Le périphérique est dans un état inconnu
        /// </summary>
        Unknown,
        /// <summary>
        /// Le périphérique est déconnecté
        /// </summary>
        Disconnected,
        /// <summary>
        /// Le périphérique n'est pas disponible
        /// </summary>
        NotAvailable,
        /// <summary>
        /// Le périphérique est disponible mais non activé
        /// </summary>
        Available,
        /// <summary>
        /// Le périphérique est disponible et activé
        /// </summary>
        Active
    }

    public sealed class DeviceDescription
    {
        public string Model { get; set; }
    }

    public sealed class DeviceStatus
    {
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DeviceGenericStatus Status { get; set; }
        public string DetailedStatus { get; set; }

        public DeviceStatus(DeviceGenericStatus baseStatus)
        {
            Status = baseStatus;
        }

        public static readonly DeviceStatus Disconnected = new DeviceStatus(DeviceGenericStatus.Disconnected);
        public static readonly DeviceStatus NotAvailable = new DeviceStatus(DeviceGenericStatus.NotAvailable);
        public static readonly DeviceStatus Unknown = new DeviceStatus(DeviceGenericStatus.Unknown);
        public static readonly DeviceStatus Available = new DeviceStatus(DeviceGenericStatus.Available);
        public static readonly DeviceStatus Active = new DeviceStatus(DeviceGenericStatus.Active);
    }


    public class DeviceConfigElement
    {
        public string Name { get; set; }
        public Type Type { get; set; }

        public bool IsSecret { get; set; }
        public bool IsServerSet { get; set; }

        public object DefaultValue { get; set; }
    }


    public interface IDeviceBase : IDisposable
    {

        Guid Guid { get; }


        /// <summary>
        /// Obtient le descriptif du device
        /// </summary>
        /// <value>
        /// Le descriptif du device
        /// </value>
        DeviceDescription GetDescription();

        DeviceStatus Status { get; }

        event EventHandler StatusChanged;



        void Configure(Guid deviceGuid, string deviceName, string configData);

        DeviceConfigElement[] GetConfigElements();

        void Restart();


        /// <summary>
        /// Event levé lorsque le device a trouvé de lui même une nouvelle config valide
        /// </summary>
        event EventHandler NewValidConfigurationFound;

        /// <summary>
        /// Event levé lorsque la configuration connue n'est plus valide
        /// </summary>
        event EventHandler InvalidConfiguration;

        /// <summary>
        /// Event levé lorsque la communication avec un device est rétablie (exemple un device déconnecté est de nouveau connecté)
        /// </summary>
        event EventHandler ConnectionRestored;

        event EventHandler ErrorRaised;

    }
}
