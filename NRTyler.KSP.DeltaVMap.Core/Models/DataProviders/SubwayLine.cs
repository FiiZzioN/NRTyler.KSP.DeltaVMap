// ***********************************************************************
// Assembly         : NRTyler.KSP.DeltaVMap.Core
//
// Author           : Nicholas Tyler
// Created          : 10-30-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-01-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.DeltaVMap.Core.Models.DataProviders
{
    /// <summary>
    /// Contains and organizes all steps of the journey that need to be completed in order to reach a desired <see cref="CelestialBody"/>.
    /// Also holds general information about the journey and the destination. 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "SubwayLine")]
    public class SubwayLine : INotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayLine"/> class.
        /// </summary>
        public SubwayLine()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubwayLine" /> class.
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody" /> that this SubwayLine's information is dedicated to.</param>
        /// <param name="lineIdentifier">The unique identifier that aids in differentiating and sorting this line from other SubwayLine.</param>
        public SubwayLine(CelestialBody target, string lineIdentifier)
        {
            Initialize(target, lineIdentifier);
        }

        #endregion

        #region Properties and Fields

        private string lineIdentifier;
        private CelestialBody target;
        private Landing landing;
        private Orbit orbit;
        private EllipticalOrbit ellipticalOrbit;
        private Intercept intercept;
        private LineInformation lineInformation;
        private SortedDictionary<int, SubwayStep> sortedSteps;

        /// <summary>
        /// Gets or sets a string of random numbers, letters or a combination of the two that are unique to this <see cref="SubwayLine"/>.
        /// </summary>
        [DataMember]
        public virtual string LineIdentifier
        {
            get { return this.lineIdentifier; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.lineIdentifier = value;
                OnPropertyChanged(nameof(LineIdentifier));
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="CelestialBody"/> that this <see cref="SubwayLine"/> is dedicated to.
        /// </summary>
        [DataMember]
        public virtual CelestialBody Target
        {
            get { return this.target; }
            set
            {
                this.target = value;
                OnPropertyChanged(nameof(Target));
            }
        }

        /// <summary>
        /// Gets or sets the energy needed in order to land on the target's surface safely.
        /// </summary>
        [DataMember]
        public virtual Landing Landing
        {
            get { return this.landing; }
            set
            {
                this.landing = value;
                OnPropertyChanged(nameof(Landing));
            }
        }

        /// <summary>
        /// Gets or sets the energy needed to enter an orbit and it's parameters. This also contains
        /// <see cref="SubwayLine"/>(s) for the target's moon(s), should it have any at all.
        /// </summary>
        [DataMember]
        public virtual Orbit Orbit
        {
            get { return this.orbit; }
            set
            {
                this.orbit = value;
                OnPropertyChanged(nameof(Orbit));
            }
        }

        /// <summary>
        /// Gets or sets the energy needed to enter an elliptical orbit and it's parameters. This also 
        /// contains <see cref="SubwayLine"/>(s) for the target's moon(s), should it have any at all.
        /// </summary>
        [DataMember]
        public virtual EllipticalOrbit EllipticalOrbit
        {
            get { return this.ellipticalOrbit; }
            set
            {
                this.ellipticalOrbit = value;
                OnPropertyChanged(nameof(EllipticalOrbit));
            }
        }

        /// <summary>
        /// Gets or sets the energy needed to get an intercept with your
        /// target, and how long it should take until you intercept it.
        /// </summary>
        [DataMember]
        public virtual Intercept Intercept
        {
            get { return this.intercept; }
            set
            {
                this.intercept = value;
                OnPropertyChanged(nameof(Intercept));
            }
        }

        /// <summary>
        /// Gets or sets any penitent information about this specific <see cref="SubwayLine"/>.
        /// </summary>
        [DataMember]
        public virtual LineInformation LineInformation
        {
            get { return this.lineInformation; }
            set
            {
                this.lineInformation = value;
                OnPropertyChanged(nameof(LineInformation));
            }
        }

        /// <summary>
        /// Gets or sets the SortedDictionary containing all of this SubwayLine's steps. The steps are sorted by their <see cref="StepID"/>.
        /// </summary>
        [DataMember]
        public virtual SortedDictionary<int, SubwayStep> SortedSteps
        {
            get { return this.sortedSteps; }
            set
            {
                this.sortedSteps = value;
                OnPropertyChanged(nameof(SortedSteps));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes every field or <see cref="object"/> in this class, and fulfills all constructor requirement(s).
        /// </summary>
        /// <param name="target">The <see cref="CelestialBody"/> this line is targeting.</param>
        /// <param name="lineIdentifier">The unique identifier for this SubwayLine.</param>
        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        private void Initialize(CelestialBody target, string lineIdentifier)
        {
            // Set this SubwayLine's Target and Identifier.
            Target          = target ?? (Target = new CelestialBody());
            LineIdentifier  = String.IsNullOrWhiteSpace(lineIdentifier) ? "Invalid Identifier" : lineIdentifier;

            // Create the various SubwaySteps and their target.
            Landing         = new Landing(Target);
            Orbit           = new Orbit(Target);
            EllipticalOrbit = new EllipticalOrbit(Target);
            Intercept       = new Intercept(Target);

            // Create the SortedDictionary and add the SubwaySteps 
            // that were just created to the SortedSteps Dictionary.
            InitializeSortedSteps();

            // Create the LineInformation.
            LineInformation = new LineInformation(this);
        }

        /// <summary>
        /// Initializes the SortedSteps Dictionary and then adds every SubwayStep to said Dictionary.
        /// </summary>
        private void InitializeSortedSteps()
        {
            SortedSteps = new SortedDictionary<int, SubwayStep>();

            AddStep(Landing);
            AddStep(Orbit);
            AddStep(EllipticalOrbit);
            AddStep(Intercept);

            // Takes a SubwayStep, gets its StepID, and then adds both the step
            // and its ID to the proper section of the SortedSteps Dictionary.
            void AddStep(SubwayStep subwayStep)
            {
                var key   = (int)subwayStep.StepID;
                var value = subwayStep;

                SortedSteps.Add(key, value);
            }
        }
        
        #endregion

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