namespace web.Models
{
    public class CellModel
    {
        public int Horizontal {  get; set; }
        public int Vertical { get; set; }
        public int MinesAroundCell { get; set; }
        public int IsMine { get; set; }
        public int IsVisible { get; set; }
    }
}
