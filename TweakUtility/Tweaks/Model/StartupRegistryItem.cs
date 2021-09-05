using Microsoft.Win32;
using System;

namespace TweakUtility.Tweaks.Model
{
    public class StartupRegistryItem : StartupItem
    {
        public RegistryKey Key { get; set; }

        public override string Name
        {
            get => Key.Name;
            set
            {
            }
        }

        public override string Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Add() => throw new NotImplementedException();

        public override void Remove() => throw new NotImplementedException();
    }
}