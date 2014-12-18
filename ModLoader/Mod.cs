using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Reflection;
using System . Threading;
using System . Threading . Tasks;
using System . Collections . ObjectModel;

namespace OrangeEndless
{
	public interface IMod
	{
		Task Start ( );

		Task Suspend ( );


	}


	public class LoadedMod
	{
		public Func<Task> Start { get; set; }

		public Func<Task> Suspend { get; set; }

		public string Name { get; set; }

		public string Author { get; set; }

		public string Introduction { get; set; }

		public Guid Id { get; set; }

		public Collection<Guid> Demand { get;set; }
	}
}
