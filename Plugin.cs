using System;

using Exiled.API.Features;

namespace RoleCategories
{
    public class Plugin : Plugin<Config, Translation>
    {
        public static Plugin Instance { get; private set; }
        public Plugin()
        {
            Instance = this;
        }
        public override string Author => "FalconinVoid";
        public override string Name => "RoleCategories";
        public override string Prefix => "RCategories";
        public override Version RequiredExiledVersion => new Version(9, 5, 1);
        public override Version Version => new Version(1, 1, 0);
        public override void OnEnabled()
        {
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}
