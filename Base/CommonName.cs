/****************************** Header ******************************\
Class Name: CommonName
Summary: Provides a list of all physical components on the robot, and
their respective names in software.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Base
{
    /// <summary>
    /// Class to define the way IComponents are found and bound to controls, via their CommonNames, 
    /// basically a string wrapper with utilities
    /// </summary>
    public sealed class CommonName
    {
        //declaired for use in auton
        //public static readonly CommonName Gear_switch = new CommonName("gearShift");

        #region Private Fields

        private readonly string name;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">vaule of the CommonName (the name)</param>
        public CommonName(string name)
        {
            this.name = name;
        }

        #endregion Public Constructors

        private bool Equals(CommonName other) => string.Equals(name, other.name);

        /// <summary>
        /// Overrides the Equals method for CommonNames
        /// </summary>
        /// <param name="obj">object of interest</param>
        /// <returns>comparison result</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is CommonName && Equals((CommonName) obj);
        }

        /// <summary>
        /// Returns the hash of the CommonName's string value
        /// </summary>
        /// <returns>Hash value as int</returns>
        public override int GetHashCode() => name.GetHashCode();

        /// <summary>
        /// Binds a config file value to a CommonName
        /// </summary>
        /// <param name="attribute">attribute of interest from config</param>
        /// <returns>List of bindings</returns>
        [Obsolete("ToBindCommonName is no longer used, as bindings are dynamically done within the Config class.")]
        public static List<CommonName> ToBindCommonName(XAttribute attribute)
        {
            var bindings = new List<CommonName>();
            var type = typeof(CommonName);

            if (attribute == null)
                return null;

            var values = attribute.Value.Contains(",")
                ? attribute.Value.Split(',').ToList()
                : new List<string> {attribute.Value};

            values = values.Distinct().ToList();

            for (var i = 0; i < values.Count; i++)
                values[i] = values[i].Replace(" ", string.Empty);

            var names = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            var iterations = values.Count;

            for (var i = 0; i < iterations; i++)
                foreach (var v in names.Select(t => t.GetValue(null)))
                {
                    if (v.ToString() == values[0])
                    {
                        bindings.Add((CommonName) v);
                        values.Remove(values[0]);
                        break;
                    }

                    if (!values.Any())
                        return bindings;
                }

            foreach (var str in values)
                Report.Error(
                    $"Control attempting to bind to the resource \"{str}\" that has no CommonName, or doesn't exist!");

            return bindings;
        }

        #region Public Methods

        /// <summary>
        /// != operator for CommonNames
        /// </summary>
        /// <param name="x">object of interest</param>
        /// <param name="y">object of interest</param>
        /// <returns>comparison result</returns>
        public static bool operator !=(CommonName x, CommonName y) => x?.name != y?.name;

        /// <summary>
        /// == operator for CommonNames
        /// </summary>
        /// <param name="x">object of interest</param>
        /// <param name="y">object of interest</param>
        /// <returns>comparison result</returns>
        public static bool operator ==(CommonName x, CommonName y) => x?.name == y?.name;

        /// <summary>
        /// Overrides ToString for CommonName
        /// </summary>
        /// <returns>string value of the CommonName</returns>
        public override string ToString() => name;

        #endregion Public Methods
    }
}