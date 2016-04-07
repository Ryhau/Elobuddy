using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace SoloAatrox
{
    class Program
    {
        public const string ChampionName = "Aatrox";

        public static Spell.Skillshot Q;

        public static Spell.Skillshot E;

        public static Spell.Active R;

        public static AIHeroClient myHero { get { return ObjectManager.Player; } }


        public static AIHeroClient Player
        {
            get { return ObjectManager.Player; }
        }


        

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }






        private static void Loading_OnLoadingComplete(EventArgs arg)
        {
            

            if (Player.ChampionName != "Aatrox") return;

            Game.OnTick += delegate
            {

                var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Magical, Player.ServerPosition);
                var targetE = TargetSelector.GetTarget(E.Range, DamageType.Magical, Player.ServerPosition);


                if (targetQ != null)
                {
                    if (Q.IsReady() && Q.IsInRange(targetQ))
                    {
                        var predQ = Q.GetPrediction(targetQ);

                        Q.Cast(predQ.CastPosition);


                    }
                }

                if (targetE != null)
                {
                    if (E.IsReady() && E.IsInRange(targetE))
                    {
                        var predE = E.GetPrediction(targetQ);

                        E.Cast(predE.CastPosition);


                    }
                }




                if (R.IsReady())
                {
                    if (Player.HealthPercent <= 50)
                    {
                        R.Cast();

                    }

                }


            };

            Bootstrap.Init(null);

            Q = new Spell.Skillshot(SpellSlot.Q, 650, SkillShotType.Circular, (int)0.6f, 250, 2000);
            E = new Spell.Skillshot(SpellSlot.E, 1075, SkillShotType.Linear, (int)0.25f, 35, 1250);
            R = new Spell.Active(SpellSlot.R, 550);

            Chat.Print("<font color='#a0a0a0'>SoloAatrox by</font><font color='#957123'> Ryhau<font color='#a0a0a0'> loaded!</font>");
            Chat.Print("<font color='#a0a0a0'>Visit </font><font color='#6ba6d4'>https://github.com/Ryhau/ElobuddyAddons <font color='#a0a0a0'>for more AddOns!</font>");
            Chat.Print("<font color='#a0a0a0'>Have a nice day!</font>");





        }

        public static List<Item> AatroxItems = new List<Item>()
        {
            new Item(0, 0),
            new Item(350, (int)ItemId.Sapphire_Crystal),         //1
            new Item(400, (int)ItemId.Tear_of_the_Goddess),      //2
            new Item(1100, (int)ItemId.Sorcerers_Shoes),         //3
            new Item(1250, (int)ItemId.Needlessly_Large_Rod),    //4
            new Item(1100, (int)ItemId.Archangels_Staff),        //5
            new Item(2800, (int)ItemId.Frozen_Heart),            //6
            new Item(1200, (int)ItemId.Catalyst_the_Protector),  //7
            new Item(1800, (int)ItemId.Rod_of_Ages),             //8
            new Item(2650, (int)ItemId.Void_Staff),              //9
            new Item(850, (int)ItemId.Blasting_Wand),            //10
            new Item(2150, (int)ItemId.Rabadons_Deathcap)        //11
        };




    }





}





