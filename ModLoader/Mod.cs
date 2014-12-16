using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Reflection;
using System . Threading;
using System . Threading . Tasks;

namespace OrangeEndless
{
	public interface Mod
	{
		Task Start ( );

		Task Stop (  );


	}


	public struct LoadedMod
	{
		public Func<Task> Start;

		public Func< Task> Stop;

		public string Name;

		public string Author;

		public string Introduction;

		public Guid ID;

		public List<Guid> Demand;
	}
}
