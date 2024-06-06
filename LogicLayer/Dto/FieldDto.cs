namespace LogicLayer.Dto
{
    public class FieldDto
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
        public decimal MinePercent { get; set; }
        public CellDto[,] MineField { get; set; }
        public FieldDto(int horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            MineField = new CellDto[horizontal, vertical];
        }

    }
}
