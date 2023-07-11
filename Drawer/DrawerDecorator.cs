using Chess.Core;

namespace ChessTerminal.Drawer;

public class DrawerDecorator
{
    private Game game;
    private string currentPlayerStr =>
        game.currentPlayer.color == Color.WHITE ? "White" : "Black";

    public DrawerDecorator(Game game) => this.game = game;

    public void DrawLetterLine()
    {
        Console.ResetColor();

        for (int i = 0; i < game.board.grid.GetLength(0) + 1; i++)
            DrawLetters(i);

        Console.WriteLine();
    }

    public void DrawNumber(int i)
    {
        Console.ResetColor();
        Console.Write(" " + Math.Abs(-1 - i) + " ");
    }

    public void DrawCurrentPlayerInfo()
    {
        DrawMessage(currentPlayerStr + "'s turn.", ConsoleColor.White);
        DrawMessage("Waiting for input...", ConsoleColor.Green);
    }

    public void DrawError(Exception e) =>
        DrawMessage(e.Message, ConsoleColor.Red);

    public void DrawCurrentKingIsChecked() =>
        DrawMessage(currentPlayerStr + " king is checked!", ConsoleColor.Yellow);

    public void DrawCurrentKingIsCheckmated() =>
        DrawMessage(currentPlayerStr + " king is checkmated!", ConsoleColor.Red);

    public void DrawGameIsInStaleMate() =>
        DrawMessage("Stalemate.", ConsoleColor.Red);

    private void DrawMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private void DrawLetters(int i)
    {
        if (i == 0)
            Console.Write("  ");
        else
            DrawLetter(i);
    }

    private void DrawLetter(int i)
    {
        char letter = (char)(i - 1 + 65);
        Console.Write(" " + letter.ToString().ToLower());
    }
}