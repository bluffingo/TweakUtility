namespace TweakUtility.Tweaks.Model
{
    public abstract class StartupItem
    {
        public abstract string Name { get; set; }

        public abstract string Path { get; set; }

        public abstract void Add();

        public abstract void Remove();
    }
}