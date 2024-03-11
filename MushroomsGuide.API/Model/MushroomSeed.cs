using MushroomsGuide.API.Model.Enums;

namespace MushroomsGuide.API.Model;

public class MushroomSeed
{
    public int Id { get; set; }
    public required string Denumire { get; set; }
    public string? DenumirePopulara { get; set; }
    public string? CorpulFructifer { get; set; }
    public string? Ramurile { get; set; }
    public string? Palaria { get; set; }
    public string? StratulHimenial { get; set; }
    public string? Gleba { get; set; }
    public string? TuburileSporifere { get; set; }
    public string? Lamelele { get; set; }
    public string? Piciorul { get; set; }
    public string? Carnea { get; set; }
    public string? PerioadaDeAparitie { get; set; }
    public string? ValoareaAlimentara { get; set; }
    public string? SpeciiAsemanatoare { get; set; }
    public int[]? IdSpeciiAsemanatoare { get; set; }


    private bool? _esteMedicinala;
    public bool? EsteMedicinala
    {
        get
        {
            if (ValoareaAlimentara == null) return null;

            var txt = ValoareaAlimentara.ToLower();
            if (txt.Contains("medicinala") || txt.Contains("medicinală"))
            {
                return true;

            }
            else if (txt.Contains("terapeutica") || txt.Contains("terapeutică"))
            {
                return true;

            }

            return false;
        }
        set => _esteMedicinala = value;

    }


    private CorpFructiferEnum _morfologieCorpFructifer;
    public CorpFructiferEnum MorfologieCorpFructifer
    {
        get
        {
            if (Id <= 77) return CorpFructiferEnum.HimenoforNelamelarNetubular;
            else if (Id <= 137) return CorpFructiferEnum.HimenoforTubular;
            else return CorpFructiferEnum.HimenoforLamelar;
        }
        set => _morfologieCorpFructifer = value;
    }


    private ComestibilitateEnum _comestibilitate;
    public ComestibilitateEnum Comestibilitate
    {
        get
        {
            //if (ValoareaAlimentara == null) return ComestibilitateEnum.Necunoscută;

            var txt = ValoareaAlimentara.ToLower();
            if (txt.Contains("otravitoare") || txt.Contains("otrăvitoare"))
            {
                return ComestibilitateEnum.Otrăvitoare;

            }
            else if (txt.Contains("necomestibila") || txt.Contains("necomestibilă"))
            {
                return ComestibilitateEnum.Necomestibilă;

            }
            else if (txt.Contains("conditionat") || txt.Contains("condiționat"))
            {
                return ComestibilitateEnum.CondiționatComestibilă;

            }
            else if (txt.Contains("comestibila") || txt.Contains("comestibilă"))
            {
                return ComestibilitateEnum.Comestibilă;
            }

            return ComestibilitateEnum.Necomestibilă;

            //return ComestibilitateEnum.Necunoscută;
        }
        set => _comestibilitate = value;
    }


    private HashSet<LocDeFructificatieEnum> _locDeFructificatie = [];
    public HashSet<LocDeFructificatieEnum> LocDeFructificatie
    {
        get
        {
            if (PerioadaDeAparitie == null) return _locDeFructificatie;

            var txt = PerioadaDeAparitie.ToLower();

            if (txt.Contains("foioase") || txt.Contains("foioaselor") || txt.Contains("plopi"))
            {
                _locDeFructificatie.Add(LocDeFructificatieEnum.PădureDeFoioase);

            }
            if (txt.Contains("conifere") || txt.Contains("coniferelor") || txt.Contains("brad") || txt.Contains("brazi")
                || txt.Contains("pin") || txt.Contains("pini") || txt.Contains("zadă"))
            {
                _locDeFructificatie.Add(LocDeFructificatieEnum.PădureDeConifere);

            }
            if (txt.Contains("pajiste") || txt.Contains("pasune") || txt.Contains("iarba")
                || txt.Contains("campie") || txt.Contains("iarbă") || txt.Contains("câmpie")
                || txt.Contains("pajiște") || txt.Contains("pășune") || txt.Contains("pașune")
                || txt.Contains("pajiști") || txt.Contains("pășuni") || txt.Contains("câmpii")
                || txt.Contains("pajisti") || txt.Contains("pasuni") || txt.Contains("campii"))
            {
                _locDeFructificatie.Add(LocDeFructificatieEnum.Pășune);

            }

            if (txt.Contains("putred") || txt.Contains("lemnul putred") || txt.Contains("lemnul mort"))
            {
                _locDeFructificatie.Add(LocDeFructificatieEnum.CrengiȘiCioate);
            }

            return _locDeFructificatie;
        }
        set => _locDeFructificatie = value;
    }


    private HashSet<int> _luniDeAparitie = [];
    public HashSet<int> LuniDeAparitie
    {
        get
        {
            if (PerioadaDeAparitie == null) return [];
            var words = PerioadaDeAparitie.Split(['-', ',', ';', ':'], 3);
            var startMonth = TransformMonth(words[0].ToLower());
            var endMonth = TransformMonth(words[1].ToLower());

            if (startMonth != 0 && endMonth != 0)
            {
                for (var i = startMonth; i <= endMonth; i++)
                {
                    _luniDeAparitie.Add(i);
                }
            }

            return _luniDeAparitie;
        }
        set => _luniDeAparitie = value;
    }

    private int[] _perioada;
    public int[] Perioada
    {
        get
        {
            if (PerioadaDeAparitie == null) return [];
            var words = PerioadaDeAparitie.Split(['-', ',', ';', ':'], 3);
            var startMonth = TransformMonth(words[0].ToLower());
            var endMonth = TransformMonth(words[1].ToLower());

            return [startMonth, endMonth];
        }
        set => _perioada = value;
    }


    private int TransformMonth(string monthName)
    {
        switch (monthName)
        {
            case "ianuarie": return 1;
            case "februarie": return 2;
            case "martie": return 3;
            case "aprilie": return 4;
            case "mai": return 5;
            case "iunie": return 6;
            case "iulie": return 7;
            case "august": return 8;
            case "septembrie": return 9;
            case "octombrie": return 10;
            case "noiembrie": return 11;
            case "decembrie": return 12;
            default: return 0;
        }
    }
}
