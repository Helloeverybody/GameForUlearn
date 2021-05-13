using System.Collections.Generic;
using System.Windows.Forms;

namespace Model.Interfaces
{
    public interface IMainForm
    {
        Map Map { get; set; }
        Player Player { get; set; }
        Timer moveTimer { get; }
        Timer timer { get; }
        List<OnMapItem> itemsOnMap { get; set; }
        GameState GameState { get; set; }
    }
}