namespace MushroomsGuide.API.ViewModels;

public class MushroomGraph
{
    public IEnumerable<MushroomNode> Nodes { get; set; }
    public IEnumerable<MushroomEdge> Edges { get; set; }
}
