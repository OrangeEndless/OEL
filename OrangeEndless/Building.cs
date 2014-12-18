using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Globalization;

namespace OrangeEndless
{
	public class Building
	{
		private decimal startcps;

		public decimal StartCps
		{
			get { return startcps; }
			set { startcps = decimal . Ceiling ( value ); }
		}


		private decimal starprice;

		public decimal StartPrice
		{
			get { return starprice; }
			set { starprice = decimal . Ceiling ( value ); }
		}


		public decimal Price
		{
			get
			{
				return decimal . Ceiling ( StartPrice * Convert . ToDecimal ( Math . Pow ( 1.15 , ( double ) Number ) ) );
			}
		}

		public decimal Cps
		{
			get
			{
				return decimal . Ceiling ( StartCps * Number * Convert . ToDecimal ( Math . Pow ( 2 , ( double ) Level ) ) );
			}
		}

		private decimal number;

		public decimal Number
		{
			get { return number; }
			set { number = decimal . Ceiling ( value ); }
		}

		private decimal level;

		public decimal Level
		{
			get { return level; }
			set { level = decimal . Ceiling ( value ); }
		}

		public string Name { get; set; }

		public string Introduction { get; set; }

		public string Id { get; set; }

		public async Task Suspend ( )
		{
			await Task . Run ( ( ) =>
			{
				Properties . Settings . Default [ Id + "Level" ] = Level;
				Properties . Settings . Default [ Id + "Number" ] = Number;
			} );
		}

		public async static Task<Building> LoadBuilding ( int key )
		{
			return await Task . Run ( ( ) =>
			{
				string id = key . ToString ( CultureInfo . InvariantCulture );
				var Temp = new Building
				{
					Name = BuildingsNameResource . ResourceManager . GetString ( "R" + id , CultureInfo . InvariantCulture ) ,
					Introduction = BuildingsIntroductionResourse . ResourceManager . GetString ( "R" + id , CultureInfo . InvariantCulture ) ,
					StartCps = Convert . ToDecimal ( BuildingsStartCpsResourse . ResourceManager . GetString ( "R" + id , CultureInfo . InvariantCulture ) , CultureInfo . InvariantCulture ) ,
					StartPrice = Convert . ToDecimal ( BuildingsStartPriceResourse . ResourceManager . GetString ( "R" + id , CultureInfo . InvariantCulture ) , CultureInfo . InvariantCulture ) ,
					Level = ( decimal ) ( Properties . Settings . Default [ id + "Level" ] ?? ( object ) 0d ) ,
					Number = ( decimal ) ( Properties . Settings . Default [ id + "Number" ] ?? ( object ) 0d ) ,
					Id = id
				};
				return Temp;
			}
			);
		}

	}
}
