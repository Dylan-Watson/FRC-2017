/****************************** Header ******************************\
Class Name: ActiveCollection
Summary: Stores all components on the robot controled by the software.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using WPILib.Exceptions;

namespace Base
{
    /// <summary>
    /// Class that stores the currently active collection of components on the robot.
    /// </summary>
    public sealed class ActiveCollection
    {
        #region Private Fields

        private static readonly Lazy<ActiveCollection> _lazy =
            new Lazy<ActiveCollection>(() => new ActiveCollection());

        #endregion Private Fields

        #region Private Constructors

        private ActiveCollection()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Instance of the singleton
        /// </summary>
        public static ActiveCollection Instance => _lazy.Value;

        #endregion Public Properties



        #region Private Fields

        private readonly Dictionary<string, IComponent> componentCollection = new Dictionary<string, IComponent>();

        private readonly InputComponentUpdateLoop inputComponentUpdateLoop = new InputComponentUpdateLoop();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Gets the motors that were flagged to be on the left side fo the drive train.
        /// </summary>
        public List<IComponent> GetLeftDriveMotors
            => (from t in componentCollection.Values where t is Motor select t as Motor).ToList().Where
                (t => t.DriveSide == Motor.Side.Left).ToList().Cast<IComponent>().ToList();

        /// <summary>
        /// Gets the motors that were flagged to be on the right side fo the drive train.
        /// </summary>
        public List<IComponent> GetRightDriveMotors
            => (from t in componentCollection.Values where t is Motor select t as Motor).ToList().Where
                (t => t.DriveSide == Motor.Side.Right).ToList().Cast<IComponent>().ToList();

        /// <summary>
        /// Adds a component to the active collection, and reports and errors in doing so.
        /// </summary>
        /// <param name="component">The IComponent to add</param>
        public void AddComponent(IComponent component)
        {
            try
            {
                if (componentCollection.ContainsKey(component.Name))
                    throw new AllocationException(
                        $"Attempting to allocate two components with the same name - {component.Name}");
                componentCollection.Add(component.Name, component);

                var input = component as InputComponent;
                if (input != null)
                    inputComponentUpdateLoop.AddInputComponent(input);
            }
            catch (AllocationException ex)
            {
                Report.Error(ex.Message);
                Report.Warning(
                    $"As a result the latter defined {component.Name} was not added to the active collection, " +
                    "meaning any controls intended to use this component will not work, or will control the " +
                    $"component that {component.Name} was a duplicate of. CHECK CONFIG.");
            }
            catch (Exception ex)
            {
                //TODO: report errors or throw new one, I haven't decided yet.
                Log.Write(ex);
            }
        }

        /// <summary>
        /// Gets a IComponent from the active colletion by it's name. Reports any errors and returns
        /// null should there be any.
        /// </summary>
        /// <param name="commonName">CommonName of the component</param>
        /// <returns></returns>
        public IComponent Get(CommonName commonName)
        {
            try
            {
                return componentCollection[commonName.ToString()];
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }

            Report.Error($"Cannot find component {commonName}, it does not exist in the active collection!");
            return null;
        }

        /// <summary>
        /// Gets and casts an IComponents item to a Motor by it's CommonName. Reports any errors and
        /// returns null should there be any.
        /// </summary>
        /// <param name="commonName">CommonName of the component/motor</param>
        /// <returns></returns>
        public Motor GetMotorItem(CommonName commonName)
        {
            try
            {
                return (Motor) componentCollection[commonName.ToString()];
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }

            Report.Error($"Cannot find component {commonName}, it does not exist in the active collection!");

            return null;
        }

        /// <summary>
        /// Disposes of all IComponents within the Active Collection, and then clears the collection
        /// </summary>
        public void ReleaseActiveCollection()
        {
            inputComponentUpdateLoop.ClearInputComponents();
            foreach (var component in componentCollection)
                component.Value.Dispose();
            componentCollection.Clear();
        }

        #endregion Public Methods
    }

    public class InputComponentUpdateLoop : ControlLoop
    {
        #region Private Fields

        private readonly List<InputComponent> inputComponents = new List<InputComponent>();

        #endregion Private Fields

        #region Public Constructors

        public InputComponentUpdateLoop()
        {
            OverrideCycleTime(.05);
            StartWhenReady();
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void main()
        {
            if (inputComponents.Count == 0) return;
            lock (inputComponents)
            {
                foreach (var input in inputComponents)
                    input.Get();
            }
        }

        #endregion Protected Methods

        #region Public Methods

        public void AddInputComponent(InputComponent input)
        {
            lock (inputComponents)
            {
                inputComponents.Add(input);
            }
        }

        public void ClearInputComponents()
        {
            inputComponents.Clear();
        }

        #endregion Public Methods
    }
}