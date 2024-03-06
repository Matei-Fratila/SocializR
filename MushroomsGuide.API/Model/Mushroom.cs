using MushroomsGuide.API.Model.Enums;

namespace MushroomsGuide.API.Model;

public class Mushroom
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
    public int[] Perioada { get; set; } = [];
    public string? PerioadaDeAparitie { get; set; }
    public string? ValoareaAlimentara { get; set; }
    public string? SpeciiAsemanatoare { get; set; }
    public int[]? IdSpeciiAsemanatoare { get; set; }
    public bool? EsteMedicinala { get; set; }

    public CorpFructiferEnum MorfologieCorpFructifer { get; set; }
    public ComestibilitateEnum Comestibilitate { get; set; }
    public HashSet<LocDeFructificatieEnum> LocDeFructificatie { get; set; } = [];

    private HashSet<LuniEnum> _luniDeAparitie = [];
    public HashSet<LuniEnum> LuniDeAparitie
    {
        get
        {
            for (var i = Perioada[0]; i <= Perioada[1]; i++)
            {
                _luniDeAparitie.Add((LuniEnum)i);
            }

            return _luniDeAparitie;
        }

        set => _luniDeAparitie = value;
    }
}
