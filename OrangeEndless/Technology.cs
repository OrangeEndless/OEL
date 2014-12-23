using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Globalization;

namespace OrangeEndless
{
    public class Technology
    {
        public string Name { get; set; }

        public string Introduction { get; set; }

        public string Id { get; set; }

        public bool IsGet { get; set; }

        public Func<Task> Check { get; set; }

        public async Task Suspend ( )
        {
            await Task . Run ( ( ) =>
            {
                Properties . Settings . Default [ Id + "IsGet" ] = IsGet;
            } );
        }

        public static Task<Technology> LoadTechnology ( int key )
        {
            return Task . Run ( ( ) =>
            {
                string id = key . ToString ( CultureInfo . InvariantCulture );
                var Temp=new Technology
                {

                };
                return Temp;
            } );
        }
    }
}
