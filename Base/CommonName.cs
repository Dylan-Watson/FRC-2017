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
    public sealed class CommonName
    {
        //declaired for use in auton
        public static readonly CommonName Right_drive1 = new CommonName("rightDrive_1");

        public static readonly CommonName Right_drive2 = new CommonName("rightDrive_2");
        public static readonly CommonName Left_drive1 = new CommonName("leftDrive_1");
        public static readonly CommonName Left_drive2 = new CommonName("leftDrive_2");
        public static readonly CommonName Gear_switch = new CommonName("gearShift");

        #region Private Fields

        private readonly string name;

        #endregion Private Fields

        #region Public Constructors

        public CommonName(string name)
        {
            this.name = name;
        }

        #endregion Public Constructors

        private bool Equals(CommonName other) => string.Equals(name, other.name);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj is CommonName && Equals((CommonName) obj);
        }

        public override int GetHashCode() => name.GetHashCode();

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

        public static bool operator !=(CommonName x, CommonName y) => x?.name != y?.name;

        public static bool operator ==(CommonName x, CommonName y) => x?.name == y?.name;

        public override string ToString() => name;

        #endregion Public Methods
    }
}