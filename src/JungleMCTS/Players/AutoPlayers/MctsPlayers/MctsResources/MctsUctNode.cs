using JungleMCTS.Enums;
using JungleMCTS.GameBoard;

namespace JungleMCTS.Players.AutoPlayers.MctsPlayers.MctsResources
{
    public class MctsUctNode
    {
        public MctsUctNode? Parent { get; set; }

        public List<MctsUctNode> Children { get; set; } = [];

        public double Points { get; set; } = 0;

        public double Visits { get; set; } = 0;

        public Board Board { get; set; }

        public List<MctsAction> UntriedActions { get; set; }

        public MctsAction? Action { get; set; }

        public double Value => Visits > 0 ? Points / Visits : -1;

        public MctsUctNode(MctsUctNode? parent, Board board, List<MctsAction> untriedActions, MctsAction? action = null)
        {
            Parent = parent;
            Board = board;
            UntriedActions = untriedActions;
            Action = action;
        }

        public MctsUctNode SelectChild(double c)
        {
            return Children.OrderByDescending(child =>
                child.Points / (child.Visits + 1) +
                c * Math.Sqrt(2 * Math.Log(Visits + 1) / (child.Visits + 1))
            ).First();
        }

        public void AddChild(MctsUctNode child)
        {
            Children.Add(child);
        }
    }
}
