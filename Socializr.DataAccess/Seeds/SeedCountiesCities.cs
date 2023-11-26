using SocializR.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SocializR.DataAccess.Seeds
{
    static class SeedCountiesCities
    { 
        public static void Seed(SocializRContext context)
        {
            if(context.Counties.Any())
            {
                return;
            }

            var counties = new List<County>
            {
                new County
                {
                    Name = "Alba",
                    ShortName = "AB",
                    Cities=new List<City>
                    {
                        new City { Name = "Abrud"},

                        new City { Name = "Aiud"},
                        
                        new City { Name = "Alba Iulia"},
                        
                        new City { Name = "Baia de Arieş"},
                        
                        new City { Name = "Băneasa"},
                        
                        new City { Name = "Bistra"},
                        
                        new City { Name = "Blaj"},
                        
                        new City { Name = "Bucerdea Grânoasă"},
                        
                        new City { Name = "Câlnic"},
                        
                        new City { Name = "Câmpeni"},
                        
                        new City { Name = "Cetatea de Baltă"},
                        
                        new City { Name = "Crăciunelu de Jos"},
                        
                        new City { Name = "Cricău"},
                        
                        new City { Name = "Cugir"},
                        
                        new City { Name = "Daia Română"},
                        
                        new City { Name = "Ighiu"},
                        
                        new City { Name = "Jidvei"},
                        
                        new City { Name = "Lopadea Nouă"},
                        
                        new City { Name = "Noslac"},
                        
                        new City { Name = "Ocna Mureş"},
                        
                        new City { Name = "Rădeşti"},
                        
                        new City { Name = "Râmeţ"},
                        
                        new City { Name = "Roşia Montană"},
                        
                        new City { Name = "Săliştea"},
                        
                        new City { Name = "Sântimbru"},
                        
                        new City { Name = "Săsciori"},
                        
                        new City { Name = "Sebeş"},
                        
                        new City { Name = "Şona"},
                        
                        new City { Name = "Teiuş"},
                        
                        new City { Name = "Tuşnad"},
                        
                        new City { Name = "Unirea"},
                        
                        new City { Name = "Vinţu de Jos"},
                        
                        new City { Name = "Zlatna"},

                        new City { Name = "Dumbrava"},

                        new City { Name = "Vidra"},

                        new City { Name = "Cergău"},

                        new City { Name = "Valea Lungă"},

                        new City { Name = "Mirăslău"},

                        new City { Name = "Doştat"},

                        new City { Name = "Almaşu Mare"},

                        new City { Name = "Stremţ"},

                        new City { Name = "Blandiana"},

                        new City { Name = "Sona"},

                        new City { Name = "Ohaba"},

                        new City { Name = "Mihalţ"},

                        new City { Name = "Hopârta"},
                    }
                },

                new County
                {
                    Name = "Arad",
                    ShortName = "AR",
                    Cities=new List<City>
                    {
                        new City { Name = "Arad"},

                        new City { Name = "Chişineu Criş"},
                        
                        new City { Name = "Gurahonţ"},
                        
                        new City { Name = "Ineu"},
                        
                        new City { Name = "Lipova (AR},"},
                        
                        new City { Name = "Nădlac"},
                        
                        new City { Name = "Şiria"},
                        
                        new City { Name = "Vinga"},

                        new City { Name = "Budeşti"},

                        new City { Name = "Chişinău-Criş"},

                        new City { Name = "Curtici"},

                        new City { Name = "Nădlag"},

                        new City { Name = "Pâncota"},

                        new City { Name = "Pecica"},

                        new City { Name = "Sântana"},

                        new City { Name = "Săvârşin"},

                        new City { Name = "Sebiş"},

                        new City { Name = "Vladimirescu"},

                        new City { Name = "Sintea Mare"},

                        new City { Name = "Frumuşeni"},

                        new City { Name = "Secusigiu"},

                        new City { Name = "Felnac"},

                        new City { Name = "Vârfurile"},

                        new City { Name = "Bocsig"},

                        new City { Name = "Grăniceri"},

                        new City { Name = "Peregu Mare"},

                        new City { Name = "Bârsa"},

                    }
                },

                new County
                {
                    Name = "Argeş",
                    ShortName =  "AG",
                    Cities=new List<City>
                    {
                        new City { Name = "Bascov"},
                        
                        new City { Name = "Colibaşi"},
                        
                        new City { Name = "Corbeni"},
                        
                        new City { Name = "Costeşti"},
                        
                        new City { Name = "Curtea de Argeş"},
                        
                        new City { Name = "Merişani"},
                        
                        new City { Name = "Mioveni"},
                        
                        new City { Name = "Piteşti"},
                        
                        new City { Name = "Slatina"},
                        
                        new City { Name = "Slobozia"},
                        
                        new City { Name = "Ştefăneşti"},
                        
                        new City { Name = "Topoloveni"},
                        
                        new City { Name = "Zărneşti"},

                        new City { Name = "Câmpulung Muscel"},

                        new City { Name = "Catane"},

                        new City { Name = "Coteşti"},

                        //new City { Name = "leşti"},

                        new City { Name = "Popeşti"},

                        new City { Name = "Moşoaia"},

                        new City { Name = "Vedea"},

                        new City { Name = "Poiana Lacului"},

                        new City { Name = "Negraşi"},

                        new City { Name = "Albestii de Arges"},

                        new City { Name = "Buzoeşti"},

                        new City { Name = "Buchia de Sus"},

                        new City { Name = "Hărtiești"},

                        new City { Name = "Prundeni Bărbuceni"},

                        new City { Name = "Lerești"},

                        new City { Name = "Bradu"},

                        new City { Name = "Suici"},

                        new City { Name = "Mărăcineni"},

                        new City { Name = "Cepari"},

                        new City { Name = "Valea Danului"},

                        new City { Name = "Poienarii de Argeş"},

                        new City { Name = "Sălătrucu"},

                        new City { Name = "Brăduleţ"},

                        new City { Name = "Drăganu"},

                        new City { Name = "Valea Iaşului"},

                        new City { Name = "Cetăţeni"},

                        new City { Name = "Lunca Corbului"},

                        new City { Name = "Băiculeşti"},

                        new City { Name = "Tigveni"},

                        new City { Name = "Băbana"},

                        new City { Name = "Oarja"},

                        new City { Name = "Căteasca"},

                    }
                },

                new County
                {
                    Name = "Bacău",
                    ShortName = "BC",
                    Cities=new List<City>
                    {
                        new City { Name = "Bacău"},

                        new City { Name = "Buhuşi"},
                        
                        new City { Name = "Caşin"},
                        
                        new City { Name = "Comăneşti"},
                        
                        new City { Name = "Dărmăneşti"},
                        
                        new City { Name = "Lipova"},
                        
                        new City { Name = "Moineşti"},
                        
                        new City { Name = "Oneşti"},
                        
                        new City { Name = "Parincea"},
                        
                        new City { Name = "Podu Turcului"},
                        
                        new City { Name = "Slănic Moldova"},
                        
                        new City { Name = "Târgu Ocna"},

                        new City { Name = "Baraţi"},

                        new City { Name = "Bogdan Vodă"},
                    
                        new City { Name = "Odobeşti"},

                        new City { Name = "Urechesti"},

                        new City { Name = "Pârjol"},

                        new City { Name = "Zemeş"},

                        new City { Name = "Dofteana"},

                        new City { Name = "Nicolae Balcescu"},

                        new City { Name = "Scorţeni"},

                        new City { Name = "Fărăoani"},

                        new City { Name = "Gioseni"},

                        new City { Name = "Bereşti-Tazlău"},

                        new City { Name = "Ghimeş"},

                        new City { Name = "Izvorul Berheciului"},

                        new City { Name = "Bereşti Bistriţa"},

                        new City { Name = "Mărgineni"},

                        new City { Name = "Mănăstirea Caşin"},

                        new City { Name = "Sascut"},

                        new City { Name = "Răchitoasa"},

                        new City { Name = "Agaş"},

                        new City { Name = "Palanca"},

                        new City { Name = "Caiuti"},

                        new City { Name = "Gura Văii"},

                        new City { Name = "Brusturoasa"},

                        new City { Name = "Ardeoani"},

                        new City { Name = "Găiceana"},

                        new City { Name = "Prăjeşti"},

                        new City { Name = "Balcani"},

                        new City { Name = "Pârgăreşti"},

                        new City { Name = "Coţofăneşti"},

                        new City { Name = "Corbasca"},

                        new City { Name = "Uat Huruieşti"},

                        new City { Name = "Letea Veche"},

                        new City { Name = "Orbeni"},

                        new City { Name = "Motoşeni"},

                        new City { Name = "Negri"},

                        new City { Name = "Oituz"},

                        new City { Name = "Răcăciuni"},

                        new City { Name = "Stănişeşti"},

                        new City { Name = "Hemeiuş"},

                    }
                },

                new County
                {
                    Name = "Bihor",
                    ShortName =  "BH",
                    Cities=new List<City>
                    {
                        new City { Name = "Aleşd"},

                        new City { Name = "Aştileu"},
                        
                        new City { Name = "Băile Felix"},
                        
                        new City { Name = "Balc"},
                                          
                        new City { Name = "Batar"},
                                          
                        new City { Name = "Beiuş"},
                                          
                        new City { Name = "Biharia"},
                                          
                        new City { Name = "Boianu Mare"},
                                          
                        new City { Name = "Cherechiu"},
                                          
                        new City { Name = "Cociuba Mare"},
                                          
                        new City { Name = "Copăcel"},
                                          
                        new City { Name = "Girişu de Criş"},
                                          
                        new City { Name = "Holod"},
                                          
                        new City { Name = "Husasau de Tinca"},
                                          
                        new City { Name = "Lugaşu de Jos"},
                                          
                        new City { Name = "Mădăraş"},
                                          
                        new City { Name = "Măgeşti"},
                                          
                        new City { Name = "Marghita"},
                                          
                        new City { Name = "Olcea"},
                                          
                        new City { Name = "Oradea"},
                                          
                        new City { Name = "Oşorhei"},
                                          
                        new City { Name = "Săcuieni"},
                                          
                        new City { Name = "Sălacea"},
                                          
                        new City { Name = "Salonta"},
                                          
                        new City { Name = "Sîmbăta"},
                                          
                        new City { Name = "Ştei"},
                                          
                        new City { Name = "Şuncuiuş"},
                                          
                        new City { Name = "Suplacu de Barcău"},
                        
                        new City { Name = "Ţetchea"},
                        
                        new City { Name = "Tileagd"},
                        
                        new City { Name = "Tulca"},
                        
                        new City { Name = "Valea lui Mihai"},
                        
                        new City { Name = "Vascău"},
                        
                        new City { Name = "Viişoara"},

                        new City { Name = "Borşa"},

                        new City { Name = "Bratca"},

                        new City { Name = "Şimian"},

                        new City { Name = "Popeşti"},

                        new City { Name = "Vadu Crişului"},

                        new City { Name = "Vaşcău"},

                        new City { Name = "Derna"},

                        new City { Name = "Toboliu"},

                        new City { Name = "Sălard"},

                    }
                },

                new County
                {
                    Name = "Bistriţa Năsăud",
                    ShortName =  "BN",
                    Cities=new List<City>
                    {
                        new City { Name = "Beclean"},

                        new City { Name = "Bistriţa"},
                        
                        new City { Name = "Bistriţa Năsăud"},
                        
                        new City { Name = "Năsăud"},
                        
                        new City { Name = "Sângeorz Băi"},

                        new City { Name = "Petru Rareş"},

                        new City { Name = "Salva"},

                        new City { Name = "Romuli"},

                        new City { Name = "Nuşeni"},

                        new City { Name = "Rodna"},

                        new City { Name = "Ilva Mică"},

                        new City { Name = "Ilva Mare"},

                        new City { Name = "Lechinţa"},

                        new City { Name = "Leşu"},

                        new City { Name = "Rebrişor"},

                        new City { Name = "Rebrişoara"},

                    }
                },
 
                new County
                {
                    Name = "Botoşani",
                    ShortName =  "BT",
                    Cities=new List<City>
                    {
                        new City { Name = "Botoşani"},

                        new City { Name = "Călăraşi"},
                        
                        new City { Name = "Darabani"},
                        
                        new City { Name = "Dorohoi"},
                        
                        new City { Name = "Saveni"},
                        
                        new City { Name = "Ştefăneşti"},

                        new City { Name = "Suharău"},

                        new City { Name = "Româneşti"},

                        new City { Name = "Roma"},

                        new City { Name = "Stauceni"},

                        new City { Name = "Manoleasa"},

                        new City { Name = "Bucecea"},

                        new City { Name = "Hlipiceni"},

                        new City { Name = "Ibăneşti"},

                        new City { Name = "Tudora"},

                        new City { Name = "Mileanca"},

                        new City { Name = "Şendriceni"},

                        new City { Name = "Drăguşeni"},

                        new City { Name = "Mitoc"},

                        new City { Name = "Pomârla"},

                        new City { Name = "Truşeşti"},

                        new City { Name = "Corlăţeni"},

                        new City { Name = "Cristeşti"},

                        new City { Name = "Cristineşti"},

                        new City { Name = "Dersca"},

                        new City { Name = "Mihăileni"},

                        //new City { Name = "rbăneşti"},

                        new City { Name = "Budeşti"},

                        new City { Name = "Hudești"},

                        new City { Name = "Vlăsineşti"},

                        new City { Name = "Dimacheni"},

                        new City { Name = "Mihai Eminescu"},

                        new City { Name = "Coşula"},

                        new City { Name = "Suliţa"},

                        new City { Name = "Mihălășeni"},

                        new City { Name = "Havîrna"},

                        new City { Name = "Ungureni"},

                        new City { Name = "Copălău"},

                        new City { Name = "Dângeni"},

                        new City { Name = "Vorona"},

                        new City { Name = "Curtești"},

                        new City { Name = "Cândeşti"},

                    }
                },

                new County
                {
                    Name = "Braşov",
                    ShortName = "BV",
                    Cities=new List<City>
                    {
                        new City { Name = "Bran"},

                        new City { Name = "Braşov"},
                        
                        new City { Name = "Codlea"},
                        
                        new City { Name = "Făgăraş"},
                        
                        new City { Name = "Moeciu"},
                        
                        new City { Name = "Poiana Braşov"},
                        
                        new City { Name = "Predeal"},
                        
                        new City { Name = "Râşnov"},
                        
                        new City { Name = "Rupea"},
                        
                        new City { Name = "Săcele"},
                        
                        new City { Name = "Timişul de Jos"},
                        
                        new City { Name = "Timişul de Sus"},
                        
                        new City { Name = "Tohanul Nou"},
                        
                        new City { Name = "Victoria"},
                        
                        new City { Name = "Vulcan"},
                        
                        new City { Name = "Zărneşti"},

                        new City { Name = "Ghimbav"},

                        new City { Name = "Crizbav"},

                        new City { Name = "Prejmer"},

                        new City { Name = "Jibert"},

                        new City { Name = "Feldioara"},

                        new City { Name = "Părău"},

                        new City { Name = "Ticuş"},

                        new City { Name = "Buneşti"},

                        new City { Name = "Fundata"},

                        new City { Name = "Şercaia"},

                        new City { Name = "Poiana Mărului"},

                        new City { Name = "Bod"},

                        new City { Name = "Măieruș"},

                        new City { Name = "Șoarș"},

                        new City { Name = "Apata"},

                        new City { Name = "Ormeniş"},

                    }
                },
 
                new County
                {
                    Name = "Brăila",
                    ShortName = "BR",
                    Cities=new List<City>
                    {
                        new City { Name = "Brăila"},

                        new City { Name = "Făurei"},
                        
                        new City { Name = "Ianca"},
                        
                        new City { Name = "Însurăţei"},

                        new City { Name = "Cireşu"},

                        new City { Name = "Ruşeţu"},

                        new City { Name = "Salcia"},

                        new City { Name = "Chiscani"},

                        new City { Name = "Gemenele"},

                        new City { Name = "Tichileşti"},

                        new City { Name = "Viziru"},

                        new City { Name = "Vădeni"},

                        new City { Name = "Silistea"},

                        new City { Name = "Dudeşti"},

                        new City { Name = "Berteştii de Jos"},

                        new City { Name = "Movila Miresii"},

                        new City { Name = "Bărăganul"},

                        new City { Name = "Bordei Verde"},

                        new City { Name = "Surdila Găiseanca"},

                        new City { Name = "Traian"},

                        new City { Name = "Roşiori"},

                        new City { Name = "Vişani"},

                        new City { Name = "Măraşu"},

                        new City { Name = "Şuţeşti"},

                        new City { Name = "Jirlău"},

                        new City { Name = "Galbenu"},

                    }
                },
 
                new County
                {
                    Name = "Bucureşti",
                    ShortName = "B ",
                    Cities=new List<City>
                    {
                        new City { Name = "Bucureşti"},
                    }
                },
 
                new County
                {
                    Name = "Buzău",
                    ShortName = "BZ",
                    Cities=new List<City>
                    {
                        new City { Name = "Buzău"},

                        new City { Name = "Merei"},
                        
                        new City { Name = "Nehoiu"},
                        
                        new City { Name = "Pătârlagele"},
                        
                        new City { Name = "Poanele"},
                        
                        new City { Name = "Râmnicu Sărat"},
                        
                        new City { Name = "Vadu Paşi"},

                        new City { Name = "Amaru"},

                        new City { Name = "Bălăceanu"},

                        new City { Name = "Balta Albă"},

                        new City { Name = "Beceni"},

                        new City { Name = "Berca"},

                        new City { Name = "Bisoca"},

                        new City { Name = "Blăjani"},

                        new City { Name = "Boldu"},

                        new City { Name = "Brădeanu"},

                        new City { Name = "Brăeşti"},

                        new City { Name = "Budeşti"},

                        new City { Name = "Calvini"},

                        new City { Name = "Căneşti"},

                        new City { Name = "Chiliile"},

                        new City { Name = "Chiojdu"},

                        new City { Name = "Cilibia"},

                        new City { Name = "Cochirleanca"},

                        new City { Name = "Colţi"},

                        new City { Name = "Cozieni"},

                        new City { Name = "Galbinasi"},

                        new City { Name = "Grebănu"},

                        new City { Name = "Măgura"},

                        new City { Name = "Maracineni"},

                        new City { Name = "Mărgăriteşti"},

                        new City { Name = "Mînzăleşti"},

                        new City { Name = "Movila Banului"},

                        new City { Name = "Murgeşti"},

                        new City { Name = "Naeni"},

                        new City { Name = "Odăile"},

                        new City { Name = "Padina"},

                        new City { Name = "Pănădău"},

                        new City { Name = "Pardoşi"},

                        new City { Name = "Pietroasele"},

                        new City { Name = "Podria"},

                        new City { Name = "Poşta Cîlnău"},

                        new City { Name = "Puieşti"},

                        new City { Name = "Racoviţeni"},

                        new City { Name = "Rîmnicelu"},

                        new City { Name = "Robeasca"},

                        new City { Name = "Săgeata"},

                        new City { Name = "Săhăteni"},

                        new City { Name = "Săpoca"},

                        new City { Name = "Scorţoasa"},

                        new City { Name = "Scutelnici"},

                        new City { Name = "Siriu"},

                        new City { Name = "Smeeni"},

                        new City { Name = "Stîlpu"},

                        new City { Name = "Tisău"},

                        new City { Name = "Topliceni"},

                        new City { Name = "Ulmeni"},

                        new City { Name = "Unguriu"},

                        new City { Name = "Valea Rîmnicului"},

                        new City { Name = "Veneşti"},

                        new City { Name = "Ziduri"},

                        new City { Name = "Ghergheasa"},

                        new City { Name = "Ţinteşti"},

                    }
                },
 
                new County
                {
                    Name = "Caraş Severin",
                    ShortName = "CS",
                    Cities=new List<City>
                    {
                        new City { Name = "Anina"},

                        new City { Name = "Băile Herculane"},
                        
                        new City { Name = "Bocşa"},
                        
                        new City { Name = "Bozovici"},
                        
                        new City { Name = "Caransebeş"},
                        
                        new City { Name = "Moldova Nouă"},
                        
                        new City { Name = "Moldova Veche"},
                        
                        new City { Name = "Oraviţa"},
                        
                        new City { Name = "Oţelu Roşu"},
                        
                        new City { Name = "Reşiţa"},

                        new City { Name = "Sasca Montană"},

                        new City { Name = "Sacu"},

                        new City { Name = "Iablaniţa"},

                        new City { Name = "Băutari"},

                        new City { Name = "Forotic"},

                        new City { Name = "Racasdia"},

                        new City { Name = "Păltiniş"},

                        new City { Name = "Bolvașnița"},

                        new City { Name = "Zăvoi"},

                        new City { Name = "Vrani"},

                        new City { Name = "Bucoșnița"},

                        new City { Name = "Luncăviţa"},

                        new City { Name = "Obreja"},

                        new City { Name = "Ticvaniu Mare"},

                        new City { Name = "Doclin"},

                        new City { Name = "Copăcele"},

                        new City { Name = "Şopotu Nou"},

                        new City { Name = "Berliste"},

                        new City { Name = "Glimboca"},

                        new City { Name = "Bauţar"},

                        new City { Name = "Ezeris"},

                        new City { Name = "Buchin"},

                        new City { Name = "Dognecea"},

                        new City { Name = "Brebu Nou"},

                        new City { Name = "Băuţar"},

                        new City { Name = "Cornea"},

                        //new City { Name = "ruia"},

                        new City { Name = "Terva"},

                    }
                },
 
                new County
                {
                    Name = "Cluj",
                    ShortName = "CJ",
                    Cities=new List<City>
                    {
                        new City { Name = "Aghiresu"},

                        new City { Name = "Baciu"},
                        
                        new City { Name = "Baisoara"},
                        
                        new City { Name = "Bonţida"},
                        
                        new City { Name = "Buza"},
                        
                        new City { Name = "Călăraşi"},
                        
                        new City { Name = "Câmpia Turzii"},
                        
                        new City { Name = "Catina"},
                        
                        new City { Name = "Ceanu Mare"},
                        
                        new City { Name = "Chiuieşti"},
                        
                        new City { Name = "Cluj Napoca"},
                        
                        new City { Name = "Cojocna"},
                        
                        new City { Name = "Dabaca"},
                        
                        new City { Name = "Dej"},
                        
                        new City { Name = "Fizeşul Gherlii"},
                        
                        new City { Name = "Floresti"},
                        
                        new City { Name = "Gherla"},
                        
                        new City { Name = "Gilau"},
                        
                        new City { Name = "Huedin"},
                        
                        new City { Name = "Iara"},
                        
                        new City { Name = "Iclod"},
                        
                        new City { Name = "Jucu"},
                        
                        new City { Name = "Maguri Racatau"},
                        
                        new City { Name = "Mintiu Gherlii"},
                        
                        new City { Name = "Negreni"},
                        
                        new City { Name = "Pălatca"},
                        
                        new City { Name = "Petrestii de  Jos"},
                        
                        new City { Name = "Săvădisla"},
                        
                        new City { Name = "Ştefăneşti"},
                        
                        new City { Name = "Suatu"},
                        
                        new City { Name = "Tulceaţi"},
                        
                        new City { Name = "Turda"},
                        
                        new City { Name = "Unguras"},
                        
                        new City { Name = "Vultureni"},

                        new City { Name = "Borşa"},

                        new City { Name = "Mera"},

                        new City { Name = "Chinteni"},

                        new City { Name = "Deușu"},

                        new City { Name = "Bobâlna"},

                        new City { Name = "Apahida"},

                        new City { Name = "Cîţcău"},

                        new City { Name = "Mociu"},

                        new City { Name = "Poieni"},

                        new City { Name = "Geaca"},

                    }
                },
 
                new County
                {
                    Name = "Constanţa",
                    ShortName = "CT",
                    Cities=new List<City>
                    {
                        new City { Name = "2 Mai"},

                        new City { Name = "23 August"},
                        
                        new City { Name = "Agigea"},
                        
                        new City { Name = "Băneasa"},
                        
                        new City { Name = "Basarabi"},
                        
                        new City { Name = "Cap Aurora"},
                        
                        new City { Name = "Castelu"},
                        
                        new City { Name = "Cernavodă"},
                        
                        new City { Name = "Constanţa"},
                        
                        new City { Name = "Costineşti"},
                        
                        new City { Name = "Cumpăna"},
                        
                        new City { Name = "Dobromir"},
                        
                        new City { Name = "Dumbrăveni"},
                        
                        new City { Name = "Eforie Nord"},
                        
                        new City { Name = "Gîrliciu"},
                        
                        new City { Name = "Hârşova"},
                        
                        new City { Name = "Jupiter"},
                        
                        new City { Name = "Limanu"},
                        
                        new City { Name = "Mamaia"},
                        
                        new City { Name = "Mangalia"},
                        
                        new City { Name = "Medgidia"},
                        
                        new City { Name = "Mihail Kogălniceanu"},
                        
                        new City { Name = "Mircea Vodă"},
                        
                        new City { Name = "Năvodari"},
                        
                        new City { Name = "Negru Voda"},
                        
                        new City { Name = "Negru Vodă"},
                        
                        new City { Name = "Neptun"},
                        
                        new City { Name = "Olimp"},
                        
                        new City { Name = "Ovidiu"},
                        
                        new City { Name = "Palazu Mare"},
                        
                        new City { Name = "Pantelimon"},
                        
                        new City { Name = "Plopeni"},
                        
                        new City { Name = "Poarta Albă"},
                        
                        new City { Name = "PP Tortomanu"},
                        
                        new City { Name = "Săcele"},
                        
                        new City { Name = "Saturn"},
                        
                        new City { Name = "Techirghiol"},
                        
                        new City { Name = "Tortomanu"},
                        
                        new City { Name = "Tuzla"},
                        
                        new City { Name = "Valu lui Traian"},
                        
                        new City { Name = "Venus"},

                        new City { Name = "Chirnogeni"},

                        new City { Name = "Cobadin"},

                        new City { Name = "Cuza Vodă"},

                        new City { Name = "Lumina"},

                        new City { Name = "Murfatlar"},

                        new City { Name = "Săcel"},

                        new City { Name = "Topraisar"},

                        new City { Name = "Tichileşti"},

                        new City { Name = "Eforie Sud"},

                        new City { Name = "Mereni"},

                        new City { Name = "Amzacea"},

                        new City { Name = "Adamclisi"},

                        new City { Name = "Ostrov"},

                        new City { Name = "Lazu"},

                        new City { Name = "Corbu"},

                        new City { Name = "Dulceşti"},

                        new City { Name = "Mihai Viteazu"},

                        new City { Name = "Dunăreni"},

                        new City { Name = "Lipniţa"},

                        new City { Name = "Deleni"},

                        new City { Name = "Peştera"},

                        new City { Name = "Oltina"},

                        new City { Name = "Crucea"},

                        new City { Name = "Ghindăreşti"},

                        new City { Name = "Horia"},

                        new City { Name = "Nicolae Bălcescu"},

                    }
                },
 
                new County
                {
                    Name = "Covasna",
                    ShortName = "CV",
                    Cities=new List<City>
                    {
                        new City { Name = "Baraolt"},
                        
                        new City { Name = "Sfântu Gheorghe"},

                        new City { Name = "Întorsura Buzăului"},

                        new City { Name = "Târgu Secuiesc"},

                        new City { Name = "Vîlcele"},

                        new City { Name = "Zăbala"},

                        new City { Name = "Covasna"},

                        new City { Name = "Ozun"},

                        new City { Name = "Hăghig"},

                        new City { Name = "Turia"},

                        new City { Name = "Icafalău"},

                        new City { Name = "Borosneu Mare"},

                        new City { Name = "Valea Crişului"},

                        new City { Name = "Poian"},

                        new City { Name = "Arcuş"},

                        new City { Name = "Cernat"},

                    }
                },

                new County
                {
                    Name = "Călăraşi",
                    ShortName = "CL",
                    Cities=new List<City>
                    {
                        new City { Name = "Budeşti"},

                        new City { Name = "Calarasi"},
                        
                        new City { Name = "Lehliu"},
                        
                        new City { Name = "Lehliu Gară"},
                        
                        new City { Name = "Olteniţa"},

                        new City { Name = "Bărăganu"},

                        new City { Name = "Borcea"},

                        new City { Name = "Cuza Vodă"},

                        new City { Name = "Dârvari"},

                        new City { Name = "Dragalina"},

                        new City { Name = "Mitreni"},

                        new City { Name = "Modelu"},

                        new City { Name = "Nana"},

                        new City { Name = "Perişoru"},

                        new City { Name = "Plătăreşti"},

                        new City { Name = "Roseţi"},

                        new City { Name = "Alexandru Odobescu"},

                        new City { Name = "Dor Mărunt"},

                        new City { Name = "Fundulea"},

                        new City { Name = "Curcani"},

                        new City { Name = "Chirnogi"},

                        new City { Name = "Dorobanţu"},

                        new City { Name = "Jegălia"},

                        new City { Name = "Luica"},

                        new City { Name = "Spanţov"},

                        new City { Name = "Belciugatele"},

                        new City { Name = "Valea Arvei"},

                        new City { Name = "Vasilaţi"},

                        new City { Name = "Stefan cel Mare"},

                        new City { Name = "Săruleşti"},

                        new City { Name = "Frumuşani"},

                        new City { Name = "Gălbinaşi"},

                        new City { Name = "Fundeni"},

                        new City { Name = "Sohatu"},

                        new City { Name = "Tămădu Mare"},

                        new City { Name = "Rasa"},

                        new City { Name = "Ulmeni"},

                        new City { Name = "Gurbăneşti"},

                        new City { Name = "Ciocăneşti"},

                        new City { Name = "Vâlcelele"},

                        new City { Name = "Ulmu"},

                        new City { Name = "Mînăstirea"},

                        new City { Name = "Siliștea"},

                    }
                },
 
                new County
                {
                    Name = "Dolj",
                    ShortName = "DJ",
                    Cities=new List<City>
                    {
                        new City { Name = "Băileşti"},

                        new City { Name = "Bârca"},
                        
                        new City { Name = "Bechet"},
                        
                        new City { Name = "Bratovoieşti"},
                        
                        new City { Name = "Breasta"},
                        
                        new City { Name = "Calafat"},
                        
                        new City { Name = "Cerăt"},
                        
                        new City { Name = "Cioroiaşi"},
                        
                        new City { Name = "Coţofenii din Faţă"},
                        
                        new City { Name = "Craiova"},
                        
                        new City { Name = "Filiaşi"},
                        
                        new City { Name = "Leu"},
                        
                        new City { Name = "Pieleşti"},
                        
                        new City { Name = "Podari"},
                        
                        new City { Name = "Rast"},
                        
                        new City { Name = "Verbiţa"},

                        new City { Name = "Segarcea"},

                        new City { Name = "Almăj"},

                        new City { Name = "Amărăştii de Jos"},

                        new City { Name = "Amărăştii de Sus"},

                        new City { Name = "Apele Vii"},

                        new City { Name = "Argetoaia"},

                        new City { Name = "Bistreţ"},

                        new City { Name = "Botoşeşti Paia"},

                        new City { Name = "Brabova"},

                        new City { Name = "Brădeşti"},

                        new City { Name = "Braloştiţa"},

                        new City { Name = "Bulzeşti"},

                        new City { Name = "Calopăr"},

                        new City { Name = "Caraula"},

                        new City { Name = "Cârcea"},

                        new City { Name = "Cârna"},

                        new City { Name = "Carpen"},

                        new City { Name = "Castranova"},

                        new City { Name = "Celaru"},

                        new City { Name = "Cernăteşti"},

                        new City { Name = "Cetate"},

                        new City { Name = "Ciupercenii Noi"},

                        new City { Name = "Coşoveni"},

                        new City { Name = "Coţofenii din Dos"},

                        new City { Name = "Dăbuleni"},

                        new City { Name = "Daneţi"},

                        new City { Name = "Desa"},

                        new City { Name = "Dioşti"},

                        new City { Name = "Dobreşti"},

                        new City { Name = "Drăteşti"},

                        new City { Name = "Drănic"},

                        new City { Name = "Fărcaş"},

                        new City { Name = "Galicea Mare"},

                        new City { Name = "Galiciuica"},

                        new City { Name = "Gângiova"},

                        new City { Name = "Gherceşti"},

                        new City { Name = "Ghidici"},

                        new City { Name = "Ghindeni"},

                        new City { Name = "Gighera"},

                        new City { Name = "Giubega"},

                        new City { Name = "Giurgiţa"},

                        new City { Name = "Zimnicea"},

                        //new City { Name = "ieşti"},

                        new City { Name = "Greceşti"},

                        new City { Name = "Întorsura"},

                        new City { Name = "Işalniţa"},

                        new City { Name = "Leamna"},

                        new City { Name = "Lipovu"},

                        new City { Name = "Măceşu de Jos"},

                        new City { Name = "Măceşu de Sus"},

                        new City { Name = "Malu Mare"},

                        new City { Name = "Mârşani"},

                        new City { Name = "Melineşti"},

                        new City { Name = "Mischii"},

                        new City { Name = "Moţăţei"},

                        new City { Name = "Murgaşi"},

                        new City { Name = "Nei"},

                        new City { Name = "Orodel"},

                        new City { Name = "Perişor"},

                        new City { Name = "Piscu Vechi"},

                        new City { Name = "Pleniţa"},

                        new City { Name = "Pleşoi"},

                        new City { Name = "Poiana Mare"},

                        new City { Name = "Predeşti"},

                        new City { Name = "Radovan"},

                        new City { Name = "Robăneşti"},

                        new City { Name = "Rojişte"},

                        new City { Name = "Sălcuţa"},

                        new City { Name = "Scăeşti"},

                        new City { Name = "Seaca de Câmp"},

                        new City { Name = "Seaca de Pădure"},

                        new City { Name = "Secu"},

                        new City { Name = "Silistea Crucii"},

                        new City { Name = "Şimnicu de Sus"},

                        new City { Name = "Sopot"},

                        new City { Name = "Tălpaş"},

                        new City { Name = "Teasc"},

                        new City { Name = "Terpeziţa"},

                        new City { Name = "Ţuglui"},

                        new City { Name = "Urzicuţa"},

                        new City { Name = "Valea Stanciului"},

                        new City { Name = "Vela"},

                        new City { Name = "Vîrtop"},

                        new City { Name = "Vîrvoru de Jos"},

                        new City { Name = "Izvoare"},

                        new City { Name = "Călăraşi"},

                        new City { Name = "Bucovăţ"},

                        new City { Name = "Bîrca"},

                        new City { Name = "Gingiova"},

                        new City { Name = "Ostroveni"},

                    }
                },

                new County
                {
                    Name = "Dâmboviţa",
                    ShortName = "DB",
                    Cities=new List<City>
                    {
                        new City { Name = "Aninoasa Db"},

                        new City { Name = "Bilciureşti"},
                        
                        new City { Name = "Braniştea"},
                        
                        new City { Name = "Conţeşti"},
                        
                        new City { Name = "Crangurile"},
                        
                        new City { Name = "Crevedia"},
                        
                        new City { Name = "Dramireşti"},
                        
                        new City { Name = "Fieni"},
                        
                        new City { Name = "Găeşti"},
                        
                        new City { Name = "Gura Foii"},
                        
                        new City { Name = "Gura Ocniţei"},
                        
                        new City { Name = "I.L.Caragiale"},
                        
                        new City { Name = "Malu cu Flori"},
                        
                        new City { Name = "Moşani"},
                        
                        new City { Name = "Moreni"},
                        
                        new City { Name = "Moroeni"},
                        
                        new City { Name = "Nucet"},
                        
                        new City { Name = "Ocniţa"},
                        
                        new City { Name = "Odobeşti"},
                        
                        new City { Name = "Petreşti"},
                        
                        new City { Name = "Pietroşiţa"},
                        
                        new City { Name = "Pucioasa"},
                        
                        new City { Name = "Răcari"},
                        
                        new City { Name = "Răzvad"},
                        
                        new City { Name = "Şelaru"},
                        
                        new City { Name = "Târvişte"},
                        
                        new City { Name = "Tătărani"},
                        
                        new City { Name = "Titu"},
                        
                        new City { Name = "Vladeni"},
                        
                        new City { Name = "Voineşti"},

                        new City { Name = "Dobreşti"},

                        new City { Name = "Valea Voievozilor"},

                        new City { Name = "Valea Lunga"},

                        new City { Name = "Comişani"},

                        new City { Name = "Tărtăşeşti"},

                        new City { Name = "Valea Mare"},

                        new City { Name = "Butimanu"},

                        new City { Name = "Bezdead"},

                        new City { Name = "Vulcana Băi"},

                        new City { Name = "Şotânga"},

                        new City { Name = "Ciocanesti"},

                        new City { Name = "Răzvadu de Jos"},

                        new City { Name = "Vârfuri"},

                        new City { Name = "Tărtărești"},

                        new City { Name = "Bucşani"},

                        new City { Name = "Dradăneşti"},

                        new City { Name = "Viișoara"},

                        new City { Name = "Văcăreşti"},

                        new City { Name = "Cojasca"},

                        new City { Name = "Brezoaele"},

                        new City { Name = "Ludeşti"},

                        new City { Name = "Vulcana Pandele"},

                        new City { Name = "Sălcioara"},

                        new City { Name = "Lungulețu"},

                        new City { Name = "Lucieni"},

                        new City { Name = "Potlogi"},

                        new City { Name = "Vişina"},

                    }
                },
 
                new County
                {
                    Name = "Galaţi",
                    ShortName = "GL",
                    Cities=new List<City>
                    {
                        new City { Name = "Bălăbăneşti"},

                        new City { Name = "Băneasa"},
                        
                        new City { Name = "Barcea"},
                        
                        new City { Name = "Bereşti"},
                        
                        new City { Name = "Bereşti - Meria"},
                        
                        new City { Name = "Brăhăşeşti"},
                        
                        new City { Name = "Buciumeni"},
                        
                        new City { Name = "Cavadineşti"},
                        
                        new City { Name = "Cerţeşti"},
                        
                        new City { Name = "Corbii Mari"},
                        
                        new City { Name = "Corni"},
                        
                        new City { Name = "Corod"},
                        
                        new City { Name = "Cudalbi"},
                        
                        new City { Name = "Drăgăneşti"},
                        
                        new City { Name = "Draguseni"},
                        
                        new City { Name = "Fârţăneşti"},
                        
                        new City { Name = "Folteşti"},
                        
                        new City { Name = "Frumuşiţa"},
                        
                        new City { Name = "Galaţi"},
                        
                        new City { Name = "Iveşti"},
                        
                        new City { Name = "Jorăşti"},
                        
                        new City { Name = "Lieşti"},
                        
                        new City { Name = "Movileni"},
                        
                        new City { Name = "Munteni"},
                        
                        new City { Name = "Nămoloasa"},
                        
                        new City { Name = "Nicoreşti"},
                        
                        new City { Name = "Oancea"},
                        
                        new City { Name = "Pechea"},
                        
                        new City { Name = "Piscu"},
                        
                        new City { Name = "Poiana"},
                        
                        new City { Name = "Rediu"},
                        
                        new City { Name = "Scânteieşti"},
                        
                        new City { Name = "Schela"},
                        
                        new City { Name = "Slobozia Conachi"},
                        
                        new City { Name = "Smulţi"},
                        
                        new City { Name = "Suceveni"},
                        
                        new City { Name = "Târgu Bujor"},
                        
                        new City { Name = "Tecuci"},
                        
                        new City { Name = "Ţepu"},
                        
                        new City { Name = "Tuluceşti"},
                        
                        new City { Name = "Umbrăreşti"},
                        
                        new City { Name = "Valea Mărului"},
                        
                        new City { Name = "Vânători"},
                        
                        new City { Name = "Vârlezi"},

                        new City { Name = "Targu Bujor"},

                        new City { Name = "Smârdan (GL},"},

                        new City { Name = "Măstăcani"},

                        new City { Name = "Băleni"},

                        new City { Name = "Costache Negri"},

                        new City { Name = "Vladesti"},

                        new City { Name = "Griviţa"},

                        new City { Name = "Matca"},

                        new City { Name = "Tudor Vladimirescu"},

                        new City { Name = "Fundeni"},

                        new City { Name = "Zăvoaia"},

                        new City { Name = "Sfinții Împărați"},

                        new City { Name = "Bălășești"},

                        //new City { Name = "?endreni"},

                    }
                },
 
                new County
                {
                    Name = "Giurgiu",
                    ShortName = "GR",
                    Cities=new List<City>
                    {
                        new City { Name = "Bolintin Vale"},

                        new City { Name = "Ghimpaţi"},
                        
                        new City { Name = "Giurgiu"},

                        new City { Name = "Grădiştea"},

                        new City { Name = "Mihăileşti"},

                        new City { Name = "Prundu"},

                        new City { Name = "Buturugeni"},

                        new City { Name = "Malu"},

                        new City { Name = "Vărăşti"},

                        new City { Name = "Găujani"},

                    }
                },
 
                new County
                {
                    Name = "Gorj",
                    ShortName = "GJ",
                    Cities=new List<City>
                    {
                        new City { Name = "Aninoasa Gj"},

                        new City { Name = "Baia de Fier"},
                        
                        new City { Name = "Bălăneşti"},
                        
                        new City { Name = "Băleşti"},
                        
                        new City { Name = "Barbatesti"},
                        
                        new City { Name = "Bengeşti - Ciocadia"},
                        
                        new City { Name = "Berleşti"},
                        
                        new City { Name = "Borăscu"},
                        
                        new City { Name = "Bumbeşti"},
                        
                        new City { Name = "Bumbeşti Jiu"},
                        
                        new City { Name = "Bustuchin"},
                        
                        new City { Name = "Capreni"},
                        
                        new City { Name = "Ciuperceni"},
                        
                        new City { Name = "Cruşeţ"},
                        
                        new City { Name = "Danciulesti"},
                        
                        new City { Name = "Farcasesti"},
                        
                        new City { Name = "Dineşti"},
                        
                        new City { Name = "Ioneşti"},
                        
                        new City { Name = "Leleşti"},
                        
                        new City { Name = "Logreşti"},
                        
                        new City { Name = "Mătăsari"},
                        
                        new City { Name = "Motru"},
                        
                        new City { Name = "Novaci"},
                        
                        new City { Name = "Peştişani"},
                        
                        new City { Name = "Plopşoru"},
                        
                        new City { Name = "Polovragi"},
                        
                        new City { Name = "Priria"},
                        
                        new City { Name = "Roşia de Amaradia"},
                        
                        new City { Name = "Rovinari"},
                        
                        new City { Name = "Săuleşti"},
                        
                        new City { Name = "Scoarţa"},
                        
                        new City { Name = "Stejari"},
                        
                        new City { Name = "Stoina"},
                        
                        new City { Name = "Târgu Jiu"},
                        
                        new City { Name = "Teleşti"},
                        
                        new City { Name = "Tg. Cărbuneşti"},
                        
                        new City { Name = "Ţicleni"},
                        
                        new City { Name = "Tismana"},
                        
                        new City { Name = "Turburea"},
                        
                        new City { Name = "Turceni"},

                        new City { Name = "Bârseşti"},

                        new City { Name = "Hurezani"},

                        new City { Name = "Târgu Cărbuneşti"},

                        new City { Name = "Slivileşti"},

                        new City { Name = "Arcani"},

                        new City { Name = "Văgiuleşti"},

                        new City { Name = "Muşeteşti"},

                        new City { Name = "Lainici"},

                        new City { Name = "Fărcășești"},

                        new City { Name = "Runcu"},

                        new City { Name = "Budieni"},

                        new City { Name = "Albeni"},

                        new City { Name = "Bîlteni"},

                        new City { Name = "Alimpeşti"},

                        new City { Name = "Urdari"},

                        new City { Name = "Turcineşti"},

                        new City { Name = "Padeş"},

                    }
                },

                new County
                {
                    Name = "Harghita",
                    ShortName = "HR",
                    Cities=new List<City>
                    {
                        new City { Name = "Avrămeşti"},

                        new City { Name = "Bălan"},
                        
                        new City { Name = "Bilbor"},
                        
                        new City { Name = "Borsec"},
                        
                        new City { Name = "Ciumani"},
                        
                        new City { Name = "Corund"},
                        
                        new City { Name = "Cristuru Secuiesc"},
                        
                        new City { Name = "Dealu"},
                        
                        new City { Name = "Dîrjiu"},
                        
                        new City { Name = "Ditrău"},
                        
                        new City { Name = "Gălăutaş"},
                        
                        new City { Name = "Gheorgheni"},
                        
                        new City { Name = "Gheorghieni"},
                        
                        new City { Name = "Joseni"},
                        
                        new City { Name = "Leliceni"},
                        
                        new City { Name = "Lunca de Jos"},
                        
                        new City { Name = "Lunca de Sus"},
                        
                        new City { Name = "Mădăraş (HR},"},
                        
                        new City { Name = "Mărtiniş"},
                        
                        new City { Name = "Miercurea Ciuc"},
                        
                        new City { Name = "Odorheiu Sec."},
                        
                        new City { Name = "Porumbeni"},
                        
                        new City { Name = "Praid"},
                        
                        new City { Name = "Racu"},
                        
                        new City { Name = "Remetea"},
                        
                        new City { Name = "Sănsimion"},
                        
                        new City { Name = "Subcetate"},
                        
                        new City { Name = "Suseni"},
                        
                        new City { Name = "Tomeşti"},
                        
                        new City { Name = "Topliţa"},
                        
                        new City { Name = "Tulgheş"},
                        
                        new City { Name = "Vlăhiţa"},
                        
                        new City { Name = "Voşlăbeni"},

                        new City { Name = "Brădeşti"},

                        new City { Name = "Izvoare"},

                        new City { Name = "Odorheiu Secuiesc"},

                        new City { Name = "Ocland"},

                        new City { Name = "Zetea"},

                        new City { Name = "Lueta"},

                        new City { Name = "Vărșag"},

                        new City { Name = "Sîndominic"},

                        new City { Name = "Mujna"},

                        new City { Name = "Corbu"},

                        new City { Name = "Tușnad Băi"},

                    }
                },

                new County
                {
                    Name = "Hunedoara",
                    ShortName = "HD",
                    Cities=new List<City>
                    {
                        new City { Name = "Aninoasa Hd"},

                        new City { Name = "Băcia"},
                        
                        new City { Name = "Băile Tuşnad"},
                        
                        new City { Name = "Băiţa"},
                        
                        new City { Name = "Baru Mare"},
                        
                        new City { Name = "Boşorod"},
                        
                        new City { Name = "Brad"},
                        
                        new City { Name = "Brănişca"},
                        
                        new City { Name = "Bretea Romana"},
                        
                        new City { Name = "Burjuc"},
                        
                        new City { Name = "Călan"},
                        
                        new City { Name = "Cerbal"},
                        
                        new City { Name = "Certeju de Sus"},
                        
                        new City { Name = "Deva"},
                        
                        new City { Name = "Dobra"},
                        
                        new City { Name = "Geoagiu"},
                        
                        new City { Name = "Ghelari"},
                        
                        new City { Name = "Gurasada"},
                        
                        new City { Name = "Hărău"},
                        
                        new City { Name = "Haţeg"},
                        
                        new City { Name = "Hunedoara"},
                        
                        new City { Name = "Ilia"},
                        
                        new City { Name = "Lăpugiu de Jos"},
                        
                        new City { Name = "Lupeni"},
                        
                        new City { Name = "Orăştie"},
                        
                        new City { Name = "Orăştioara de Sus"},
                        
                        new City { Name = "P P Cerbal"},
                        
                        new City { Name = "Petrila"},
                        
                        new City { Name = "Petroşani"},
                        
                        new City { Name = "Post Poliţie Cerbal"},
                        
                        new City { Name = "Pui"},
                        
                        new City { Name = "Rapoltu Mare"},
                        
                        new City { Name = "Râu de Mori"},
                        
                        new City { Name = "Romos"},
                        
                        new City { Name = "Simeria"},
                        
                        new City { Name = "Sîntămăria Orlea"},
                        
                        new City { Name = "Şoimuş"},
                        
                        new City { Name = "Teliucu Infer."},
                        
                        new City { Name = "Uricani"},
                        
                        new City { Name = "Vulcan (HD},"},

                        new City { Name = "Bârcea Mare"},

                        new City { Name = "Brâznic"},

                        new City { Name = "Bulzeşti"},

                        new City { Name = "Căzăneşti"},

                        new City { Name = "Teliucu Inferior"},

                        new City { Name = "Zam"},

                    }
                },

                new County
                {
                    Name = "Ialomiţa",
                    ShortName = "IL",
                    Cities=new List<City>
                    {
                        new City { Name = "Amara"},

                        new City { Name = "Budeşti"},
                        
                        new City { Name = "Feteşti"},
                        
                        new City { Name = "Slobozia"},
                        
                        new City { Name = "Ţăndărei"},
                        
                        new City { Name = "Urziceni"},
                        
                        new City { Name = "Vama Veche"},

                        new City { Name = "Broşteni"},

                        new City { Name = "Bucu"},

                        new City { Name = "Căzăneşti"},

                        new City { Name = "Ciocârlia"},

                        new City { Name = "Giurgeni"},

                        new City { Name = "Ion Roată"},

                        new City { Name = "Mihail Kogălniceanu"},

                        new City { Name = "Moviliţa"},

                        new City { Name = "Sfântu Gheorghe"},

                        new City { Name = "Slobozia Nouă"},

                        new City { Name = "Drajna Nouă"},

                        new City { Name = "Gheorghe Lazăr"},

                        new City { Name = "Jilavele"},

                        new City { Name = "Ciulniţa"},

                        new City { Name = "Fierbinţi-Târg"},

                        new City { Name = "Coşereni"},

                        new City { Name = "Andrăşeşti"},

                        new City { Name = "Mărculeşti"},

                        new City { Name = "Axintele"},

                        new City { Name = "Săveni"},

                        new City { Name = "Cegani"},

                        new City { Name = "Ograda"},

                        new City { Name = "Salcioara"},

                        new City { Name = "Borăneşti"},

                        new City { Name = "Cocora"},

                        new City { Name = "Sineşti"},

                        new City { Name = "Dridu"},

                        new City { Name = "Făcăeni"},

                        new City { Name = "Grindu"},

                        new City { Name = "Borduşani"},

                        new City { Name = "Bărbuleşti"},

                        new City { Name = "Platoneşti"},

                        new City { Name = "Griviţa"},

                        new City { Name = "Valea Ciorii"},

                        new City { Name = "Munteni Buzău"},

                        new City { Name = "Traian"},

                        new City { Name = "Răduleşti"},

                        new City { Name = "Ciochina"},

                        new City { Name = "Albeşti"},

                        new City { Name = "Gheorghe Doja"},

                        new City { Name = "Reviga"},

                        new City { Name = "Vlădeni"},

                        new City { Name = "Cosâmbeşti"},

                        new City { Name = "Manasia"},

                        new City { Name = "Scânteia"},

                        new City { Name = "Armășești"},

                        new City { Name = "Perieţi"},

                    }
                },

                new County
                {
                    Name = "Iaşi",
                    ShortName = "IS",
                    Cities=new List<City>
                    {
                        new City { Name = "Costeşti"},

                        new City { Name = "Cotnari"},
                        
                        new City { Name = "Hârlău"},
                        
                        new City { Name = "Iaşi"},
                        
                        new City { Name = "Paşcani"},
                        
                        new City { Name = "Podu Iloaiei"},
                        
                        new City { Name = "Răducăneni"},
                        
                        new City { Name = "Târgu Frumos"},
                        
                        new City { Name = "Voineşti"},

                        new City { Name = "Borşa"},

                        new City { Name = "Grozeşti"},

                        new City { Name = "Popeşti"},

                        new City { Name = "Sinesti"},

                        new City { Name = "Bârlesti"},

                        new City { Name = "Iepureni"},

                        new City { Name = "Miroslava"},

                        new City { Name = "Movileni"},

                        new City { Name = "Valea Seacă"},

                        new City { Name = "Bărcăneşti"},

                        new City { Name = "Mironeasa"},

                        new City { Name = "Roşcani"},

                        new City { Name = "Balș"},

                        new City { Name = "Ion Neculce"},

                        //new City { Name = "rban"},

                        new City { Name = "Focuri"},

                        new City { Name = "Ruginoasa"},

                        new City { Name = "Ciurea"},

                        new City { Name = "Leţcani"},

                        new City { Name = "Bălţaţi"},

                        new City { Name = "Dagiţa"},

                        new City { Name = "Erbiceni"},

                        new City { Name = "Coarnele Caprei"},

                        new City { Name = "Comarna"},

                        new City { Name = "Ţibăneşti"},

                        new City { Name = "Scheia"},

                        new City { Name = "Ipatele"},

                        new City { Name = "Lespezi"},

                        new City { Name = "Țigănași"},

                        new City { Name = "Bârnova"},

                        new City { Name = "Lungani"},

                        new City { Name = "Oţeleni"},

                        new City { Name = "Mirosloveşti"},

                        new City { Name = "Mosna"},

                        new City { Name = "Cepleniţa"},

                        new City { Name = "Stolniceni Prăjescu"},

                        new City { Name = "Valea Lupului"},

                        new City { Name = "Ciohorăni"},

                        new City { Name = "Tomești"},

                        new City { Name = "Butea"},

                        new City { Name = "Brăești"},

                        new City { Name = "Tanşa"},

                        new City { Name = "Mirceşti"},

                        new City { Name = "Părcovoi"},

                        new City { Name = "Scânteia"},

                        new City { Name = "Schitu Duca"},

                        new City { Name = "Prisăcani"},

                    }
                },

                new County
                {
                    Name = "Ilfov",
                    ShortName = "IF",
                    Cities=new List<City>
                    {
                        new City { Name = "Buftea"},

                        new City { Name = "Cornetu"},
                        
                        new City { Name = "Ilfov"},
                        
                        new City { Name = "Moşoaia"},
                        
                        new City { Name = "Otopeni"},
                        
                        new City { Name = "Snav"},
                        
                        new City { Name = "Urziceni"},

                        new City { Name = "Afumaţi"},

                        new City { Name = "Bragadiru"},

                        new City { Name = "Chitila"},

                        new City { Name = "Clinceni"},

                        new City { Name = "Jilava"},

                        new City { Name = "Măgurele"},

                        new City { Name = "Pipera"},

                        new City { Name = "Tunari"},

                        new City { Name = "Vidra"},

                        new City { Name = "Voluntari"},

                        new City { Name = "Chiajna"},

                        new City { Name = "Pantelimon"},

                        new City { Name = "Domneşti"},

                        new City { Name = "Periş"},

                        new City { Name = "Ciorogârla"},

                        new City { Name = "Dramireşti Vale"},

                        new City { Name = "Găneasa"},

                        new City { Name = "Popeşti Leordeni"},

                        new City { Name = "Bălăceanca"},

                        new City { Name = "Cernica"},

                        new City { Name = "Brăneşti"},

                        new City { Name = "Dobroeşti"},

                        new City { Name = "Ciolpani"},

                        new City { Name = "Baloteşti"},

                        new City { Name = "Dărăști"},

                        new City { Name = "Dramireşti Deal"},

                        new City { Name = "Glina"},

                        new City { Name = "Dascălu"},

                    }
                },

                new County
                {
                    Name = "Maramureş",
                    ShortName = "MM",
                    Cities=new List<City>
                    {
                        new City { Name = "Baia Mare"},

                        new City { Name = "Baia Sprie"},
                        
                        new City { Name = "Borşa"},
                        
                        new City { Name = "Budeşti"},
                        
                        new City { Name = "Cavnic"},
                        
                        new City { Name = "Dramireşti"},
                        
                        new City { Name = "Seini"},
                        
                        new City { Name = "Sighetu Marmaţiei"},
                        
                        new City { Name = "Şomcuta Mare"},
                        
                        new City { Name = "Târgu Lăpuş"},
                        
                        new City { Name = "Vişeu de Sus"},

                        new City { Name = "Bogdan Vodă"},

                        new City { Name = "Cicârlău"},

                        new City { Name = "Groşi"},

                        new City { Name = "Leordina"},

                        new City { Name = "Ocna Şugatag"},

                        new City { Name = "Şişeşti"},

                        new City { Name = "Suciu de Sus"},

                        new City { Name = "Tăuţii Măgherăuş"},

                        new City { Name = "Gheorghe Pop de Băseşti"},

                        new City { Name = "Colţău"},

                        new City { Name = "Poienile de Sub Munte"},

                        new City { Name = "Recea"},

                        new City { Name = "Vadu Izei"},

                        new City { Name = "Sarasău"},

                        new City { Name = "Ruscova"},

                    }
                },

                new County
                {
                    Name = "Mehedinţi",
                    ShortName = "MH",
                    Cities=new List<City>
                    {
                        new City { Name = "Baia de Aramă"},

                        new City { Name = "Ceahlău"},
                        
                        new City { Name = "Drobeta Turnu Severin"},
                        
                        new City { Name = "Orşova"},
                        
                        new City { Name = "Strehaia"},
                        
                        new City { Name = "Vânju Mare"},

                        new City { Name = "Bâcleş"},

                        new City { Name = "Bala"},

                        new City { Name = "Bălăciţa"},

                        new City { Name = "Balta"},

                        new City { Name = "Bâlvăneşti"},

                        new City { Name = "Brezniţa-Motru"},

                        new City { Name = "Brezniţa-Ocol"},

                        new City { Name = "Burila Mare"},

                        new City { Name = "Butoieşti"},

                        new City { Name = "Cireşu"},

                        new City { Name = "Corcova"},

                        new City { Name = "Corlăţel"},

                        new City { Name = "Cujmir"},

                        new City { Name = "Devesel"},

                        new City { Name = "Dubova"},

                        new City { Name = "Eşelniţa"},

                        new City { Name = "Gârla Mare"},

                        //new City { Name = "deanu"},

                       // new City { Name = "şu"},

                        new City { Name = "Gruia"},

                        new City { Name = "Hinova"},

                        new City { Name = "Izvoru Bârzii"},

                        new City { Name = "Jiana"},

                        new City { Name = "Malovăţ"},

                        new City { Name = "Obârşia de Câmp"},

                        new City { Name = "Obârşia-Cloşani"},

                        new City { Name = "Oprişor"},

                        new City { Name = "Pădina Mare"},

                        new City { Name = "Pătulele"},

                        new City { Name = "Podeni"},

                        new City { Name = "Ponoarele"},

                        new City { Name = "Poroina Mare"},

                        new City { Name = "Pristol"},

                        new City { Name = "Prunişor"},

                        new City { Name = "Punghina"},

                        new City { Name = "Rova"},

                        new City { Name = "Şovarna"},

                        new City { Name = "Stângăceaua"},

                        new City { Name = "Şviniţa"},

                        new City { Name = "Tâmna"},

                        new City { Name = "Vânjuleţ"},

                        new City { Name = "Vlădaia"},

                        new City { Name = "Voloiac"},

                        new City { Name = "Vrata"},

                        new City { Name = "Căzăneşti"},

                        new City { Name = "Simian"},

                        new City { Name = "Salcia"},

                    }
                },

                new County
                {
                    Name = "Mureş",
                    ShortName = "MS",
                    Cities=new List<City>
                    {
                        new City { Name = "Iernut"},

                        new City { Name = "Luduş"},

                        new City { Name = "Reghin"},

                        new City { Name = "Sângeorgiu Păd."},

                        new City { Name = "Sarmasu"},

                        new City { Name = "Sighişoara"},

                        new City { Name = "Sovata"},

                        new City { Name = "Târgu Mureş"},

                        new City { Name = "Târnăveni"},

                        new City { Name = "Corunca"},

                        new City { Name = "Mădăraş"},

                        new City { Name = "PETELEA"},

                        new City { Name = "Sângeorgiu de Pădure"},

                        new City { Name = "Miercurea Nirajului"},

                        new City { Name = "Deda"},

                        new City { Name = "Sulpac"},

                        new City { Name = "Adămuş"},

                        new City { Name = "Albesti"},

                        new City { Name = "Apold"},

                        new City { Name = "Eremitu"},

                        new City { Name = "Găneşti"},

                        new City { Name = "Hodac"},

                        new City { Name = "Sângeorgiu de Mureş"},

                        new City { Name = "Fântânele"},

                        new City { Name = "Ernei"},

                        new City { Name = "Aluniş"},

                        new City { Name = "Ruşii Munţi"},

                        new City { Name = "Adamus"},

                        new City { Name = "Ungheni"},

                        new City { Name = "Vărgata"},

                        new City { Name = "Crăciuneşti"},

                        //new City { Name = "rneşti"},

                        new City { Name = "Făgărău"},

                    }
                },

                new County
                {
                    Name = "Neamţ",
                    ShortName = "NT",
                    Cities=new List<City>
                    {
                        new City { Name = "Bicaz"},

                        new City { Name = "Piatra Neamţ"},

                        new City { Name = "Roman"},

                        new City { Name = "Roznov"},

                        new City { Name = "Săvineşti"},

                        new City { Name = "Secuieni"},

                        new City { Name = "Târgu Neamţ"},

                        new City { Name = "Chilii"},

                        new City { Name = "Fărcaşa"},

                        new City { Name = "Groşi"},

                        new City { Name = "Izvoare"},

                        new City { Name = "Petru Voda"},

                        new City { Name = "Mărgineni"},

                        new City { Name = "Dulcesti"},

                        new City { Name = "Bălţăteşti"},

                        new City { Name = "Borleşti"},

                        new City { Name = "Doljeşti"},

                        new City { Name = "Săbăoani"},

                        new City { Name = "Războieni"},

                        new City { Name = "Brusturi"},

                        new City { Name = "Timişeşti"},

                        new City { Name = "Urecheni"},

                        new City { Name = "Români"},

                        new City { Name = "Poiana Teiului"},

                        new City { Name = "Păstrăveni"},

                        new City { Name = "Pânceşti"},

                        new City { Name = "Ghindăoani"},

                        new City { Name = "Botești"},

                        new City { Name = "Hangu"},

                        new City { Name = "Răuceşti"},

                        new City { Name = "Ţibucani"},

                        new City { Name = "Podoleni"},

                        new City { Name = "Nisiporești"},

                        new City { Name = "Ştefan cel Mare"},

                        new City { Name = "Boghicea"},

                        new City { Name = "Dumbrava Rosie"},

                        new City { Name = "Costişa"},

                        new City { Name = "Agapia"},

                        new City { Name = "Bârgăuani"},

                        new City { Name = "Petricani"},

                        new City { Name = "Bahna"},

                        new City { Name = "Gadinţi"},

                        new City { Name = "Grumăzeşti"},

                        new City { Name = "Dramireşti"},

                        new City { Name = "Văleni"},

                        new City { Name = "Dobreni"},

                        new City { Name = "Gircina"},

                        new City { Name = "Gherăești"},

                        new City { Name = "Bodeşti"},

                        new City { Name = "Dochia"},

                    }
                },

                new County
                {
                    Name = "Olt",
                    ShortName = "OT",
                    Cities=new List<City>
                    {
                        new City { Name = "Bals"},

                        new City { Name = "Caracal"},

                        new City { Name = "Corabia"},

                        new City { Name = "Drăgăneşti Olt"},

                        new City { Name = "Piatra Olt"},

                        new City { Name = "Scorniceşti"},

                        new City { Name = "Slatina"},

                        new City { Name = "Maglavit"},

                        new City { Name = "Milcov"},

                        new City { Name = "Ostroveni"},

                        new City { Name = "Teslui"},

                        new City { Name = "Brebeni"},

                        new City { Name = "Grojdibodu,"},

                        new City { Name = "Vlădila"},

                        new City { Name = "Deveselu"},

                        new City { Name = "Studina"},

                        new City { Name = "Topana"},

                        new City { Name = "Potcoava"},

                        new City { Name = "Obârşia Nouă"},

                        new City { Name = "Gura Padinii"},

                        new City { Name = "Făgeţelu"},

                        new City { Name = "Coteana"},

                        new City { Name = "Călui"},

                        new City { Name = "Slătioara"},

                        new City { Name = "Tătuleşti"},

                        new City { Name = "Pleşoiu"},

                        new City { Name = "Brincoveni"},

                        new City { Name = "Dobrun"},

                        new City { Name = "Grădinari"},

                        new City { Name = "Fălcoiu"},

                        new City { Name = "Ipotesti"},

                        new City { Name = "Dobrosloveni"},

                        new City { Name = "Nicolae Titulescu"},

                        new City { Name = "Stoenești"},

                        new City { Name = "Dobroteasa"},

                        new City { Name = "Urzica"},

                        new City { Name = "Perieti"},

                        new City { Name = "Brastavatu"},

                        new City { Name = "Dăneasa"},

                        new City { Name = "Voineasa"},

                        new City { Name = "Sărbii Măgura"},

                        new City { Name = "Izvoarele"},

                        new City { Name = "Cârlogani"},

                        new City { Name = "Şerbăneşti"},

                        new City { Name = "Osica de Sus"},

                        new City { Name = "Morunglav"},

                        new City { Name = "Osica de Jos"},

                        new City { Name = "Radomireşti"},

                        new City { Name = "Ghimpeţeni"},

                        new City { Name = "Gostavatu"},

                        new City { Name = "Vâlcele"},

                        new City { Name = "Băbiciu"},

                    }
                },
 
                new County
                {
                    Name = "Prahova",
                    ShortName = "PH",
                    Cities=new List<City>
                    {
                        new City { Name = "Azuga"},

                        new City { Name = "Băicoi"},

                        new City { Name = "Băneşti"},

                        new City { Name = "Bărcăneşti"},

                        new City { Name = "Berceni"},

                        new City { Name = "Boldeşti Scăeni"},

                        new City { Name = "Breaza"},

                        new City { Name = "Buşteni"},

                        new City { Name = "Călugăreni"},

                        new City { Name = "Câmpina"},

                        new City { Name = "Ciorani"},

                        new City { Name = "Comarnic"},

                        new City { Name = "Dumbrăveşti"},

                        new City { Name = "Filipeştii de Pădure"},

                        new City { Name = "Filipeştii de Tîrg"},

                        new City { Name = "Gura Vadului"},

                        new City { Name = "Gura Vitioarei"},

                        new City { Name = "Măgurele"},

                        new City { Name = "Măgureni"},

                        new City { Name = "Mizil"},

                        new City { Name = "Păcureţi"},

                        new City { Name = "Păuleşti"},

                        new City { Name = "Ploieşti"},

                        new City { Name = "Plopeni"},

                        new City { Name = "Podenii Noi"},

                        new City { Name = "Poiana Ţapului"},

                        new City { Name = "PP Drajna"},

                        new City { Name = "Ploiesti"},

                        new City { Name = "Sălciile"},

                        new City { Name = "Sinaia"},

                        new City { Name = "Sîngeru"},

                        new City { Name = "Slănic Prahova"},

                        new City { Name = "Şurani"},

                        new City { Name = "Tinosu"},

                        new City { Name = "Urlaţi"},

                        new City { Name = "Valea Călugărească"},

                        new City { Name = "Văleni de Munte"},

                        new City { Name = "Floreşti"},

                        //new City { Name = "rnet"},

                        new City { Name = "Drajna"},

                        new City { Name = "Bucov"},

                        new City { Name = "Apostolache"},

                        new City { Name = "Secăria"},

                        new City { Name = "Sângeru"},

                        new City { Name = "Măneciu"},

                        new City { Name = "Poienarii Burchii"},

                        new City { Name = "Târgşorul Vechi"},

                        new City { Name = "Brazi"},

                        new City { Name = "Bertea"},

                        new City { Name = "Plopu"},

                        new City { Name = "Olari"},

                        new City { Name = "Tomşani"},

                        new City { Name = "Moara"},

                        new City { Name = "Cocorăştii Mislii"},

                        new City { Name = "Ciolpani"},

                        //new City { Name = "rta"},

                        new City { Name = "Puchenii Mari"},

                        new City { Name = "Telega"},

                        new City { Name = "Terișani"},

                        new City { Name = "Albeşti Paleologu"},

                        new City { Name = "Blejoi"},

                        new City { Name = "Adunaţi"},

                        new City { Name = "Proviţa de Jos"},

                        new City { Name = "Nedelea"},

                        new City { Name = "Brebu"},

                        new City { Name = "Cornu de Jos"},

                        new City { Name = "Baba Ana"},

                    }
                },

                new County
                {
                    Name = "Satu Mare",
                    ShortName = "SM",
                    Cities=new List<City>
                    {
                        new City { Name = "Carei"},

                        new City { Name = "Careii Mari"},

                        new City { Name = "Negreşti Oaş"},

                        new City { Name = "Satu Mare"},

                        new City { Name = "Tăşnad"},

                        new City { Name = "Hurezu Mare"},

                        new City { Name = "Livada"},

                        new City { Name = "Ardud"},

                        new City { Name = "Cehal"},

                        new City { Name = "Gherţa Mică"},

                        new City { Name = "Craidorolţ"},

                        new City { Name = "Halmeu"},

                        new City { Name = "Tarna Mare"},

                        new City { Name = "Terebeşti"},

                        new City { Name = "Tiream"},

                        new City { Name = "Beltiug"},

                        new City { Name = "Dorolt"},

                        new City { Name = "Doba"},

                        new City { Name = "Supur"},

                        new City { Name = "Bixad"},

                        new City { Name = "Acâş"},

                        new City { Name = "Odoreu"},

                        new City { Name = "Apa"},

                        new City { Name = "Valea Vinului"},

                        new City { Name = "Santău"},

                        new City { Name = "Tășnad"},

                        new City { Name = "Decebal"},

                        new City { Name = "Hohod"},

                    }
                },

                new County
                {
                    Name = "Sibiu",
                    ShortName = "SB",
                    Cities=new List<City>
                    {
                        new City { Name = "Agnita"},

                        new City { Name = "Avrig"},

                        new City { Name = "Cisnădie"},

                        new City { Name = "Copşa Mică"},

                        new City { Name = "Cristian"},

                        new City { Name = "Floreşti"},

                        new City { Name = "Mediaş"},

                        new City { Name = "Ocna Sibiului"},

                        new City { Name = "Răşinari"},

                        new City { Name = "Sibiu"},

                        new City { Name = "Tălmaciu"},

                        new City { Name = "Dumbrăveni"},

                        new City { Name = "Sălişte"},

                        new City { Name = "Agîrbiciu"},

                        new City { Name = "Roşia"},

                        new City { Name = "Hoghilag"},

                        new City { Name = "Laslea"},

                        new City { Name = "Şura Mare"},

                        new City { Name = "Axente Sever"},

                        new City { Name = "Blăjel"},

                        new City { Name = "Vurpăr"},

                        new City { Name = "Cârţa"},

                        new City { Name = "Șelimbăr"},

                        new City { Name = "Micasasa"},

                        new City { Name = "Turnu Roşu"},

                        new City { Name = "Nocrich"},

                    }
                },

                new County
                {
                    Name = "Suceava",
                    ShortName = "SV",
                    Cities=new List<City>
                    {
                        new City { Name = "Câmpulung Mold."},

                        new City { Name = "Dărmăneşti"},

                        new City { Name = "Dumbraveni"},

                        new City { Name = "Fălticeni"},

                        new City { Name = "Gura Humorului"},

                        new City { Name = "Rădăuţi"},

                        new City { Name = "Siret"},

                        new City { Name = "Slatina"},

                        new City { Name = "Solca"},

                        new City { Name = "Suceava"},

                        new City { Name = "Vatra Dornei"},

                        new City { Name = "Câmpulung Moldovenesc"},

                        new City { Name = "Sadova"},

                        new City { Name = "Dolhasca"},

                        new City { Name = "Broşteni"},

                        new City { Name = "Frasin"},

                        new City { Name = "Bogdăneşti"},

                        new City { Name = "Crucea"},

                        new City { Name = "Zvoriştea"},

                        new City { Name = "Cârlibaba"},

                        new City { Name = "Grăniceşti"},

                        new City { Name = "Ciprian Porumbescu"},

                        new City { Name = "Liteni"},

                        new City { Name = "Izvoarele Sucevei"},

                        new City { Name = "Râşca"},

                        new City { Name = "Burdujeni"},

                        new City { Name = "Horodniceni"},

                        new City { Name = "Valea Moldovei"},

                        new City { Name = "Marginea"},

                        new City { Name = "Salcea"},

                        new City { Name = "Iacobeni"},

                        new City { Name = "Brodina"},

                        new City { Name = "Udeşti"},

                        new City { Name = "Panaci"},

                        new City { Name = "Preuteşti"},

                        new City { Name = "Mălini"},

                        new City { Name = "Stroiești"},

                        new City { Name = "Vulturești"},

                        new City { Name = "Capu Câmpului"},

                        new City { Name = "Pojorâta"},

                        new City { Name = "Vicovu de Sus"},

                        new City { Name = "Vereşti"},

                        new City { Name = "Frătăuţii Noi"},

                        new City { Name = "Catalina"},

                        new City { Name = "Ipoteşti"},

                        new City { Name = "Poiana Stampei"},

                        new City { Name = "Drăguşeni"},

                        new City { Name = "Ilișeşti"},

                        new City { Name = "Vatra Moldoviței"},

                        new City { Name = "Vama"},

                        new City { Name = "Șcheia"},

                        new City { Name = "Dorneşti"},

                        new City { Name = "Cajvana"},

                        new City { Name = "Vicovu de Jos"},

                        new City { Name = "Bosanci"},

                        new City { Name = "Voitinel"},

                        new City { Name = "Baia"},

                        new City { Name = "Putna"},

                        new City { Name = "Dorna Candrenilor"},

                    }
                },

                new County
                {
                    Name = "Sălaj",
                    ShortName = "SJ",
                    Cities=new List<City>
                    {
                        new City { Name = "Cehul Silvaniei"},

                        new City { Name = "Jibou"},

                        new City { Name = "Şimleul Silv."},

                        new City { Name = "Zalău"},

                        new City { Name = "Şimleul Silvaniei"},

                        new City { Name = "Ileanda"},

                        new City { Name = "Nuşfalău"},

                        new City { Name = "Hida"},

                        new City { Name = "Mirşid"},

                        new City { Name = "Bobota"},

                        new City { Name = "Plopiş"},

                        new City { Name = "Boghiş"},

                        new City { Name = "Cehu Silvaniei"},

                        new City { Name = "Creaca"},

                        new City { Name = "Cormeniș"},

                        new City { Name = "Someș Guruslău"},

                        new City { Name = "Marca"},

                    }
                },

                new County
                {
                    Name = "Teleorman",
                    ShortName = "TR",
                    Cities=new List<City>
                    {
                        new City { Name = "Alexandria"},

                        new City { Name = "Drăgăneşti Vlaş"},

                        new City { Name = "Roşiori de Vede"},

                        new City { Name = "Turnu Măgurele"},

                        new City { Name = "Videle"},

                        new City { Name = "Zimnicea"},

                        new City { Name = "Bragadiru"},

                        new City { Name = "Dobroteşti"},

                        new City { Name = "Drăgăneşti Vlaşca"},

                        new City { Name = "Nanov"},

                        new City { Name = "Plosca"},

                        new City { Name = "Plopii Slovisteşti"},

                        new City { Name = "Drăgăneşti de Vede"},

                        new City { Name = "Trivalea Moșteni"},

                        new City { Name = "Uda Clocociov"},

                        new City { Name = "Poeni"},

                        new City { Name = "Scrioaştea"},

                        new City { Name = "Orbeasca"},

                        new City { Name = "Necșești"},

                        new City { Name = "Saelele"},

                        new City { Name = "Vitănești"},

                        new City { Name = "Buzescu"},

                    }
                },

                new County
                {
                    Name = "Timiş",
                    ShortName = "TM",
                    Cities=new List<City>
                    {
                        new City { Name = "Buziaş"},

                        new City { Name = "Deta"},

                        new City { Name = "Făget"},

                        new City { Name = "Jimbolia"},

                        new City { Name = "Luj"},

                        new City { Name = "Sânnicolau Mare"},

                        new City { Name = "Timişoara"},

                        new City { Name = "Variaş"},

                        new City { Name = "Bucovat"},

                        new City { Name = "Dobreşti"},

                        new City { Name = "Livezile"},

                        new City { Name = "REMETEA MARE"},

                        new City { Name = "Sannicolau Mare"},

                        new City { Name = "Jamu Mare"},

                        new City { Name = "Recaş"},

                        new City { Name = "Cărpiniş"},

                        new City { Name = "Vâlcani"},

                        new City { Name = "Cenad"},

                        new City { Name = "Giarmata"},

                        new City { Name = "Nădrag"},

                        new City { Name = "Sacoşu Turcesc"},

                        new City { Name = "Lenauheim"},

                        new City { Name = "Mănăştiur"},

                        new City { Name = "Saravale"},

                        new City { Name = "Dudeştii Noi"},

                        new City { Name = "Bara"},

                        new City { Name = "Giroc"},

                        new City { Name = "Sânpetru Mare"},

                        new City { Name = "Sânmihaiu Roman"},

                        new City { Name = "Pischia"},

                        new City { Name = "Dudeştii Vechi"},

                        new City { Name = "Ştiuca"},

                        new City { Name = "Chevereşu Mare"},

                        new City { Name = "Gavojdia"},

                        new City { Name = "Banloc"},

                        new City { Name = "Ghiroda"},

                        new City { Name = "Belint"},

                        new City { Name = "Budeştii Noi"},

                        new City { Name = "Orţişoara"},

                        new City { Name = "Tomnatic"},

                        new City { Name = "Fibiş"},

                        new City { Name = "Cenei"},

                        new City { Name = "Voiteg"},

                        new City { Name = "Sagna"},

                        new City { Name = "Gătaia"},

                        new City { Name = "Dumbrava"},

                        new City { Name = "Sloc"},

                        new City { Name = "Teremia Mare"},

                        new City { Name = "Periam"},

                        new City { Name = "Birda"},

                        new City { Name = "Pesac"},

                        new City { Name = "Margina"},

                        new City { Name = "Sinmihaiu Roman"},

                        new City { Name = "Otelec"},

                        new City { Name = "Ciacova"},

                        new City { Name = "Moraviţa"},

                        new City { Name = "Liebling"},

                        new City { Name = "Uivar"},

                        new City { Name = "Fârdea"},

                        new City { Name = "Racoviţa"},

                    }
                },

                new County
                {
                    Name = "Tulcea",
                    ShortName = "TL",
                    Cities=new List<City>
                    {
                        new City { Name = "Babadag"},

                        new City { Name = "Isaccea"},

                        new City { Name = "Măcin"},

                        new City { Name = "Sfântu Gheorghe"},

                        new City { Name = "Sulina"},

                        new City { Name = "Tulcea"},

                        new City { Name = "Greci"},

                        new City { Name = "Vulturu"},

                        new City { Name = "Tichileşti"},

                        new City { Name = "Somova"},

                        new City { Name = "Mahmudia"},

                        new City { Name = "Baia"},

                        new City { Name = "Valea Nucilor"},

                        new City { Name = "Mihai Bravu"},

                        new City { Name = "IC Brătianu"},

                        new City { Name = "Izvoarele"},

                        new City { Name = "Peceneaga"},

                        new City { Name = "Smârdan"},

                        new City { Name = "Cerna"},

                        new City { Name = "Casimcea"},

                        new City { Name = "Jijila"},

                        new City { Name = "Dăeni"},

                        new City { Name = "Mila"},

                        new City { Name = "Garvăn"},

                        new City { Name = "Frecăţei"},

                        new City { Name = "Nufăru"},

                        new City { Name = "Ciucurova"},

                        new City { Name = "Ceamurlia de Sus"},

                        new City { Name = "Ceamurlia de Jos"},

                    }
                },

                new County
                {
                    Name = "Vaslui",
                    ShortName = "VS",
                    Cities=new List<City>
                    {
                        new City { Name = "Bârlad"},

                        new City { Name = "Costeşti"},

                        new City { Name = "Huşi"},

                        new City { Name = "Murgeni"},

                        new City { Name = "Negreşti"},

                        new City { Name = "Vaslui"},

                        new City { Name = "Voineşti"},

                        new City { Name = "Brădeşti"},

                        new City { Name = "Hârsoveni"},

                        new City { Name = "Ivăneşti"},

                        new City { Name = "Lipovăţ"},

                        new City { Name = "Pădureni"},

                        new City { Name = "Hoceni"},

                        new City { Name = "Sărăţeni"},

                        new City { Name = "Poieneşti"},

                        new City { Name = "Ghergheşti"},

                        new City { Name = "Perieni"},

                        new City { Name = "Epureni"},

                        new City { Name = "Brodoc"},

                        new City { Name = "Albesti"},

                        new City { Name = "Zorleni"},

                        new City { Name = "Grivita"},

                        new City { Name = "Lunca Banului"},

                        new City { Name = "Iana"},

                        new City { Name = "Micleşti"},

                        new City { Name = "Fereşti"},

                        new City { Name = "Șuletea"},

                        new City { Name = "Drînceni"},

                        new City { Name = "Zapodeni"},

                        new City { Name = "Rafaila"},

                        new City { Name = "Băceşti"},

                        new City { Name = "Tutova"},

                        new City { Name = "Muntenii de Jos"},

                        new City { Name = "Oseşti"},

                        new City { Name = "Arsura"},

                        new City { Name = "Todireşti"},

                        new City { Name = "Olteneşti"},

                        new City { Name = "Bogdănești"},

                        new City { Name = "Roşieşti"},

                        new City { Name = "Muntenii de Sus"},

                        new City { Name = "Băcani"},

                        new City { Name = "Dăneşti"},

                        new City { Name = "Codăești"},

                        new City { Name = "Tanacu"},

                    }
                },

                new County
                {
                    Name = "Vrancea",
                    ShortName = "VN",
                    Cities=new List<City>
                    {
                        new City { Name = "Adjud"},

                        new City { Name = "Focşani"},

                        new City { Name = "Mărăşeşti"},

                        new City { Name = "Odobeşti (VN},"},

                        new City { Name = "Panciu"},

                        new City { Name = "Andreiaşu"},

                        new City { Name = "Andreiaşu de Jos"},

                        new City { Name = "Bârseşti (VN},"},

                        new City { Name = "Bilieşti"},

                        new City { Name = "Bogheşti"},

                        new City { Name = "Boloteşti"},

                        new City { Name = "Bordeşti"},

                        new City { Name = "Câmpineanca"},

                        new City { Name = "Cârligele"},

                        new City { Name = "Chiojdeni"},

                        new City { Name = "Cîmpuri"},

                        new City { Name = "Ciorăşti"},

                        new City { Name = "Cîrligele"},

                        new City { Name = "Corbiţa"},

                        new City { Name = "Dumitreşti"},

                        new City { Name = "Fitioneşti"},

                        new City { Name = "Garoafa"},

                        new City { Name = "loganu"},

                        new City { Name = "Gugeşti"},

                        new City { Name = "Gura Caliţei"},

                        new City { Name = "Homocea"},

                        new City { Name = "Jariştea"},

                        new City { Name = "Jitia"},

                        new City { Name = "Măicăneşti"},

                        new City { Name = "Milcovul"},

                        new City { Name = "Moviliţa (VN},"},

                        new City { Name = "Năneşti"},

                        new City { Name = "Năruja"},

                        new City { Name = "Negrileşti"},

                        new City { Name = "Nereju"},

                        new City { Name = "Nistoreşti"},

                        new City { Name = "Obrejiţa"},

                        new City { Name = "Paltin"},

                        new City { Name = "Păuneşti"},

                        new City { Name = "Ploscuţeni"},

                        new City { Name = "Poiana Cristei"},

                        new City { Name = "Pufeşti"},

                        new City { Name = "Răcoasa"},

                        new City { Name = "Răstoaca"},

                        new City { Name = "Reghiu"},

                        new City { Name = "Rugineşti"},

                        new City { Name = "Sihlea"},

                        new City { Name = "Slobozia Bradului"},

                        new City { Name = "Slobozia Ciorăşti"},

                        new City { Name = "Soveja"},

                        new City { Name = "Străoane"},

                        new City { Name = "Suraia"},

                        new City { Name = "Tănăsoaia"},

                        new City { Name = "Tătăranu"},

                        new City { Name = "Ţifeşti"},

                        new City { Name = "Tîmboieşti"},

                        new City { Name = "Tulnici"},

                        new City { Name = "Valea Sării"},

                        new City { Name = "Vintileasca"},

                        new City { Name = "Vîrteşcoiu"},

                        new City { Name = "Vizantea Livezi"},

                        new City { Name = "Vrîncioaia"},

                        new City { Name = "Tulburea"},

                        new City { Name = "Popeşti (VN},"},

                        new City { Name = "Urecheşti (VN},"},

                        new City { Name = "Vulturu (VN},"},

                    }
                },

                new County
                {
                    Name = "Vâlcea",
                    ShortName = "VL",
                    Cities=new List<City>
                    {
                        new City { Name = "Băile vora"},

                        new City { Name = "Băile Olăneşti"},

                        new City { Name = "Bălceşti"},

                        new City { Name = "Brezoi"},

                        new City { Name = "Căciulata"},

                        new City { Name = "Călimăneşti"},

                        new City { Name = "Costeşti"},

                        new City { Name = "Drăgăşani"},

                        //new City { Name = "vora"},

                        new City { Name = "Horezu"},

                        new City { Name = "Olăneşti"},

                        new City { Name = "Râmnicu Vâlcea"},

                        new City { Name = "Amărăşti"},

                        new City { Name = "Budeşti"},

                        new City { Name = "Căzăneşti"},

                        new City { Name = "Cireşu"},

                        new City { Name = "Popeşti"},

                        new City { Name = "Voineasa"},

                        new City { Name = "Bunesti"},

                        new City { Name = "Frânceşti"},

                        new City { Name = "Roeşti"},

                        new City { Name = "Vaideeni"},

                        new City { Name = "Falcoiu"},

                        new City { Name = "Creţeni"},

                        new City { Name = "Lăpuşata"},

                        new City { Name = "Câineni"},

                        new City { Name = "Laloșu"},

                        new City { Name = "Păuşeşti"},

                        new City { Name = "Lădeşti"},

                        new City { Name = "Livezi"},

                        new City { Name = "Băbeni"},

                    }
                },

            };

            context.AddRange(counties);
            context.SaveChanges();
        }
    }
}
