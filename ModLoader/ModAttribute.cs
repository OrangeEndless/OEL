using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Collections . ObjectModel;

namespace OrangeEndless
{
    [AttributeUsage ( AttributeTargets . Class )]
    sealed public class ModAttribute : Attribute
    {
        public string Name { get; private set; }

        public string Author { get; private set; }

        public string Introduction { get; private set; }

        public Guid Id { get; private set; }

        public Collection<Guid> Demand { get; private set; }

        public ModAttribute ( string name , string author , string introduction , string id , string [ ] demand )
        {
            Name = name;
            Author = author;
            Introduction = introduction;
            Id = Guid . Parse ( id );
            Demand = new Collection<Guid> ( );
            if ( demand != null )
            {
                foreach ( var item in demand )
                {
                    Demand . Add ( Guid . Parse ( item ) );
                }

            }
        }
    }

}
