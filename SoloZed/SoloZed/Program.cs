using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Rendering;
using System;
using System.Drawing;

namespace SoloZed
{
    class SoloZed
    {
        private const string MenuName = "SoloZed";
        public const string ChampionName = "Zed";
        private static Menu Menu;
        public static Spell.Skillshot Q;
        public static Spell.Active E;
        public static bool mspamm { get { return Menu["mspamm"].Cast<KeyBind>().CurrentValue; } }

        public static AIHeroClient Player
        {
            get { return ObjectManager.Player; }
        }

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnStart(EventArgs args)
        {
            Menu = MainMenu.AddMenu("Mastery Spammer", "masteryspm");
            Menu.Add("mspamm", new KeyBind("Spamm mastery", false, KeyBind.BindTypes.PressToggle, 'L'));
            Menu.Add("dk", new CheckBox("Draw Keybind"));
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.ChampionName != "Zed") return;

            Q = new Spell.Skillshot(SpellSlot.Q, 925, SkillShotType.Linear, 250, 1700, 50);
            E = new Spell.Active(SpellSlot.E, 280);

            if (mspamm)
            {
                var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Physical, Player.ServerPosition);
                var targetE = TargetSelector.GetTarget(E.Range, DamageType.Physical, Player.ServerPosition);

                if (targetQ != null)
                {
                    if (Q.IsReady() && Q.IsInRange(targetQ))
                    {
                        Q.Cast(targetQ);
                    }
                }

                if (targetE != null)
                {
                    if (E.IsReady() && E.IsInRange(targetE))
                    {
                        E.Cast();
                    }
                }
            }
        }

    }
}
