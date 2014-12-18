﻿using System;
using System . IO;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Reflection;
using System . Threading . Tasks;
using System . Runtime . CompilerServices;
using System . Timers;
using System . Collections . ObjectModel;
using OrangeEndless . Properties;

namespace OrangeEndless
{
	[OrangeEndless . Mod ( "OrangeEndless" , "Wencey Wang" , "The Core Function For OrangeEndLess" , @"{00000001-0000-0000-0000-000000000001}" , new string [ ] { } )]
	public class OrangeEndlessCore : OrangeEndless . IMod
	{
		Timer Ticks;

		private decimal numberoforange;

		public decimal NumberOfOrange
		{
			get { return numberoforange; }
			set { numberoforange = decimal . Ceiling ( value ); }
		}

		private decimal numberofmoney;

		public decimal NumberOfMoney
		{
			get { return numberofmoney; }
			set { numberofmoney = decimal . Ceiling ( value ); }
		}

		public Collection<Building> ListOfBuilding { get; private set; }

		public async Task Start ( )
		{
			for ( int i = 0 ; i <= 11 ; i++ )
			{
				var building = await Building . LoadBuilding ( i );
				ListOfBuilding . Add ( building );
			}
			Ticks . Start ( );
		}

		public async Task Suspend ( )
		{
			Ticks . Stop ( );
			foreach ( var item in ListOfBuilding )
			{
				await item . Suspend ( );
			}
			Settings . Default . Save ( );
		}

		public async void Tick ( object sender , ElapsedEventArgs e )
		{
			await Task . Run ( ( ) =>
			{
				foreach ( var item in ListOfBuilding )
				{
					NumberOfOrange += item . Cps;
				}
			} );
			await Task . Run ( ( ) => { } );
			await Task . Run ( ( ) => { } );
		}

		public async void BuyBuildings ( int index , decimal number )
		{
			await Task . Run ( ( ) => { } );
		}

		[System . Diagnostics . CodeAnalysis . SuppressMessage ( "Microsoft.Usage" , "CA1801:ReviewUnusedParameters" , MessageId = "core" )]
		public OrangeEndlessCore ( Core core )
		{
			Ticks = new Timer ( 1000 );
			Ticks . AutoReset = true;
			Ticks . Enabled = true;
			Ticks . Elapsed += Tick;
		}

	}
}
