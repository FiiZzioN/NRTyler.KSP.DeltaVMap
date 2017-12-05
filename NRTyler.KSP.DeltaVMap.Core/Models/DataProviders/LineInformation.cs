// ************************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
// 
// Author           : Nicholas Tyler
// Created          : 10-30-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-30-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// Holds information that's pertinent to the 
    /// <see cref="DataProviders.SubwayLine"/> that it's attached to.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class LineInformation : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineInformation"/> class.
        /// </summary>
        /// <param name="subwayLine">
        /// The <see cref="DataProviders.SubwayLine"/> that 
        /// information will be pulled from or assigned to.
        /// </param>
        public LineInformation(SubwayLine subwayLine)
        {
            SubwayLine = subwayLine;
        }

        private int totalEnergyRequired;
        private Dictionary<string, string> commInfo;
        private string returnWindowCycle;

        /// <summary>
        /// Gets the <see cref="DataProviders.SubwayLine"/> that this
        /// <see cref="LineInformation"/> is associated with.
        /// </summary>
        public SubwayLine SubwayLine { get; }

        /// <summary>
        /// Gets or sets the total amount of energy required to launch from a given 
        /// home-world, and land on the <see cref="DataProviders.SubwayLine"/> target.
        /// </summary>
        public int TotalEnergyRequired
        {
            get { return this.totalEnergyRequired; }
            set
            {
                if (value < 0 || value == this.totalEnergyRequired) return;

                this.totalEnergyRequired = value;
                OnPropertyChanged(nameof(TotalEnergyRequired));
            }
        }

        /// <summary>
        /// Gets or sets the various tracking station level and class requirement 
        /// combinations needed in order to communicate with a spacecraft at the
        /// <see cref="DataProviders.SubwayLine"/> target.
        /// </summary>
        public Dictionary<string, string> CommInfo
        {
            get { return this.commInfo; }
            set
            {
                this.commInfo = value;
                OnPropertyChanged(nameof(CommInfo));
            }
        }

        /// <summary>
        /// Gets or sets the average time between launch windows to return home.
        /// </summary>
        public string ReturnWindowCycle
        {
            get { return this.returnWindowCycle; }
            set
            {
                // Using strings instead of a datetime or timespan until I can get a 
                // solid kerbaltimespan class so it can work properly with KSP years.
                if (String.IsNullOrWhiteSpace(value)) return;
                if (this.returnWindowCycle == value) return;

                this.returnWindowCycle = value;
                OnPropertyChanged(nameof(ReturnWindowCycle));
            }
        }


        public void UpdateInformation()
        {
#if !DEBUG
            throw new NotImplementedException(); 
#endif
        }



        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}