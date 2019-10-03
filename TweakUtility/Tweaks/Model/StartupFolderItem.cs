using System;
using System.IO;

namespace TweakUtility.Tweaks.Model
{
    public class StartupFolderItem : StartupItem
    {
        public bool Public
        {
            get => System.IO.Path.GetDirectoryName(this.Path).StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup));
        }

        public override string Name
        {
            get => System.IO.Path.GetFileNameWithoutExtension(this.Path);
            set
            {
                string directory = System.IO.Path.GetDirectoryName(this.Path);
                string extension = System.IO.Path.GetExtension(this.Path);
                string newPath = System.IO.Path.Combine(directory, $"{value}{extension}");

                File.Move(this.Path, newPath);

                this.Path = newPath;
            }
        }

        private string _path;

        public StartupFolderItem(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Specified path of startup item not found", path);
            }

            this.Path = path;
        }

        public override string Path
        {
            get => _path;
            set => _path = value;
        }

        public override void Add() => throw new NotImplementedException();

        public override void Remove()
        {
            if (File.Exists(this.Path))
            {
                File.Delete(this.Path);
            }
        }
    }
}