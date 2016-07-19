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

namespace Base
{
    public class ActiveCollection
    {
        #region Private Fields

        private readonly Dictionary<string, IComponent> componentCollection = new Dictionary<string, IComponent>();

        #endregion Private Fields

        #region Public Methods

        public List<IComponent> GetLeftDriveMotors
            => (from t in componentCollection.Values where t is Motor select t as Motor).ToList().Where
                (t => t.DriveSide == Motor.Side.Left).ToList().Cast<IComponent>().ToList();

        public List<IComponent> GetRightDriveMotors
            => (from t in componentCollection.Values where t is Motor select t as Motor).ToList().Where
                (t => t.DriveSide == Motor.Side.Right).ToList().Cast<IComponent>().ToList();

        public void AddComponent(IComponent component)
        {
            try
            {
                componentCollection.Add(component.Name, component);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

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

        #endregion Public Methods
    }
}