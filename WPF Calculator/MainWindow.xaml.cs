using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokeAPI;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class PKMID
        {
            public string Name { get; set; }
            public string URL { get; set; }
        }

        static string[] moves;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBox1.SelectedIndex < moves.Length && ComboBox1.SelectedIndex >= 0)
            {
                int number = 0;
                int.TryParse(moves[ComboBox1.SelectedIndex].Replace("/", string.Empty), out number);
                GetPKMMove(number);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] pokemon_list = new string[] { "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran♀", "Nidorina", "Nidoqueen", "Nidoran♂", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetch’d", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr. Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-Oh", "Celebi", "Treecko", "Grovyle", "Sceptile", "Torchic", "Combusken", "Blaziken", "Mudkip", "Marshtomp", "Swampert", "Poochyena", "Mightyena", "Zigzagoon", "Linoone", "Wurmple", "Silcoon", "Beautifly", "Cascoon", "Dustox", "Lotad", "Lombre", "Ludicolo", "Seedot", "Nuzleaf", "Shiftry", "Taillow", "Swellow", "Wingull", "Pelipper", "Ralts", "Kirlia", "Gardevoir", "Surskit", "Masquerain", "Shroomish", "Breloom", "Slakoth", "Vigoroth", "Slaking", "Nincada", "Ninjask", "Shedinja", "Whismur", "Loudred", "Exploud", "Makuhita", "Hariyama", "Azurill", "Nosepass", "Skitty", "Delcatty", "Sableye", "Mawile", "Aron", "Lairon", "Aggron", "Meditite", "Medicham", "Electrike", "Manectric", "Plusle", "Minun", "Volbeat", "Illumise", "Roselia", "Gulpin", "Swalot", "Carvanha", "Sharpedo", "Wailmer", "Wailord", "Numel", "Camerupt", "Torkoal", "Spoink", "Grumpig", "Spinda", "Trapinch", "Vibrava", "Flygon", "Cacnea", "Cacturne", "Swablu", "Altaria", "Zangoose", "Seviper", "Lunatone", "Solrock", "Barboach", "Whiscash", "Corphish", "Crawdaunt", "Baltoy", "Claydol", "Lileep", "Cradily", "Anorith", "Armaldo", "Feebas", "Milotic", "Castform", "Kecleon", "Shuppet", "Banette", "Duskull", "Dusclops", "Tropius", "Chimecho", "Absol", "Wynaut", "Snorunt", "Glalie", "Spheal", "Sealeo", "Walrein", "Clamperl", "Huntail", "Gorebyss", "Relicanth", "Luvdisc", "Bagon", "Shelgon", "Salamence", "Beldum", "Metang", "Metagross", "Regirock", "Regice", "Registeel", "Latias", "Latios", "Kyogre", "Groudon", "Rayquaza", "Jirachi", "Deoxys", "Turtwig", "Grotle", "Torterra", "Chimchar", "Monferno", "Infernape", "Piplup", "Prinplup", "Empoleon", "Starly", "Staravia", "Staraptor", "Bidoof", "Bibarel", "Kricketot", "Kricketune", "Shinx", "Luxio", "Luxray", "Budew", "Roserade", "Cranidos", "Rampardos", "Shieldon", "Bastiodon", "Burmy", "Wormadam", "Mothim", "Combee", "Vespiquen", "Pachirisu", "Buizel", "Floatzel", "Cherubi", "Cherrim", "Shellos", "Gastrodon", "Ambipom", "Drifloon", "Drifblim", "Buneary", "Lopunny", "Mismagius", "Honchkrow", "Glameow", "Purugly", "Chingling", "Stunky", "Skuntank", "Bronzor", "Bronzong", "Bonsly", "Mime Jr.", "Happiny", "Chatot", "Spiritomb", "Gible", "Gabite", "Garchomp", "Munchlax", "Riolu", "Lucario", "Hippopotas", "Hippowdon", "Skorupi", "Drapion", "Croagunk", "Toxicroak", "Carnivine", "Finneon", "Lumineon", "Mantyke", "Snover", "Abomasnow", "Weavile", "Magnezone", "Lickilicky", "Rhyperior", "Tangrowth", "Electivire", "Magmortar", "Togekiss", "Yanmega", "Leafeon", "Glaceon", "Gliscor", "Mamoswine", "Porygon-Z", "Gallade", "Probopass", "Dusknoir", "Froslass", "Rotom", "Uxie", "Mesprit", "Azelf", "Dialga", "Palkia", "Heatran", "Regigigas", "Giratina", "Cresselia", "Phione", "Manaphy", "Darkrai", "Shaymin", "Arceus", "Victini", "Snivy", "Servine", "Serperior", "Tepig", "Pignite", "Emboar", "Oshawott", "Dewott", "Samurott", "Patrat", "Watchog", "Lillipup", "Herdier", "Stoutland", "Purrloin", "Liepard", "Pansage", "Simisage", "Pansear", "Simisear", "Panpour", "Simipour", "Munna", "Musharna", "Pidove", "Tranquill", "Unfezant", "Blitzle", "Zebstrika", "Roggenrola", "Boldore", "Gigalith", "Woobat", "Swoobat", "Drilbur", "Excadrill", "Audino", "Timburr", "Gurdurr", "Conkeldurr", "Tympole", "Palpitoad", "Seismitoad", "Throh", "Sawk", "Sewaddle", "Swadloon", "Leavanny", "Venipede", "Whirlipede", "Scolipede", "Cottonee", "Whimsicott", "Petilil", "Lilligant", "Basculin", "Sandile", "Krokorok", "Krookodile", "Darumaka", "Darmanitan", "Maractus", "Dwebble", "Crustle", "Scraggy", "Scrafty", "Sigilyph", "Yamask", "Cofagrigus", "Tirtouga", "Carracosta", "Archen", "Archeops", "Trubbish", "Garbodor", "Zorua", "Zoroark", "Minccino", "Cinccino", "Gothita", "Gothorita", "Gothitelle", "Solosis", "Duosion", "Reuniclus", "Ducklett", "Swanna", "Vanillite", "Vanillish", "Vanilluxe", "Deerling", "Sawsbuck", "Emolga", "Karrablast", "Escavalier", "Foongus", "Amoonguss", "Frillish", "Jellicent", "Alomomola", "Joltik", "Galvantula", "Ferroseed", "Ferrothorn", "Klink", "Klang", "Klinklang", "Tynamo", "Eelektrik", "Eelektross", "Elgyem", "Beheeyem", "Litwick", "Lampent", "Chandelure", "Axew", "Fraxure", "Haxorus", "Cubchoo", "Beartic", "Cryogonal", "Shelmet", "Accelgor", "Stunfisk", "Mienfoo", "Mienshao", "Druddigon", "Golett", "Golurk", "Pawniard", "Bisharp", "Bouffalant", "Rufflet", "Braviary", "Vullaby", "Mandibuzz", "Heatmor", "Durant", "Deino", "Zweilous", "Hydreigon", "Larvesta", "Volcarona", "Cobalion", "Terrakion", "Virizion", "Tornadus", "Thundurus", "Reshiram", "Zekrom", "Landorus", "Kyurem", "Keldeo", "Meloetta", "Genesect", "Chespin", "Quilladin", "Chesnaught", "Fennekin", "Braixen", "Delphox", "Froakie", "Frogadier", "Greninja", "Bunnelby", "Diggersby", "Fletchling", "Fletchinder", "Talonflame", "Scatterbug", "Spewpa", "Vivillon", "Litleo", "Pyroar", "Flabébé", "Floette", "Florges", "Skiddo", "Gogoat", "Pancham", "Pangoro", "Furfrou", "Espurr", "Meowstic", "Honedge", "Doublade", "Aegislash", "Spritzee", "Aromatisse", "Swirlix", "Slurpuff", "Inkay", "Malamar", "Binacle", "Barbaracle", "Skrelp", "Dragalge", "Clauncher", "Clawitzer", "Helioptile", "Heliolisk", "Tyrunt", "Tyrantrum", "Amaura", "Aurorus", "Sylveon", "Hawlucha", "Dedenne", "Carbink", "Goomy", "Sliggoo", "Goodra", "Klefki", "Phantump", "Trevenant", "Pumpkaboo", "Gourgeist", "Bergmite", "Avalugg", "Noibat", "Noivern", "Xerneas", "Yveltal", "Zygarde", "Diancie", "Hoopa", "Volcanion", "Rowlet", "Dartrix", "Decidueye", "Litten", "Torracat", "Incineroar", "Popplio", "Brionne", "Primarina", "Pikipek", "Trumbeak", "Toucannon", "Yungoos", "Gumshoos", "Grubbin", "Charjabug", "Vikavolt", "Crabrawler", "Crabominable", "Oricorio", "Cutiefly", "Ribombee", "Rockruff", "Lycanroc", "Wishiwashi", "Mareanie", "Toxapex", "Mudbray", "Mudsdale", "Dewpider", "Araquanid", "Fomantis", "Lurantis", "Morelull", "Shiinotic", "Salandit", "Salazzle", "Stufful", "Bewear", "Bounsweet", "Steenee", "Tsareena", "Comfey", "Oranguru", "Passimian", "Wimpod", "Golisopod", "Sandygast", "Palossand", "Pyukumuku", "Type: Null", "Silvally", "Minior", "Komala", "Turtonator", "Togedemaru", "Mimikyu", "Bruxish", "Drampa", "Dhelmise", "Jangmo-o", "Hakamo-o", "Kommo-o", "Tapu Koko", "Tapu Lele", "Tapu Bulu", "Tapu Fini", "Cosmog", "Cosmoem", "Solgaleo", "Lunala", "Nihilego", "Buzzwole", "Pheromosa", "Xurkitree", "Celesteela", "Kartana", "Guzzlord", "Necrozma", "Magearna", "Marshadow", "Poipole", "Naganadel", "Stakataka", "Blacephalon", "Zeraora" };
            foreach (var pokemon in pokemon_list)
            {
                ComboBox2.Items.Add(pokemon);
            }
        }

        async Task GetPKMMove(int number)
        {
            Move move = await DataFetcher.GetApiObject<Move>(number);
            txtAcc.Text = move.Accuracy.Value.ToString() ?? "100";
            txtPwr.Text = move.Power.Value.ToString() ?? "0";
            txtPp.Text = move.PP.Value.ToString() ?? "0";
            txtType.Text = move.Type.Name.ToString() ?? "null";

        }

        async Task GetPKM( int number, bool recalculate = true )
        {
            Pokemon pkm = await DataFetcher.GetApiObject<Pokemon>(number +1);
            txtName.Text = pkm.Name;
            Random rng = new Random();
            WebClient wc = new WebClient();
            if (pkm.Sprites.FrontMale != null)
            {
                byte[] bytes = wc.DownloadData(pkm.Sprites.FrontMale);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    pkm_img.Source = BitmapFrame.Create(stream,
                                                      BitmapCreateOptions.None,
                                                      BitmapCacheOption.OnLoad);
                }
            } else if (pkm.Sprites.FrontFemale != null)
            {
                byte[] bytes = wc.DownloadData(pkm.Sprites.FrontFemale);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    pkm_img.Source = BitmapFrame.Create(stream,
                                                      BitmapCreateOptions.None,
                                                      BitmapCacheOption.OnLoad);
                }
            }
            if ((bool)gen12.IsChecked)
            {
                if(recalculate)
                {
                    txtBox_level.Text = rng.Next(101).ToString();
                    txtAtk.Text = rng.Next(16).ToString();
                    txtDef.Text = rng.Next(16).ToString();
                    txtSpd.Text = rng.Next(16).ToString();
                    txtSpc.Text = rng.Next(16).ToString();
                    string str = ((int.Parse(txtAtk.Text) & 8) > 0 ? "1" : "0") +
                        ((int.Parse(txtDef.Text) & 8) > 0 ? "1" : "0") +
                        ((int.Parse(txtSpd.Text) & 8) > 0 ? "1" : "0") +
                        ((int.Parse(txtSpc.Text) & 8) > 0 ? "1" : "0").ToString();
                    txtHp.Text = Convert.ToByte(str, 2).ToString();
                    txtAtk_ev.Text = rng.Next(65536).ToString();
                    txtDef_ev.Text = rng.Next(65536).ToString();
                    txtSpd_ev.Text = rng.Next(65536).ToString();
                    txtSpc_ev.Text = rng.Next(65536).ToString();
                    txtHp_ev.Text = rng.Next(65536).ToString();
                }
                txtHp_stat.Text = Math.Floor((((((pkm.Stats[5].BaseValue + int.Parse(txtHp.Text)) * 2 + ((Math.Sqrt(double.Parse(txtHp_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + int.Parse(txtBox_level.Text) + 100)).ToString();
                txtAtk_stat.Text = Math.Floor((((((pkm.Stats[4].BaseValue + int.Parse(txtAtk.Text)) * 2 + ((Math.Sqrt(double.Parse(txtAtk_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtDef_stat.Text = Math.Floor((((((pkm.Stats[3].BaseValue + int.Parse(txtDef.Text)) * 2 + ((Math.Sqrt(double.Parse(txtDef_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtSpd_stat.Text = Math.Floor((((((pkm.Stats[0].BaseValue + int.Parse(txtSpd.Text)) * 2 + ((Math.Sqrt(double.Parse(txtSpd_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtSpc_stat.Text = Math.Floor((((((pkm.Stats[2].BaseValue + int.Parse(txtSpc.Text)) * 2 + ((Math.Sqrt(double.Parse(txtSpc_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
            }
            else
            {
                if (recalculate)
                {
                    txtBox_level.Text = rng.Next(101).ToString();
                    txtAtk.Text = rng.Next(32).ToString();
                    txtDef.Text = rng.Next(32).ToString();
                    txtSpd.Text = rng.Next(32).ToString();
                    txtSpc.Text = rng.Next(32).ToString();
                    txtSpcDef.Text = rng.Next(32).ToString();
                    txtHp.Text = rng.Next(32).ToString();
                    int total_ev = 511;
                    txtAtk_ev.Text = rng.Next(total_ev).ToString();
                    total_ev -= int.Parse(txtAtk_ev.Text);
                    txtDef_ev.Text = rng.Next(total_ev).ToString();
                    total_ev -= int.Parse(txtDef_ev.Text);
                    txtSpd_ev.Text = rng.Next(total_ev).ToString();
                    total_ev -= int.Parse(txtSpd_ev.Text);
                    txtSpc_ev.Text = rng.Next(total_ev).ToString();
                    total_ev -= int.Parse(txtSpc_ev.Text);
                    txtHp_ev.Text = rng.Next(total_ev).ToString();
                    total_ev -= int.Parse(txtHp_ev.Text);
                    txtSpcDef_ev.Text = rng.Next(total_ev).ToString();
                }
                txtHp_stat.Text = Math.Floor((((2 * pkm.Stats[5].BaseValue + int.Parse(txtHp.Text) + ((Math.Sqrt(double.Parse(txtHp_ev.Text))) / 4) * int.Parse(txtBox_level.Text)) / 100) + int.Parse(txtBox_level.Text) + 10)).ToString();
                txtAtk_stat.Text = Math.Floor((((((pkm.Stats[4].BaseValue + int.Parse(txtAtk.Text)) * 2 + ((Math.Sqrt(double.Parse(txtAtk_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtDef_stat.Text = Math.Floor((((((pkm.Stats[3].BaseValue + int.Parse(txtDef.Text)) * 2 + ((Math.Sqrt(double.Parse(txtDef_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtSpd_stat.Text = Math.Floor((((((pkm.Stats[0].BaseValue + int.Parse(txtSpd.Text)) * 2 + ((Math.Sqrt(double.Parse(txtSpd_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtSpc_stat.Text = Math.Floor((((((pkm.Stats[2].BaseValue + int.Parse(txtSpc.Text)) * 2 + ((Math.Sqrt(double.Parse(txtSpc_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
                txtSpcDef_stat.Text = Math.Floor((((((pkm.Stats[1].BaseValue + int.Parse(txtSpcDef.Text)) * 2 + ((Math.Sqrt(double.Parse(txtSpcDef_ev.Text))) / 4)) * int.Parse(txtBox_level.Text)) / 100) + 5)).ToString();
            }
            //foreach (var attack in pkm.Moves)
            //{
            //    ComboBox1.Items.Add(attack.Move.Name);
            //    moves.Add(attack.Move.Url);
            //}

            moves = new string[pkm.Moves.Length];
            for (int i = 0; i < pkm.Moves.Length; i++)
            {
                ComboBox1.Items.Add(pkm.Moves[i].Move.Name);
                //moves.Add();
                moves[i] = pkm.Moves[i].Move.Url.OriginalString.Substring(31);
                //moves.Add(Regex.Match(pkm.Moves[i].Move.Url.OriginalString, @"https://pokeapi.co/api/v2/move/([0-9]*)/").Groups[0].Captures[0].Value);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            moves = new string[] { };
            ComboBox1.SelectedIndex = -1;
            ComboBox1.Items.Clear();
            txtAcc.Text = "";
            txtPwr.Text = "";
            txtType.Text = "";
            txtPp.Text = "";
            GetPKM(ComboBox2.SelectedIndex);
        }

        private void gen3up_Checked(object sender, RoutedEventArgs e)
        {
            lblSpcDef.IsEnabled = true;
            lblSpcDef_ev.IsEnabled = true;
            lblSpcDef_stat.IsEnabled = true;
            txtSpcDef.IsEnabled = true;
            txtSpcDef_ev.IsEnabled = true;
            txtSpcDef_stat.IsEnabled = true;

            lblSpcDef.Visibility = Visibility.Visible;
            lblSpcDef_ev.Visibility = Visibility.Visible;
            lblSpcDef_stat.Visibility = Visibility.Visible;
            txtSpcDef.Visibility = Visibility.Visible;
            txtSpcDef_ev.Visibility = Visibility.Visible;
            txtSpcDef_stat.Visibility = Visibility.Visible;
        }

        private void gen12_Checked(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
            {
                lblSpcDef.IsEnabled = false;
                lblSpcDef_ev.IsEnabled = false;
                lblSpcDef_stat.IsEnabled = false;
                txtSpcDef.IsEnabled = false;
                txtSpcDef_ev.IsEnabled = false;
                txtSpcDef_stat.IsEnabled = false;

                lblSpcDef.Visibility = Visibility.Hidden;
                lblSpcDef_ev.Visibility = Visibility.Hidden;
                lblSpcDef_stat.Visibility = Visibility.Hidden;
                txtSpcDef.Visibility = Visibility.Hidden;
                txtSpcDef_ev.Visibility = Visibility.Hidden;
                txtSpcDef_stat.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetPKM(ComboBox2.SelectedIndex, false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(txtPwr.Text, out int blah))
            {
                int damage = (((((((2 * int.Parse(txtBox_level.Text)) / 5) + 2) * int.Parse(txtPwr.Text) * int.Parse(txtAtk_stat.Text) / int.Parse(txtDef_stat.Text)) / 50) + 2) * 1);
                lblDamage.Content = "Damage: " + damage.ToString();
            }
            else
            {
                lblDamage.Content = "Damage: This is not a damaging move!";
            }
        }
    }
}
