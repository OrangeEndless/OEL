using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Reflection;
using System . Threading;
using System . Runtime . CompilerServices;
using System . Runtime . InteropServices;
using System . IO;

namespace OrangeEndless
{
	public class Core
	{

		public List<LoadedMod> ListOfMod;

		List<string> ModFiles;

		public async Task<List<Type>> GetModList ( )
		{
			return await Task . Run<List<Type>> ( ( ) =>
				{
					List<Type> ListOfMod = new List<Type> ( );
					foreach ( var fil in ModFiles )
					{
						var types = ( Assembly . LoadFrom ( fil ) ) . GetTypes ( );
						foreach ( var typ in types )
						{
							if ( typ . IsSubclassOf ( typeof ( Mod ) ) )
							{
								ListOfMod . Add ( typ );
							}
						}
					}
					return ListOfMod;
				} );

		}


		public void LoadMod ( List<Type> ModToLoad )
		{
			foreach ( var typ in ModToLoad )
			{
				Mod LoadingMod = Activator . CreateInstance ( typ , this ) as Mod;
				ModAttribute Att = Attribute . GetCustomAttribute ( typ , typeof ( ModAttribute ) ) as ModAttribute;
				ListOfMod . Add ( new LoadedMod
				{
					Name = Att . Name ,
					Author = Att . Author ,
					Introduction = Att . Introduction ,
					ID = Att . ID ,
					Demand = Att . Demand ,
					Start = async ( ) => { await LoadingMod . Start ( ); } ,
					Stop = async ( Deadline ) => { await LoadingMod . Stop ( Deadline ); } ,
				} );
			}
		}

		public Core ( List<string> modfiles )
		{
			ListOfMod = new List<LoadedMod> ( );

			ModFiles = modfiles;

		}

		public async void Start ( )
		{
			foreach ( var item in ListOfMod )
			{
				await item . Start ( );
			}
		}

		public async void Stop ( DateTime Deadline )
		{
			foreach ( var item in ListOfMod )
			{
				await item . Stop ( Deadline );
			}
		}
	}
}
