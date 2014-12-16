using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace OrangeEndless
{
	public class Building
	{
		public decimal StartCps { get; set; }

		public decimal StartPrice { get; set; }

		public decimal Price
		{
			get
			{
				return StartPrice * Convert . ToDecimal ( Math . Pow ( 1.15 , Number ) );
			}
		}

		public decimal Cps
		{
			get
			{
				return StartCps * Number * Convert . ToDecimal ( Math . Pow ( 2 , Level ) );
			}
		}

		public long Number { get; set; }

		public long Level { get; set; }

		public string Name { get; set; }

		public string Introduction { get; set; }

		public async Task Suspend ( )
		{

		}

		public async static Task<Building> LoadBuilding ( int key )
		{
			string ID = "R" + key . ToString ( );
			return new Building
			{

				Name = await Task . Run<string> ( ( ) => { return BuildingsNameResource . ResourceManager . GetString ( ID ); } ) ,
				Introduction = await Task . Run<string> ( ( ) => { return BuildingsIntroductionResourse . ResourceManager . GetString ( ID ); } ) ,
				StartCps = await Task . Run<decimal> ( ( ) => { return Convert . ToDecimal ( BuildingsStartCpsResourse . ResourceManager . GetString ( ID ) ); } ) ,
				StartPrice = await Task . Run<decimal> ( ( ) => { return Convert . ToDecimal ( BuildingsStartPriceResourse . ResourceManager . GetString ( ID ) ); } ) ,

			};
		}

	}
}
