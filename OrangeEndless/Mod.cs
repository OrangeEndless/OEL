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
		void Start ( );

		void Stop ( DateTime DeadLine );


	}


	public struct LoadedMod
	{
		public Action Start;

		public Action<DateTime> Stop;

		public string Name;

		public string Author;

		public string Introduction;

		public Guid ID;

		public List<Guid> Demand;
	}
}
