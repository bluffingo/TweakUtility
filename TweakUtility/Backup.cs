using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TweakUtility
{
    public class Backup
    {
        public List<BackupEntry> Entries = new List<BackupEntry>();

        public string Path { get; }

        public DateTime Date => fileInfo.CreationTime;

        public long Size => fileInfo.Length;

        public string Name => System.IO.Path.GetFileNameWithoutExtension(this.Path);

        private FileInfo fileInfo;

        public Backup(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            this.Path = filePath;
            this.fileInfo = new FileInfo(this.Path);

            var xml = File.ReadAllText(this.Path);
            if (string.IsNullOrWhiteSpace(xml))
            {
                return;
            }

            var serializer = new XmlSerializer(typeof(List<BackupEntry>));
            using (var reader = new StringReader(xml))
            {
                Entries = (List<BackupEntry>)serializer.Deserialize(reader);
            }
        }

        public Backup(params TweakEntry[] tweakEntries)
        {
            foreach (TweakEntry entry in tweakEntries)
            {
                if (!(entry is TweakOption option))
                {
                    continue;
                }

                var backupEntry = new BackupEntry()
                {
                    PageType = option.parent.GetType().FullName,
                    OptionName = option.InternalName
                };

                if (option.Type == typeof(Color))
                {
                    var color = option.GetValue<Color>();
                    var html = ColorTranslator.ToHtml(color);
                    backupEntry.Value = html;
                }
                else
                {
                    backupEntry.Value = option.GetValue<object>();
                }

                Entries.Add(backupEntry);
            }
        }

        public void Apply()
        {
            var pages = Program.GetAllTweakPages();
            foreach (var item in Entries)
            {
                var page = pages.FirstOrDefault(p => p.GetType().FullName == item.PageType);

                if (page == null)
                {
                    Debug.Fail($"[Backup] No page with the name of '{item.PageType}' could be found.");
                    continue;
                }

                var entry = page.Entries.FirstOrDefault(e => e.InternalName == item.OptionName);

                if (!(entry is TweakOption option))
                {
                    Debug.WriteLine("[Backup] Ignoring tweak entry..."); ;
                    continue;
                }

                if (option.Type == typeof(Color))
                {
                    var html = item.Value as string;
                    var color = ColorTranslator.FromHtml(html);
                    option.SetValue(color);
                }
                else
                {
                    option.SetValue(item.Value);
                }
            }
        }

        public void Export(string filePath)
        {
            using (var writer = XmlWriter.Create(filePath))
            {
                this.Export(writer);
            }
        }

        private void Export(XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(List<BackupEntry>));
            serializer.Serialize(writer, this.Entries);
        }

        public void Delete()
        {
            File.Delete(this.Path);
            Program.Backups.Remove(this);
            GC.Collect();
        }
    }

    public struct BackupEntry
    {
        public string PageType;
        public string OptionName;
        public object Value;
    }
}