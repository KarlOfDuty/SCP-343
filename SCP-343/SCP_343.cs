﻿using Smod2;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.Piping;

namespace SCP_343
{
	[PluginDetails(
	author = "Mith",
	name = "SCP-343",
	description = "SCP-343 is a passive immortal D-Class Personnel. He spawns with one Flashlight and any weapon he picks up is morphed to prevent violence. He seeks to help out who he deems worthy.",
	id = "Mith.SCP-343",
	version = "2.0.1",
	configPrefix = "343",
	SmodMajor = 3,
	SmodMinor = 10,
	SmodRevision = 5
	)]
	class SCP343 : Plugin
	{
		/// <summary>
		/// This exists so we can reference stuff like <see cref="EventLogic"/> or <see cref="PluginDetails"/>
		/// </summary>
		public SCP343 plugin;

		/// <summary>
		/// This is our main <see cref="EventLogic"/> that we reference so we know who is SCP-343. This also handles all of the events that will happen (Like being disarmed or being shot)
		/// </summary>
		public EventLogic eventLogicVar;

		/// <summary>
		/// Anything with <see cref="ConfigOption"/> is nothing but a config option. They're formatted in such a way that it takes your <see cref="PluginDetails.configPrefix"/> and then puts an underscore after the prefix this config option below would turn into 343_spawnchance
		/// </summary>
		[ConfigOption]
		public readonly float spawnchance = 10f; //Percent chance for SPC-343 to spawn at the start of the round. (0 - 100%)

		[ConfigOption]
		public readonly float opendoortime = 300f; //How many seconds till SCP-343 can open any door.

		[ConfigOption]
		public readonly bool itemconverttoggle = true;

		[ConfigOption]
		public readonly float hp = 101f;

		[ConfigOption]
		public readonly bool nuke_interact = true;

		[ConfigOption]
		public readonly bool debug = false;

		[ConfigOption]
		public readonly bool disable = false;
		
		[ConfigOption]
		public readonly bool broadcast = true;

		[ConfigOption]
		public readonly string broadcastinfo = "";

		[ConfigOption]
		public readonly bool revert = true;

		[ConfigOption]
		public readonly float revert_time = 30f;

		public override void OnDisable()
		{
		}
		
		public override void OnEnable()
		{
			plugin = this;
		}
		
		public override void Register()
		{
			this.AddEventHandlers(eventLogicVar = new EventLogic(this));
			
			this.AddCommand("343spawn", new SpawnSCP343(this, eventLogicVar));
			this.AddCommand("343_version", new SCP343_Version(this));
			this.AddCommand("343_disable", new SCP343_Disable(this));

			/* Old way of doing things (Still valid)
			this.AddConfig(new Smod2.Config.ConfigSetting("343_spawnchance", 10f, true, "Percent chance for SPC-343 to spawn at the start of the round."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_itemconverttoggle", false, true, "Should SPC-343 convert items?"));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_opendoortime", 300, true, "How long in seconds till SPC-343 can open any door."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_hp", -1, true, "How much health should SCP-343 have. Set to -1 for GodMode and if set to anything but -1 then he is counted as a normal SCP and MTF must kill him like a normal SCP."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_nuke_interact", false, true, "Should SPC-343 beable to interact with the nuke."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_disable", false, true, "Should SPC-343 beable to interact with the nuke."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_debug", false, true, "Internal testing config so I stop pushing commits that are broken >:("));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_broadcast", true, true, "When 343 spawns should that person be given information about 343"));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_broadcastinfo", "", true, "What 343 is shown if scp343_broadcast is true."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_heck", true, true, "Should players be allowed to use the .heck343 client command to respawn themselves as d-class within scp343_hecktime seconds of round start."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_hecktime", 30, true, "How long people should beable to respawn themselves as d-class."));
			*/

			//https://github.com/Grover-c13/Smod2/wiki/Enum-Lists#itemtype
			this.AddConfig(new Smod2.Config.ConfigSetting("343_itemdroplist", new int[] {0,1,2,3,4,5,6,7,8,9,10,11,14,17,19,22,27,28,29 }, true, "What items SCP-343 drops instead of picking up."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_itemstoconvert", new int[]{13,16,20,21,23,24,25,26,30}, true, "What items SCP-343 converts."));
			this.AddConfig(new Smod2.Config.ConfigSetting("343_converteditems", new int[]{15}, true, "What a item should be converted to."));
		}
	}
}