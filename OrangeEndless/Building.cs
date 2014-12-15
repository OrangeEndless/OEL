using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;

namespace CoreMod
{
	public class Building
	{
		public decimal StartCPS { get; set; }

		public decimal StartPrice { get; set; }

		public decimal Price 
		{
			get 
			{
				return StartPrice * Convert . ToDecimal ( Math . Pow ( 1.15 , Number ) );
			}
		}

		public decimal CPS 
		{ 
			get 
			{ 
				return StartCPS * Number * Convert . ToDecimal ( Math . Pow ( 2 , Level ) ); 
			} 
		}

		public long Number { get; set; }

		public long Level { get; set; }

		public string Name { get; set; }

		public string Introduction { get; set; }

		public async void Stop ( )
		{

		}
	}
}
