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

		public List<Type> GetModList ( )
		{
			List<Type> ListOfMod = new List<Type> ( );
			foreach ( var fil in ModFiles )
			{
				var types = ( Assembly . Load ( fil ) ) . GetTypes ( );
				foreach ( var typ in types )
				{
					if ( typ . IsSubclassOf ( typeof ( Mod ) ) )
					{
						ListOfMod . Add ( typ );
					}
				}
			}

			return ListOfMod;
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
					Start =  ( ) => { LoadingMod . Start ( ); } ,
					Stop = ( Deadline ) => { LoadingMod . Stop ( Deadline ); } ,
				} );
			}
		}

		public Core ( List<string> modfiles )
		{
			ListOfMod = new List<LoadedMod> ( );

			ModFiles = modfiles;

		}

		public void Start ( )
		{
			foreach ( var item in ListOfMod )
			{
				item . Start ( );
			}
		}

		public void Stop ( DateTime Deadline )
		{
			foreach ( var item in ListOfMod )
			{
				item . Stop ( Deadline ) ;
			}
		}
	}
}
