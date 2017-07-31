using SQLite;

namespace FST.Tools.CheckMeshSize
{
    public class CheckMeshSizeTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Temperature { get; set; }
        public double CellSize { get; set; }
        public double Gravity { get; set; }
        public double SpecificHeat { get; set; }
        public double Density { get; set; }
        public double HeatReleaseRate { get; set; }
        public double FireDiameter { get; set; }
        public double Ratio { get; set; }
    }
}
