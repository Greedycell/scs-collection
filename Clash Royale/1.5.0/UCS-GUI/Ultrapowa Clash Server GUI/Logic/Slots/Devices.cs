namespace CRS.Logic.Slots
{
    using System;
    using System.Collections.Generic;

    using Core.Settings;

    internal class Devices : Dictionary<IntPtr, Device>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Devices"/> class.
        /// </summary>
        public Devices() : base(Constants.MaxDevices)
        {
            // Devices.
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Devices"/> class.
        /// </summary>
        /// <param name="_Max">The maximum of devices in the list.</param>
        public Devices(int _Max) : base(_Max)
        {
            // Devices.
        }

        /// <summary>
        /// Add the specified device to the list.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public new void Add(Device _Device)
        {
            if (this.ContainsKey(_Device.Connection.Handle))
            {
                this[_Device.Connection.Handle] = _Device;
            }
            else
            {
                this.Add(_Device.Connection.Handle, _Device);
            }
        }

        /// <summary>
        /// Remove the specified device from the list.
        /// </summary>
        /// <param name="_Device">The device.</param>
        public new void Remove(Device _Device)
        {
            if (this.ContainsKey(_Device.Connection.Handle))
            {
                _Device.Connection.Close();
                this.Remove(_Device.Connection.Handle);
            }
        }

        /// <summary>
        /// Remove the specified device from the list.
        /// </summary>
        /// <param name="_Handle">The device handle.</param>
        public new void Remove(IntPtr _Handle)
        {
            if (this.ContainsKey(_Handle))
            {
                this[_Handle].Connection.Close();
                base.Remove(_Handle);
            }
        }
    }
}
