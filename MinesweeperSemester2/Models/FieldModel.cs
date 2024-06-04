using web;
namespace web.Models

{
    public class FieldModel
    {
        public CellModel[,] field { get; set; }
        public string GameStatus { get; set; }
    }
}
