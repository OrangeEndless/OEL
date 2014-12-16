using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Reflection;
using System . Threading . Tasks;
using System . Runtime . CompilerServices;
using System . Timers;

namespace CoreMod
{
	[OrangeEndless . Mod ( "Core" , "Wencey Wang" , "The Core Function For OrangeEndLess" , "00000001-0000-0000-0000-000000000001" , new string [ ] { } )]
	public class Main : OrangeEndless . Mod
	{
		public Timer Ticks;

		public decimal NumberOfOrange { get; set; }

		public decimal NumberOfMoney { get; set; }

		public List<Building> ListOfBuilding;

		public async Task Start ( )
		{
			for ( int i = 0 ; i <= 11 ; i++ )
			{
				var building = await Building . LoadBuilding ( i );
				ListOfBuilding . Add ( building );
			}
		}

		public async Task Stop ( )
		{
			foreach ( var item in ListOfBuilding )
			{
				await item . Stop ( );
			}
		}

		public async void Tick ( object sender , ElapsedEventArgs e )
		{
			await Task . Run ( ( ) =>
			{
				foreach ( var item in ListOfBuilding )
				{
					NumberOfOrange += item . CPS;
				}
			} );
			await Task . Run ( ( ) => { } );
			await Task . Run ( ( ) => { } );
		}

		public Main ( OrangeEndless . Core core )
		{
			Ticks = new Timer ( 1000 );
			Ticks . AutoReset = true;
			Ticks . Enabled = true;
			Ticks . Elapsed += Tick;

		}

	}
}
