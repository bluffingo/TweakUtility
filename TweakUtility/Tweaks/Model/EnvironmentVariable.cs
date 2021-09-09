using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TweakUtility.Tweaks.Model
{
    public class EnvironmentVariable
    {
        private EnvironmentVariable(string name, EnvironmentVariableTarget? target = null)
        {
            this.Name = name;
            this.Target = target;
        }

        public string Name { get; }

        public string Value
        {
            get
            {
                if (this.Target.HasValue)
                {
                    return Environment.GetEnvironmentVariable(this.Name, this.Target.Value);
                }
                else
                {
                    return Environment.GetEnvironmentVariable(this.Name);
                }
            }

            set
            {
                if (this.Target.HasValue)
                {
                    Environment.SetEnvironmentVariable(this.Name, value, this.Target.Value);
                }
                else
                {
                    Environment.SetEnvironmentVariable(this.Name, value);
                }
            }
        }

        public EnvironmentVariableTarget? Target { get; }

        public void Delete() => this.Value = null;

        public static IReadOnlyList<EnvironmentVariable> GetEnvironmentVariables()
        {
            var list = new List<EnvironmentVariable>();

            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                list.Add(new EnvironmentVariable((string)item.Key));
            }

            return list;
        }

        public static IReadOnlyList<EnvironmentVariable> GetEnvironmentVariables(EnvironmentVariableTarget target)
        {
            var list = new List<EnvironmentVariable>();

            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables(target))
            {
                list.Add(new EnvironmentVariable((string)item.Key, target));
            }

            return list;
        }

        public static IReadOnlyList<EnvironmentVariable> GetAllEnvironmentVariables()
        {
            return GetEnvironmentVariables(EnvironmentVariableTarget.Machine)
            .Concat(GetEnvironmentVariables(EnvironmentVariableTarget.User)).ToList();
        }
    }
}