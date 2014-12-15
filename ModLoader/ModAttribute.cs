using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrangeEndless
{

	public class ModAttribute:Attribute
	{
		public string Name { get; set; }

		public string Author { get; set; }

		public string Introduction { get; set; }

		public Guid ID { get; set; }

		public List<Guid> Demand { get; set; }


		public ModAttribute(string name,string author,string introduction,string id,string[] demend)
		{
			Name = name;
			Author = author;
			Introduction = introduction;
			ID = Guid . Parse ( id );
			Demand = new List<Guid> ( );
			foreach ( var item in demend )
			{
				Demand . Add ( Guid . Parse ( item ) );
			}
		}
	}

}
