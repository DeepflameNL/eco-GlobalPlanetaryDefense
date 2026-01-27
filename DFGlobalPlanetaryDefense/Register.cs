namespace DF.GlobalPlanetaryDefense
{
    using Eco.Core.Plugins.Interfaces;

    public class DeepflameModInitializer : IModInit
    {
        public static ModRegistration Register()
        {
            return new ModRegistration()
            {
                ModName = "DF_GlobalPlanetaryDefense",
                ModDescription =
                    "A mod that overhauls the endgame process of building the laser and computer lab.",
                ModDisplayName = "[DF] Global Planetary Defense",
            };
        }
    }
}
