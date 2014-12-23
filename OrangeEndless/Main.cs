using System;
using System . IO;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Reflection;
using System . Threading . Tasks;
using System . Runtime . CompilerServices;
using System . Timers;
using System . Collections . ObjectModel;
using OrangeEndless . Properties;

namespace OrangeEndless
{
    [OrangeEndless . Mod ( "OrangeEndless" , "Wencey Wang" , "The Core Function For OrangeEndLess" , @"{00000001-0000-0000-0000-000000000001}" , new string [ ] { } )]
    public class OrangeEndlessCore : OrangeEndless . IMod
    {
        Timer Ticks;

        private decimal numberoforange;

        public decimal NumberOfOrange
        {
            get { return numberoforange; }
            set { numberoforange = decimal . Ceiling ( value ); }
        }

        private decimal numberofmoney;

        public decimal NumberOfMoney
        {
            get { return numberofmoney; }
            set { numberofmoney = decimal . Ceiling ( value ); }
        }

        public Collection<Building> ListOfBuilding { get; private set; }

        public Collection<Technology> ListOfTechnology { get; private set; }

        public async Task Start ( )
        {
            var Ts=new List<Task> ( );
            for ( int i = 0 ; i <= 11 ; i++ )
            {
                Ts . Add ( Task . Run ( ( ) =>
                {
                    ListOfBuilding . Add ( Building . LoadBuilding ( i ) );
                } ) );

            }
            await Task . WhenAll ( Ts );
            Ticks . Start ( );
        }

        public async Task Suspend ( )
        {
            Ticks . Stop ( );
            var Ts=new List<Task> ( );
            foreach ( var item in ListOfBuilding )
            {
                Ts . Add ( item . Suspend ( ) );
            }
            await Task . WhenAll ( Ts );
            Settings . Default . Save ( );
        }

        public async void Tick ( object sender , ElapsedEventArgs e )
        {
            var Ts=new List<Task> ( );

            Ts . Add ( Task . Run ( ( ) =>
            {
                foreach ( var item in ListOfBuilding )
                {
                    NumberOfOrange += item . Cps;
                }
            } ) );
            Ts . Add ( Task . Run ( ( ) =>
            {
                foreach ( var item in ListOfTechnology )
                {
                    Ts . Add ( item . Check ( ) );
                }
            } ) );
            Ts . Add ( Task . Run ( ( ) => { } ) );
            await Task . WhenAll ( Ts );
        }

        public async Task<decimal> BuyBuildings ( int index , decimal number )
        {
            return await Task . Run ( ( ) =>
              {
                  decimal havebuy = 0;
                  for ( decimal i = 0 ; i < number && NumberOfMoney >= ListOfBuilding [ index ] . Price ; i++ )
                  {
                      NumberOfMoney -= ListOfBuilding [ index ] . Price;
                      ListOfBuilding [ index ] . Number++;
                      havebuy++;
                  }
                  return havebuy;
              } );
        }

        public async Task<decimal> SellBuildings ( int index , decimal number )
        {
            return await Task . Run ( ( ) =>
            {
                decimal havesell = 0;
                for ( int i = 0 ; i < number && ListOfBuilding [ index ] . Number > 0 ; i++ )
                {
                    ListOfBuilding [ index ] . Number--;
                    NumberOfMoney += ListOfBuilding [ index ] . Price;
                    havesell++;
                }
                return havesell;
            } );
        }

        [System . Diagnostics . CodeAnalysis . SuppressMessage ( "Microsoft.Usage" , "CA1801:ReviewUnusedParameters" , MessageId = "core" )]
        public OrangeEndlessCore ( Core core )
        {
            ListOfBuilding = new Collection<Building> ( );
            Ticks = new Timer ( 1000 );
            Ticks . AutoReset = true;
            Ticks . Enabled = true;
            Ticks . Elapsed += Tick;
        }

    }
}
