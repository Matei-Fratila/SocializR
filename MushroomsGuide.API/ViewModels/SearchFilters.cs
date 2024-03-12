using MushroomsGuide.API.Model.Enums;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace MushroomsGuide.API.ViewModels;

public class SearchFilters
{
    [FromQuery]
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    [FromQuery]
    public bool? EsteInSezon { get; set; }
    private string EsteInSezonQuery => (EsteInSezon.HasValue && EsteInSezon.Value)
        ? $"@LuniDeAparitie:[{DateTime.Today.Month} {DateTime.Today.Month}] " : String.Empty;

    [FromQuery]
    public int[]? LuniDeAparitie { get; set; }
    private string LuniDeAparitieQuery
    {
        get
        {
            if ((EsteInSezon == null || EsteInSezon.Value == false) &&
                LuniDeAparitie != null && LuniDeAparitie.Any())
            {
                var builder = new StringBuilder("(");
                foreach (var luna in LuniDeAparitie)
                {
                    builder.Append($" @LuniDeAparitie:[{luna} {luna}] |");
                }

                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }

            return string.Empty;
        }
    }

    [FromQuery]
    public CorpFructiferEnum[]? MorfologieCorpFructifer { get; set; }
    private string MorfologieCorpFructiferQuery
    {
        get
        {
            if (MorfologieCorpFructifer != null && MorfologieCorpFructifer.Any())
            {
                var builder = new StringBuilder("(");
                foreach (var corpFructifer in MorfologieCorpFructifer)
                {
                    builder.Append($" @MorfologieCorpFructifer:[{(int)corpFructifer} {(int)corpFructifer}] |");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }
            return string.Empty;
        }
    }

    [FromQuery]
    public LocDeFructificatieEnum[]? LocDeFructificatie { get; set; }
    private string LocDeFructificatieQuery
    {
        get
        {
            if (LocDeFructificatie != null && LocDeFructificatie.Any())
            {
                var builder = new StringBuilder("(");
                foreach (var loc in LocDeFructificatie)
                {
                    builder.Append($" @LocDeFructificatie:[{(int)loc} {(int)loc}] |");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }
            return string.Empty;
        }
    }

    [FromQuery]
    public ComestibilitateEnum[]? Comestibilitate { get; set; }
    private string ComestibilitateQuery
    {
        get
        {
            if (Comestibilitate != null && Comestibilitate.Any())
            {
                var builder = new StringBuilder("(");
                foreach (var comestibilitate in Comestibilitate)
                {
                    builder.Append($" @Comestibilitate:[{(int)comestibilitate} {(int)comestibilitate}] |");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }
            return string.Empty;
        }
    }

    [FromQuery]
    public bool? EsteMedicinala { get; set; }
    private string EsteMedicinalaQuery => (EsteMedicinala != null && EsteMedicinala.Value) ? "@EsteMedicinala:{true} " : String.Empty;

    [FromQuery]
    public int[]? IdSpeciiAsemanatoare { get; set; }
    private string IdSpeciiAsemanatoareQuery
    {
        get
        {
            if (IdSpeciiAsemanatoare != null && IdSpeciiAsemanatoare.Any())
            {
                var builder = new StringBuilder("(");
                foreach (var id in IdSpeciiAsemanatoare)
                {
                    builder.Append($" @IdSpeciiAsemanatoare:[{id} {id}] |");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }
            return string.Empty;
        }
    }

    [FromQuery]
    public string[]? Gen { get; set; }
    private string GenQuery
    {
        get
        {
            if (Gen != null && Gen.Any())
            {
                var builder = new StringBuilder("@Denumire:(");
                foreach (var gen in Gen)
                {
                    builder.Append($" {gen} |");
                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(") ");
                return builder.ToString();
            }
            return string.Empty;
        }
    }

    [FromQuery]
    public string? SortBy { get; set; }

    [FromQuery]
    public bool? IsAscendingOrder { get; set; }

    public string Query
    {
        get
        {
            var stringBuilder = new StringBuilder();

            var query = stringBuilder.Append(EsteInSezonQuery)
                        .Append(LuniDeAparitieQuery)
                        .Append(MorfologieCorpFructiferQuery)
                        .Append(LocDeFructificatieQuery)
                        .Append(ComestibilitateQuery)
                        .Append(EsteMedicinalaQuery)
                        .Append(IdSpeciiAsemanatoareQuery)
                        .Append(GenQuery)
                        .ToString();

            if (query != string.Empty)
            {
                return query;
            }

            return "*";
        }
    }
}
