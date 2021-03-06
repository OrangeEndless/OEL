﻿using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Reflection;
using System . Threading;
using System . Runtime . CompilerServices;
using System . Runtime . InteropServices;
using System . IO;
using System . Collections . ObjectModel;

namespace OrangeEndless
{
    public class Core
    {
        public Collection<LoadedMod> CollectionOfMod { get; private set; }

        Collection<string> ModFiles;

        public async Task<ReadOnlyCollection<Type>> GetModList ( )
        {
            return await Task . Run ( ( ) =>
                {
                    List<Type> listofmod = new List<Type> ( );
                    foreach ( var fil in ModFiles )
                    {
                        var types = ( Assembly . LoadFrom ( fil ) ) . GetTypes ( );
                        foreach ( var typ in types )
                        {
                            if ( typ . IsSubclassOf ( typeof ( IMod ) ) )
                            {
                                listofmod . Add ( typ );
                            }
                        }
                    }
                    return new ReadOnlyCollection<Type> ( listofmod );
                } );

        }


        public void LoadMod ( Collection<Type> modtoload )
        {
            if ( modtoload != null )
            {
                foreach ( var typ in modtoload )
                {
                    //todo:lots
                    ModAttribute Att = Attribute . GetCustomAttribute ( typ , typeof ( ModAttribute ) ) as ModAttribute;
                    IMod LoadingMod = Activator . CreateInstance ( typ , this ) as IMod;

                    CollectionOfMod . Add ( new LoadedMod
                    {
                        Name = Att . Name ,
                        Author = Att . Author ,
                        Introduction = Att . Introduction ,
                        Id = Att . Id ,
                        Demand = Att . Demand ,
                        Start = async ( ) => { await LoadingMod . Start ( ); } ,
                        Suspend = async ( ) => { await LoadingMod . Suspend ( ); } ,
                    } );
                }
            }
        }

        public Core ( Collection<string> modfiles  )
        {
            CollectionOfMod = new Collection<LoadedMod> ( );

            ModFiles = modfiles;



        }

        public async void Start ( )
        {
            foreach ( var item in CollectionOfMod )
            {
                try
                {
                    await item . Start ( );

                }
                catch ( Exception )
                {

                    throw;
                }
            }
        }

        public async void Suspend ( )
        {
            var Ts=new List<Task> ( );
            foreach ( var item in CollectionOfMod )
            {
                Ts.Add(item . Suspend ( ));
            }
            await Task . WhenAll ( Ts );
        }
    }
}
