using Chess.Core;
using Chess.Core.Pieces;
using ChessTerminal.Drawer;

namespace ChessTerminal;

public class InputHandler
{
    private DrawerFacade drawer;
    private Game game;
    private string input;

    public InputHandler(Game game, DrawerFacade drawer)
    {
        this.game = game;
        this.drawer = drawer;
    }

    public void Run()
    {
        try { HandleInput(); }
        catch (Exception e) { drawer.DrawError(e); }
    }

    private void HandleInput()
    {
        input = Console.ReadLine();

        if (input != null)
            HandleChosenPiece();

        drawer.DrawGame();
    }

    private void HandleChosenPiece()
    {
        if (input.Length == 0)
            return;

        System.Console.WriteLine(input);

        Piece chosenPiece = game.board.GetTile(input).piece;

        if (InputIsHintCommand())
            drawer.EnableHintsFor(chosenPiece);
        else if (InputIsMoveCommand())
        {
            string pieceTileStr = input.Split(" ")[0];
            string targetTileStr = input.Split(" ")[1];
            game.HandlePlayerMove(pieceTileStr, targetTileStr);
        }
    }

    private bool InputIsHintCommand() => input.Length == 2;

    private bool InputIsMoveCommand() => input.Length == 5;
}