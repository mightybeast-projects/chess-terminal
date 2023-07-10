using Chess.Core;

namespace ChessTerminal.Drawer;

public class DrawerDecorator
{
    private Game game;

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
        string currentPlayerStr =
            (game.currentPlayer.color == Color.WHITE) ? "♟ " : "♙ ";

        Console.WriteLine(currentPlayerStr + "'s turn.");

        Console.WriteLine("Waiting for input...");
    }

    public void DrawError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
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