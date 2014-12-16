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
using System . Collections . ObjectModel;

namespace OrangeEndless
{
	public class Core
	{

		public Collection<LoadedMod> CollectionOfMod;

		List<string> ModFiles;

		public async Task<List<Type>> GetModList ( )
		{
			return await Task . Run<List<Type>> ( ( ) =>
				{
					List<Type> listofmod = new List<Type> ( );
					foreach ( var fil in ModFiles )
					{
						var types = ( Assembly . LoadFrom ( fil ) ) . GetTypes ( );
						foreach ( var typ in types )
						{
							if ( typ . IsSubclassOf ( typeof ( IMod ) ) )
							{
								listofmod . Add ( typ );
							}
						}
					}
					return listofmod;
				} );

		}


		public void LoadMod ( Collection<Type> ModToLoad )
		{
			if ( ModToLoad != null )
			{
				foreach ( var typ in ModToLoad )
				{
					IMod LoadingMod = Activator . CreateInstance ( typ , this ) as IMod;
					ModAttribute Att = Attribute . GetCustomAttribute ( typ , typeof ( ModAttribute ) ) as ModAttribute;
					CollectionOfMod . Add ( new LoadedMod
					{
						Name = Att . Name ,
						Author = Att . Author ,
						Introduction = Att . Introduction ,
						ID = Att . Id ,
						Demand = Att . Demand ,
						Start = async ( ) => { await LoadingMod . Start ( ); } ,
						Stop = async ( ) => { await LoadingMod . Stop ( ); } ,
					} );
				}
			}
		}

		public Core ( List<string> modfiles )
		{
			CollectionOfMod = new Collection<LoadedMod> ( );

			ModFiles = modfiles;

		}

		public async void Start ( )
		{
			foreach ( var item in CollectionOfMod )
			{
				await item . Start ( );
			}
		}

		public async void Stop ()
		{
			foreach ( var item in CollectionOfMod )
			{
				await item . Stop ( );
			}
		}
	}
}
