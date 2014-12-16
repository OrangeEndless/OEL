using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Collections . ObjectModel;

namespace OrangeEndless
{
	[AttributeUsage(AttributeTargets.Class)]
	sealed public class ModAttribute : Attribute
	{
		public string Name { get; set; }

		public string Author { get; set; }

		public string Introduction { get; set; }

		public Guid Id { get; set; }

		public Collection<Guid> Demand { get; set; }


		public ModAttribute ( string name , string author , string introduction , string id , string [ ] demend )
		{
			Name = name;
			Author = author;
			Introduction = introduction;
			Id = Guid . Parse ( id );
			Demand = new Collection<Guid> ( );
			if ( demend != null )
			{
				foreach ( var item in demend )
				{
					Demand . Add ( Guid . Parse ( item ) );
				}

			}
		}
	}

}
